using Microsoft.AspNetCore.Http.HttpResults;
using RefundSystem.Application.Dtos;
using RefundSystem.Application.Services.Interfaces;
using RefundSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefundSystem.Application.Services.Implementations
{
    internal class AuthService : IAuthService
    {
        private readonly IAuthRepository authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        public async Task<bool> Register(RegisterAuthDto register)
        {
            var result = await authRepository.Register(register.Roles,register.Email, register.Username, register.Password);
            return result;
        }
    }
}
