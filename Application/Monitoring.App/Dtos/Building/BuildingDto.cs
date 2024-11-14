namespace Monitoring.App.Dtos.Building;

public class BuildingDto
{
    public Guid Id { get; set; }
    public int LocationX { get; set; }
    public int LocationY { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid UserId { get; set; }
}