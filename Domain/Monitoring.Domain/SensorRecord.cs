namespace Monitoring.Domain;

public class SensorRecord : EntityBase
{
    public Sensor Sensor { get; set; }
    public Guid SensorId { get; set; }
    
    public DateTime Date { get; set; }
    
    public int Temperature { get; set; }
    public int Humidity { get; set; }
}