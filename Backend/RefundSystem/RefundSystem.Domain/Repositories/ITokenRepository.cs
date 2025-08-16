using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefundSystem.Domain.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(string username, string email, string userId, List<string> roles);
    }
}
