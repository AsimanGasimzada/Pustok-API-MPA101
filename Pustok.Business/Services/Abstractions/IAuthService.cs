namespace Pustok.Business.Services.Abstractions;

public interface IAuthService
{
    Task<ResultDto> RegisterAsync(RegisterDto dto);
    Task<ResultDto<AccessTokenDto>> LoginAsync(LoginDto dto);
}
