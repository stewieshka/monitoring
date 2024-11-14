using Microsoft.AspNetCore.Mvc;
using Monitoring.App.Dtos.Sensor;
using Monitoring.App.Services;

namespace Monitoring.API.Controllers;

[Route("api/[controller]")]
public class SensorController : CustomControllerBase
{
    private readonly SensorService _sensorService;
    private readonly ImageService _imageService;

    public SensorController(SensorService sensorService, ImageService imageService)
    {
        _sensorService = sensorService;
        _imageService = imageService;
    }

    // GET: api/Sensor
    [HttpGet("{buildingId:guid}/get-all")]
    public async Task<IActionResult> GetSensorsAsync(Guid buildingId, int pageSize, int pageNumber,
        CancellationToken cancellationToken)
    {
        var sensors = await _sensorService.GetSensorsAsync(buildingId, pageSize, pageNumber, cancellationToken);
        return Ok(sensors);
    }

    // GET: api/Sensor/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSensorAsync(Guid id, CancellationToken cancellationToken)
    {
        var sensor = await _sensorService.GetSensorAsync(id, cancellationToken);
        return Ok(sensor);
    }

    // POST: api/Sensor
    [HttpPost]
    public async Task<IActionResult> CreateSensorAsync([FromBody] SensorDto sensorDto,
        CancellationToken cancellationToken)
    {
        var sensorId = await _sensorService.CreateSensorAsync(sensorDto, cancellationToken);
        return Ok(sensorId);
    }

    [HttpPut("add-image/{id:guid}")]
    public async Task<IActionResult> AddSensorImage(IFormFile sensorImage, Guid id)
    {
        var sensor = await _sensorService.GetSensorAsync(id);
        
        var fileName = Guid.NewGuid() + sensorImage.FileName;
        
        await using var stream = sensorImage.OpenReadStream();
        var response = await _imageService.UploadImageAsync(fileName, stream);

        sensor.PhotoUrl = $"localhost:9000/images/{response.ObjectName}";

        await _sensorService.UpdateSensorAsync(sensor);

        return Ok();
    }

    // DELETE: api/Sensor/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSensorAsync(Guid id, CancellationToken cancellationToken)
    {
        await _sensorService.DeleteSensorAsync(id, cancellationToken);
        return NoContent();
    }

    // PUT: api/Sensor/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateSensorAsync(Guid id, [FromBody] SensorDto sensorDto,
        CancellationToken cancellationToken)
    {
        var updatedSensor = await _sensorService.UpdateSensorAsync(id, sensorDto, cancellationToken);
        return Ok(updatedSensor);
    }
}