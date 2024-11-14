using Microsoft.AspNetCore.Mvc;
using Monitoring.App.Dtos.Building;
using Monitoring.App.Services;
using Monitoring.Domain;

namespace Monitoring.API.Controllers;

[Route("api/[controller]")]
public class BuildingController : CustomControllerBase
{
    private readonly BuildingService _buildingService;

    public BuildingController(BuildingService buildingService)
    {
        _buildingService = buildingService;
    }

    // GET: api/Building
    [HttpGet("{userId:guid}/get-all")]
    public async Task<IActionResult> GetBuildingsAsync(Guid userId, int pageSize, int pageNumber,
        CancellationToken cancellationToken)
    {
        var buildings = await _buildingService.GetBuildingsAsync(userId, pageSize, pageNumber, cancellationToken);
        return Ok(buildings);
    }

    // GET: api/Building/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBuildingAsync(Guid id, CancellationToken cancellationToken)
    {
        var building = await _buildingService.GetBuildingAsync(id, cancellationToken);

        return Ok(building);
    }

    // POST: api/Building
    [HttpPost]
    public async Task<IActionResult> CreateBuildingAsync([FromBody] BuildingDto building,
        CancellationToken cancellationToken)
    {
        var buildingId = await _buildingService.CreateBuildingAsync(building, cancellationToken);

        return Ok(buildingId);
    }

    // DELETE: api/Building/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBuildingAsync(Guid id, CancellationToken cancellationToken)
    {
        await _buildingService.DeleteBuildingAsync(id, cancellationToken);
        return NoContent();
    }

    // PUT: api/Building/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBuildingAsync(Guid id, [FromBody] BuildingDto dto,
        CancellationToken cancellationToken)
    {
        var updatedBuilding = await _buildingService.UpdateBuildingAsync(id, dto, cancellationToken);
        return Ok(updatedBuilding);
    }
}