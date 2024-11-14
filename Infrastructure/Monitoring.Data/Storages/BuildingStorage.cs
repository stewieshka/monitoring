using Microsoft.EntityFrameworkCore;
using Monitoring.App.Interfaces.Storages;
using Monitoring.Data.Database;
using Monitoring.Domain;

namespace Monitoring.Data.Storages;

public class BuildingStorage : IBuildingStorage
{
    private readonly MonitoringDbContext _context;

    public BuildingStorage(MonitoringDbContext context)
    {
        _context = context;
    }

    public async Task<List<Building>> GetBuildingsAsync(Guid id, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
    {
        var query = _context.Buildings.AsQueryable();
        
        query = query
            .Where(x => x.UserId == id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return await query.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<Building?> GetBuildingAsync(Guid buildingId, CancellationToken cancellationToken = default)
    {
        var building = await _context.Buildings.FirstOrDefaultAsync(x => x.Id == buildingId, cancellationToken: cancellationToken);

        return building;
    }
    
    public async Task<Guid> CreateBuildingAsync(Building building, CancellationToken cancellationToken = default)
    {
        var buildingEntry = await _context.Buildings.AddAsync(building, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return buildingEntry.Entity.Id;
    }

    public async Task<Guid> DeleteBuildingAsync(Building building, CancellationToken cancellationToken = default)
    {
        _context.Buildings.Remove(building);

        await _context.SaveChangesAsync(cancellationToken);
        
        return building.Id;
    }

    public async Task<Building> UpdateBuildingAsync(Building building, CancellationToken cancellationToken = default)
    {
        var buildingEntry = _context.Buildings.Update(building);
        
        await _context.SaveChangesAsync(cancellationToken);

        return buildingEntry.Entity;
    }
}