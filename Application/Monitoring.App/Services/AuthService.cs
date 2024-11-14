using AutoMapper;
using ErrorOr;
using Monitoring.App.Dtos;
using Monitoring.App.Dtos.Auth;
using Monitoring.App.ErrorDefinitions;
using Monitoring.App.Interfaces.Authentication;
using Monitoring.App.Interfaces.Storages;
using Monitoring.Domain;

namespace Monitoring.App.Services;

public class AuthService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUserStorage _userStorage;
    private readonly IMapper _mapper;

    public AuthService(IPasswordHasher passwordHasher, ITokenProvider tokenProvider, IUserStorage userStorage, IMapper mapper)
    {
        _passwordHasher = passwordHasher;
        _tokenProvider = tokenProvider;
        _userStorage = userStorage;
        _mapper = mapper;
    }

    public async Task<ErrorOr<AuthDto>> RegisterAsync(RegisterUserDto dto, CancellationToken cancellationToken = default)
    {
        var existingUser = await _userStorage.GetByEmailAsync(dto.Email, cancellationToken);

        if (existingUser is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        var user = _mapper.Map<User>(dto);
        
        _passwordHasher.Hash(dto.Password, out var salt, out var hash);

        user.PasswordHash = Convert.ToBase64String(hash);
        user.PasswordSalt = Convert.ToBase64String(salt);

        var id = await _userStorage.AddAsync(user, cancellationToken);

        var token = _tokenProvider.Generate(user);

        return new AuthDto
        {
            Id = id,
            Token = token
        };
    }

    public async Task<ErrorOr<AuthDto>> LoginAsync(LoginUserDto dto, CancellationToken cancellationToken = default)
    {
        var user = await _userStorage.GetByEmailAsync(dto.Email, cancellationToken);

        if (user is null)
        {
            return Errors.User.UserNotFound;
        }

        var passwordHash = Convert.FromBase64String(user.PasswordHash);

        var passwordSalt = Convert.FromBase64String(user.PasswordSalt);
        
        var passwordVerificationResult = _passwordHasher.Verify(dto.Password, passwordSalt, passwordHash);

        if (!passwordVerificationResult)
        {
            return Errors.User.WrongPassword;
        }

        var token = _tokenProvider.Generate(user);

        return new AuthDto
        {
            Id = user.Id,
            Token = token
        };
    }
}