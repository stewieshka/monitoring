using Monitoring.Domain;

namespace Monitoring.App.Interfaces.Storages;

public interface IUserStorage
{
    Task<Guid> AddAsync(User user, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}