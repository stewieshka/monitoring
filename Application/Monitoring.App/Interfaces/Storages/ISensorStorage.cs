using Monitoring.Domain;

namespace Monitoring.App.Interfaces.Storages;

public interface ISensorStorage
{
    Task<List<Sensor>> GetSensorsAsync(Guid id, int pageSize, int pageNumber, CancellationToken cancellationToken = default);

    Task<Sensor?> GetSensorAsync(Guid sensorId, CancellationToken cancellationToken = default);

    Task<Guid> CreateSensorAsync(Sensor sensor, CancellationToken cancellationToken = default);

    Task<Guid> DeleteSensorAsync(Sensor sensor, CancellationToken cancellationToken = default);

    Task<Sensor> UpdateSensorAsync(Sensor sensor, CancellationToken cancellationToken = default);
}
