namespace Monitoring.Domain;

public class Building : EntityBase
{
    public int LocationX { get; set; }
    public int LocationY { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public List<Sensor> Sensors { get; set; }
    
    public User User { get; set; }
    public Guid UserId { get; set; }
}