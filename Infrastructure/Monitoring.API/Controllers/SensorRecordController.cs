using Microsoft.AspNetCore.Mvc;
using Monitoring.App.Dtos.SensorRecord;
using Monitoring.App.Services;

namespace Monitoring.API.Controllers;

[Route("api/[controller]")]
public class SensorRecordController : CustomControllerBase
{
    private readonly SensorRecordService _sensorRecordService;

    public SensorRecordController(SensorRecordService sensorRecordService)
    {
        _sensorRecordService = sensorRecordService;
    }

    // GET: api/SensorRecord
    [HttpGet("{sensorId:guid}/get-all")]
    public async Task<IActionResult> GetSensorRecordsAsync(Guid sensorId, int pageSize, int pageNumber,
        DateTime startDate, DateTime endDate,
        CancellationToken cancellationToken)
    {
        var sensorRecords =
            await _sensorRecordService.GetSensorRecordsAsync(sensorId, pageSize, pageNumber, startDate, endDate, cancellationToken);
        return Ok(sensorRecords);
    }

    // GET: api/SensorRecord/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSensorRecordAsync(Guid id, CancellationToken cancellationToken)
    {
        var sensorRecord = await _sensorRecordService.GetSensorRecordAsync(id, cancellationToken);
        return Ok(sensorRecord);
    }

    // POST: api/SensorRecord
    [HttpPost]
    public async Task<IActionResult> CreateSensorRecordAsync([FromBody] SensorRecordDto sensorRecordDto,
        CancellationToken cancellationToken)
    {
        var sensorRecordId = await _sensorRecordService.CreateSensorRecordAsync(sensorRecordDto, cancellationToken);
        return Ok(sensorRecordId);
    }
}