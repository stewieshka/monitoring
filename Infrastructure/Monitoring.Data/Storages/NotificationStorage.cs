using Monitoring.App.Interfaces.Storages;
using Monitoring.Data.Database;
using Monitoring.Domain;

namespace Monitoring.Data.Storages;

public class NotificationStorage : INotificationStorage
{
    private readonly MonitoringDbContext _context;

    public NotificationStorage(MonitoringDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Notification notification, CancellationToken cancellationToken = default)
    {
        await _context.Notifications.AddAsync(notification, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}