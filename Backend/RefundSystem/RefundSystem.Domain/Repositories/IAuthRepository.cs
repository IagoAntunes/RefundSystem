namespace RefundSystem.Domain.Repositories
{
    public interface IAuthRepository
    {
        Task<bool> Register(string[] roles, string email, string username, string password);  
    }
}
