using RefundSystem.Application.Dtos;
using RefundSystem.Domain.Dtos;

namespace RefundSystem.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Register(RegisterAuthDto register);
        Task<string?> Login(LoginAuthDto login);
        Task<UserInfoDto?> GetUserInfo(Guid userId);
    }
}
