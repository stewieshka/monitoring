namespace Monitoring.Domain;

public class User : EntityBase
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    
    public List<Building> Buildings { get; set; }
}