using AutoMapper;
using Monitoring.App.Dtos.SensorRecord;
using Monitoring.App.Interfaces.Storages;
using Monitoring.Domain;

namespace Monitoring.App.Services;

public class SensorRecordService
{
    private readonly ISensorRecordStorage _sensorRecordStorage;
    private readonly ISensorStorage _sensorStorage;
    private readonly INotificationStorage _notificationStorage;
    private readonly IMapper _mapper;

    public SensorRecordService(ISensorRecordStorage sensorRecordStorage, IMapper mapper, ISensorStorage sensorStorage,
        INotificationStorage notificationStorage)
    {
        _sensorRecordStorage = sensorRecordStorage;
        _mapper = mapper;
        _sensorStorage = sensorStorage;
        _notificationStorage = notificationStorage;
    }

    public async Task<List<SensorRecordDto>> GetSensorRecordsAsync(Guid sensorId, int pageSize, int pageNumber,
        DateTime startDate, DateTime endDate,
        CancellationToken cancellationToken = default)
    {
        var sensorRecords =
            await _sensorRecordStorage.GetSensorRecordsAsync(sensorId, pageSize, pageNumber, startDate, endDate, cancellationToken);
        var sensorRecordsDtos = new List<SensorRecordDto>(sensorRecords.Count);

        sensorRecordsDtos.AddRange(sensorRecords.Select(sensorRecord => _mapper.Map<SensorRecordDto>(sensorRecord)));

        return sensorRecordsDtos;
    }

    public async Task<SensorRecordDto?> GetSensorRecordAsync(Guid recordId,
        CancellationToken cancellationToken = default)
    {
        var sensor = await _sensorRecordStorage.GetSensorRecordAsync(recordId, cancellationToken);
        
        return _mapper.Map<SensorRecordDto>(sensor);
    }

    public async Task<Guid> CreateSensorRecordAsync(SensorRecordDto dto,
        CancellationToken cancellationToken = default)
    {
        var sensorRecord = _mapper.Map<SensorRecord>(dto);

        var sensor = await _sensorStorage.GetSensorAsync(sensorRecord.SensorId, cancellationToken);

        sensor.BatteryLevel -= 1;

        await _sensorStorage.UpdateSensorAsync(sensor, cancellationToken);

        if (sensor.BatteryLevel <= 0)
        {
            var notification = new Notification
            {
                CreatedAt = DateTime.UtcNow,
                Audience = "audience",
                Content = "Зарядка кончилась"
            };

            await _notificationStorage.CreateAsync(notification, cancellationToken: cancellationToken);

            throw new Exception("Зарядка кончилась");
        }
        else if (sensor.BatteryLevel <= 10)
        {
            var notification = new Notification
            {
                CreatedAt = DateTime.UtcNow,
                Audience = "audience",
                Content = "Зарядка кончается"
            };

            await _notificationStorage.CreateAsync(notification, cancellationToken: cancellationToken);
        }


        return await _sensorRecordStorage.CreateSensorRecordAsync(sensorRecord, cancellationToken);
    }
}