using Monitoring.Domain;

namespace Monitoring.App.Interfaces.Storages;

public interface INotificationStorage
{
    Task CreateAsync(Notification notification, CancellationToken cancellationToken = default);
}