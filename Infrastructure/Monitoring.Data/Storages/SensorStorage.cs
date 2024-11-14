using Microsoft.EntityFrameworkCore;
using Monitoring.App.Interfaces.Storages;
using Monitoring.Data.Database;
using Monitoring.Domain;

namespace Monitoring.Data.Storages;

public class SensorStorage : ISensorStorage
{
    private readonly MonitoringDbContext _context;

    public SensorStorage(MonitoringDbContext context)
    {
        _context = context;
    }

    public async Task<List<Sensor>> GetSensorsAsync(Guid id, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
    {
        var query = _context.Sensors.AsQueryable();
        
        query = query
            .Where(x => x.BuildingId == id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return await query.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<Sensor?> GetSensorAsync(Guid sensorId, CancellationToken cancellationToken = default)
    {
        var sensor = await _context.Sensors.FirstOrDefaultAsync(x => x.Id == sensorId, cancellationToken: cancellationToken);

        return sensor;
    }
    
    public async Task<Guid> CreateSensorAsync(Sensor sensor, CancellationToken cancellationToken = default)
    {
        var sensorEntry = await _context.Sensors.AddAsync(sensor, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return sensorEntry.Entity.Id;
    }

    public async Task<Guid> DeleteSensorAsync(Sensor sensor, CancellationToken cancellationToken = default)
    {
        _context.Sensors.Remove(sensor);

        await _context.SaveChangesAsync(cancellationToken);
        
        return sensor.Id;
    }

    public async Task<Sensor> UpdateSensorAsync(Sensor sensor, CancellationToken cancellationToken = default)
    {
        var sensorEntry = _context.Sensors.Update(sensor);
        
        await _context.SaveChangesAsync(cancellationToken);

        return sensorEntry.Entity;
    }
}