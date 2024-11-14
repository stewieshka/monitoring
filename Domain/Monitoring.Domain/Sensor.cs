namespace Monitoring.Domain;

public class Sensor : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int LocationX { get; set; }
    public int LocationY { get; set; }
    public string PhotoUrl { get; set; }
    
    public Building Building { get; set; }
    public Guid BuildingId { get; set; }
    
    public List<SensorRecord> SensorRecords { get; set; }
    
    public int BatteryLevel { get; set; }
}