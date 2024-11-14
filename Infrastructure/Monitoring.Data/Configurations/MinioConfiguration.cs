namespace Monitoring.Data.Configurations;

public class MinioConfiguration
{
    public const string SectionName = "Minio";
    
    public string Login { get; set; }
    public string Password { get; set; }
    public string Endpoint { get; set; }
    public bool UseSsl { get; set; }
    public string BucketName { get; set; }
}