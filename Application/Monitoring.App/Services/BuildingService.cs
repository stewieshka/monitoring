using AutoMapper;
using Monitoring.App.Dtos.Building;
using Monitoring.App.Interfaces.Storages;
using Monitoring.Domain;

namespace Monitoring.App.Services;

public class BuildingService
{
    private readonly IBuildingStorage _buildingStorage;
    private readonly IMapper _mapper;

    public BuildingService(IBuildingStorage buildingStorage, IMapper mapper)
    {
        _buildingStorage = buildingStorage;
        _mapper = mapper;
    }

    public async Task<List<BuildingDto>> GetBuildingsAsync(Guid userId, int pageSize, int pageNumber,
        CancellationToken cancellationToken = default)
    {
        var buildings = await _buildingStorage.GetBuildingsAsync(userId, pageSize, pageNumber, cancellationToken);

        var buildingsDto = new List<BuildingDto>(buildings.Count);

        buildingsDto.AddRange(buildings.Select(building => _mapper.Map<BuildingDto>(building)));

        return buildingsDto;
    }

    public async Task<BuildingDto?> GetBuildingAsync(Guid buildingId, CancellationToken cancellationToken = default)
    {
        var building = await _buildingStorage.GetBuildingAsync(buildingId, cancellationToken);
        return _mapper.Map<BuildingDto>(building);
    }

    public async Task<Guid> CreateBuildingAsync(BuildingDto dto, CancellationToken cancellationToken = default)
    {
        var building = _mapper.Map<Building>(dto);

        return await _buildingStorage.CreateBuildingAsync(building, cancellationToken);
    }

    public async Task<Guid> DeleteBuildingAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var building = await _buildingStorage.GetBuildingAsync(id, cancellationToken);

        return await _buildingStorage.DeleteBuildingAsync(building, cancellationToken);
    }

    public async Task<Building> UpdateBuildingAsync(Guid id, BuildingDto dto,
        CancellationToken cancellationToken = default)
    {
        var building = _mapper.Map<Building>(dto);

        building.Id = id;

        return await _buildingStorage.UpdateBuildingAsync(building, cancellationToken);
    }
}