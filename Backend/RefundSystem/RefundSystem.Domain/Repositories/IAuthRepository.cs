using RefundSystem.Domain.Dtos;

namespace RefundSystem.Domain.Repositories
{
    public interface IAuthRepository
    {
        Task<bool> Register(string[] roles, string email, string username, string password);  
        Task<string?> Login(string username, string password);  
        Task<UserInfoDto?> GetUserInfo(Guid userId);  
    }
}
