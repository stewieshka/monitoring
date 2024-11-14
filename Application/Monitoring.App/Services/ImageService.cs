using Minio.DataModel.Response;
using Monitoring.App.Interfaces.Storages;

namespace Monitoring.App.Services;

public class ImageService
{
    private readonly IImageStorage _imageStorage;

    public ImageService(IImageStorage imageStorage)
    {
        _imageStorage = imageStorage;
    }

    public async Task<PutObjectResponse> UploadImageAsync(string objectName, Stream data)
    {
        var response = await _imageStorage.UploadImageAsync(objectName, data);
        return response;
    }
}