using Microsoft.EntityFrameworkCore;
using Monitoring.App.Interfaces.Storages;
using Monitoring.Data.Database;
using Monitoring.Domain;

namespace Monitoring.Data.Storages;

public class SensorRecordStorage : ISensorRecordStorage
{
    private readonly MonitoringDbContext _context;

    public SensorRecordStorage(MonitoringDbContext context)
    {
        _context = context;
    }

    public async Task<List<SensorRecord>> GetSensorRecordsAsync(Guid id, int pageSize, int pageNumber, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        var query = _context.SensorRecords.AsQueryable();
        
        startDate = startDate.ToUniversalTime();
        endDate = endDate.ToUniversalTime();
        
        query = query
            .Where(x => x.SensorId == id)
            .Where(x => x.Date >= startDate)
            .Where(x => x.Date <= endDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return await query.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<SensorRecord?> GetSensorRecordAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sensorRecord = await _context.SensorRecords.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);

        return sensorRecord;
    }
    
    public async Task<Guid> CreateSensorRecordAsync(SensorRecord sensorRecord, CancellationToken cancellationToken = default)
    {
        var buildingEntry = await _context.SensorRecords.AddAsync(sensorRecord, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return buildingEntry.Entity.Id;
    }
}