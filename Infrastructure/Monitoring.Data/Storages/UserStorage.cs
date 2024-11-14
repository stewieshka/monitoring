using Microsoft.EntityFrameworkCore;
using Monitoring.App.Interfaces.Storages;
using Monitoring.Data.Database;
using Monitoring.Domain;

namespace Monitoring.Data.Storages;

public class UserStorage : IUserStorage
{
    private readonly MonitoringDbContext _context;

    public UserStorage(MonitoringDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> AddAsync(User user, CancellationToken cancellationToken = default)
    {
        var entry = await _context.Users.AddAsync(user, cancellationToken);
        var affectedRows = await _context.SaveChangesAsync(cancellationToken);

        if (affectedRows != 1)
        {
            throw new Exception("Something happened while adding a user");
        }

        return entry.Entity.Id;
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken: cancellationToken);

        return user;
    }
}