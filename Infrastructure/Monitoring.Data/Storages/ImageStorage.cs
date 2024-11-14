using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.DataModel.Response;
using Monitoring.App.Interfaces.Storages;
using Monitoring.Data.Configurations;
using Monitoring.App.Interfaces;

namespace Monitoring.Data.Storages;

public class ImageStorage : IImageStorage
{
    private readonly IMinioClient _minioClient;
    private readonly MinioConfiguration _minioConfiguration;

    public ImageStorage(IMinioClient minioClient, IOptions<MinioConfiguration> minioConfiguration)
    {
        _minioClient = minioClient;
        _minioConfiguration = minioConfiguration.Value;
    }
    
    public async Task<PutObjectResponse> UploadImageAsync(string objectName, Stream data)
    {
        var bucketExists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_minioConfiguration.BucketName));
        if (!bucketExists)
        {
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_minioConfiguration.BucketName));
        }
        
        var response = await _minioClient.PutObjectAsync(new PutObjectArgs()
            .WithBucket(_minioConfiguration.BucketName)
            .WithObject(objectName)
            .WithStreamData(data)
            .WithObjectSize(data.Length)
            .WithContentType("image/jpeg"));

        return response;
    }
}