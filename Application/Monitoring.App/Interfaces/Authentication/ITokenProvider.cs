using Monitoring.Domain;

namespace Monitoring.App.Interfaces.Authentication;

public interface ITokenProvider
{
    string Generate(User user);
}