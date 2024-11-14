using Monitoring.Domain;

namespace Monitoring.App.Interfaces.Storages;

public interface IBuildingStorage
{
    Task<List<Building>> GetBuildingsAsync(Guid id, int pageSize, int pageNumber,
        CancellationToken cancellationToken = default);

    Task<Building?> GetBuildingAsync(Guid buildingId, CancellationToken cancellationToken = default);

    Task<Guid> CreateBuildingAsync(Building building, CancellationToken cancellationToken = default);

    Task<Guid> DeleteBuildingAsync(Building building, CancellationToken cancellationToken = default);

    Task<Building> UpdateBuildingAsync(Building building, CancellationToken cancellationToken = default);
}