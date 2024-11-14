namespace Monitoring.Domain;

public class Notification : EntityBase
{
    public DateTime CreatedAt { get; set; }
    public string Audience { get; set; }
    public string Content { get; set; }
}