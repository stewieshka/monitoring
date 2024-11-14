using Microsoft.AspNetCore.Http;

namespace Monitoring.App.Dtos.Sensor;

public class SensorDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int LocationX { get; set; }
    public int LocationY { get; set; }
    
    public string PhotoUrl { get; set; }

    public Guid BuildingId { get; set; }
}