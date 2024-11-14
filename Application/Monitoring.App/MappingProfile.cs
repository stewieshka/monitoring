using AutoMapper;
using Monitoring.App.Dtos;
using Monitoring.App.Dtos.Auth;
using Monitoring.App.Dtos.Building;
using Monitoring.App.Dtos.Sensor;
using Monitoring.App.Dtos.SensorRecord;
using Monitoring.Domain;

namespace Monitoring.App;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterUserDto, User>();

        CreateMap<BuildingDto, Building>().ReverseMap();

        CreateMap<SensorDto, Sensor>().ReverseMap();

        CreateMap<SensorRecordDto, SensorRecord>().ReverseMap();
    }
}