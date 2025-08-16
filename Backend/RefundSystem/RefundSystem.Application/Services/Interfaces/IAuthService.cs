using RefundSystem.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefundSystem.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Register(RegisterAuthDto register);
        Task<string?> Login(LoginAuthDto login);
    }
}
