using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Monitoring.App.Interfaces.Authentication;
using Monitoring.Data.Configurations;
using Monitoring.Domain;

namespace Monitoring.Data.Authentication;

public class TokenProvider(IOptions<JwtConfiguration> options) : ITokenProvider
{
    private readonly JwtConfiguration _configuration = options.Value;
    
    public string Generate(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(_configuration.ExpiryMinutes),
            SigningCredentials = credentials,
            Issuer = _configuration.Issuer,
            Audience = _configuration.Audience
        };

        var handler = new JsonWebTokenHandler();

        var token = handler.CreateToken(tokenDescriptor);

        return token;
    }
}