using Monitoring.Domain;

namespace Monitoring.App.Interfaces.Storages;

public interface ISensorRecordStorage
{
    Task<List<SensorRecord>> GetSensorRecordsAsync(Guid id, int pageSize, int pageNumber, DateTime startDate,
        DateTime endDate, CancellationToken cancellationToken = default);
    
    Task<SensorRecord?> GetSensorRecordAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Guid> CreateSensorRecordAsync(SensorRecord sensorRecord, CancellationToken cancellationToken = default);
}
