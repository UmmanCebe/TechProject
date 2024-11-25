using Core.Security.Dtos;
using Core.Security.JWT;

namespace TechCareer.Service.Abstracts;

public interface IAuthService
{
    Task<AccessToken> LoginAsync(UserForLoginDto dto,CancellationToken cancellationToken);
    Task<AccessToken> RegisterAsync(UserForRegisterDto dto,CancellationToken cancellationToken);
}