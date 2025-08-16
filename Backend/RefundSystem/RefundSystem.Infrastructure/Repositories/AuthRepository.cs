using Microsoft.AspNetCore.Identity;
using RefundSystem.Domain.Repositories;

namespace RefundSystem.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthRepository(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> Register(string[] roles, string email, string username, string password)
        {
            var identityUser = new IdentityUser
            {
                Email = email,
                UserName = username,
            };
            var identityResult = await userManager.CreateAsync(identityUser, password);
            if (identityResult.Succeeded)
            {
                if (roles != null && roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, roles);
                    if (identityResult.Succeeded)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
