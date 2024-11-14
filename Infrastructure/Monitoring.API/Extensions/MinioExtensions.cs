using Minio;
using Minio.DataModel.Args;

namespace Monitoring.API.Extensions;

public static class MinioExtensions
{
    public static void ApplyMinioPolicy(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        var minioClient = scope.ServiceProvider
            .GetRequiredService<IMinioClient>();

        var bucketName = app.Configuration["Minio:BucketName"];
        
        var bucketExists = minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
        if (!bucketExists.Result)
        {
            minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName)).Wait();
        }
        
        const string policy = """
                              
                                      {
                                          "Version": "2012-10-17",
                                          "Statement": [
                                              {
                                                  "Effect": "Allow",
                                                  "Principal": { "AWS": [ "*" ] },
                                                  "Action": [ "s3:GetObject" ],
                                                  "Resource": [ "arn:aws:s3:::images/*" ]
                                              }
                                          ]
                                      }
                              """;

        minioClient.SetPolicyAsync(new SetPolicyArgs()
            .WithPolicy(policy)
            .WithBucket(bucketName))
            .Wait();
    }
}