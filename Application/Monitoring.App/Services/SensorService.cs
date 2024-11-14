using AutoMapper;
using Microsoft.AspNetCore.Http;
using Monitoring.App.Dtos.Sensor;
using Monitoring.App.Interfaces.Storages;
using Monitoring.Domain;

namespace Monitoring.App.Services;

public class SensorService
{
    private readonly ISensorStorage _sensorStorage;
    private readonly IMapper _mapper;
    private readonly IImageStorage _imageStorage;

    public SensorService(ISensorStorage sensorStorage, IMapper mapper, IImageStorage imageStorage)
    {
        _sensorStorage = sensorStorage;
        _mapper = mapper;
        _imageStorage = imageStorage;
    }

    public async Task<List<SensorDto>> GetSensorsAsync(Guid buildingId, int pageSize, int pageNumber,
        CancellationToken cancellationToken = default)
    {
        var sensors = await _sensorStorage.GetSensorsAsync(buildingId, pageSize, pageNumber, cancellationToken);

        var sensorDtos = new List<SensorDto>(sensors.Count);

        sensorDtos.AddRange(sensors.Select(sensor => _mapper.Map<SensorDto>(sensor)));

        return sensorDtos;
    }

    public async Task<SensorDto?> GetSensorDtoAsync(Guid sensorId, CancellationToken cancellationToken = default)
    {
        var sensor = await _sensorStorage.GetSensorAsync(sensorId, cancellationToken);
        return _mapper.Map<SensorDto>(sensor);
    }
    
    public async Task<Sensor?> GetSensorAsync(Guid sensorId, CancellationToken cancellationToken = default)
    {
        return await _sensorStorage.GetSensorAsync(sensorId, cancellationToken);
    }

    public async Task<Guid> CreateSensorAsync(SensorDto dto,
        CancellationToken cancellationToken = default)
    {
        var sensor = _mapper.Map<Sensor>(dto);

        sensor.BatteryLevel = 100;

        sensor.PhotoUrl = $"localhost:9000/nothing";

        return await _sensorStorage.CreateSensorAsync(sensor, cancellationToken);
    }

    public async Task<Guid> DeleteSensorAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sensor = await _sensorStorage.GetSensorAsync(id, cancellationToken);

        return await _sensorStorage.DeleteSensorAsync(sensor, cancellationToken);
    }

    public async Task<Sensor> UpdateSensorAsync(Guid id, SensorDto dto, CancellationToken cancellationToken = default)
    {
        var sensor = _mapper.Map<Sensor>(dto);

        sensor.Id = id;

        return await _sensorStorage.UpdateSensorAsync(sensor, cancellationToken);
    }

    public async Task<Sensor> UpdateSensorAsync(Sensor sensor, CancellationToken cancellationToken = default)
    {
        return await _sensorStorage.UpdateSensorAsync(sensor, cancellationToken);
    }
}