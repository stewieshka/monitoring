namespace Monitoring.App.Dtos.SensorRecord;

public class SensorRecordDto
{
    public Guid Id { get; set; }
    public Guid SensorId { get; set; }
    
    public DateTime Date { get; set; }
    
    public int Temperature { get; set; }
    public int Humidity { get; set; }
}