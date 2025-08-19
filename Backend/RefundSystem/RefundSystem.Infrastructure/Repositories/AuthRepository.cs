using Microsoft.AspNetCore.Identity;
using RefundSystem.Domain.Dtos;
using RefundSystem.Domain.Repositories;

namespace RefundSystem.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthRepository(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        public async Task<UserInfoDto?> GetUserInfo(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if(user == null)
            {
                return null;
            }
            var userInfo = new UserInfoDto
            {
                Email = user.Email,
                Name = user.UserName
            };
            return userInfo;
        }

        public async Task<string?> Login(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var passwordIsCorrect = await userManager.CheckPasswordAsync(user, password);
                if (passwordIsCorrect)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user.UserName,user.Email, user.Id, roles.ToList());
                        //var response = new LoginResponseDto
                        //{
                        //    JwtToken = jwtToken,
                        //};
                        return jwtToken;
                    }
                }
            }
            return null;
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
