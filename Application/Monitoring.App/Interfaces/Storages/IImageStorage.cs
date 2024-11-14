using Minio.DataModel.Response;

namespace Monitoring.App.Interfaces.Storages;

public interface IImageStorage
{
    Task<PutObjectResponse> UploadImageAsync(string objectName, Stream data);
}