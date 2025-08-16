using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefundSystem.API.Requests.Auth;
using RefundSystem.Application.Dtos;
using RefundSystem.Application.Services.Interfaces;

namespace RefundSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IAuthService authService;

        public AuthController(
            IMapper mapper    ,
            IAuthService authService
        )
        {
            this.mapper = mapper;
            this.authService = authService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterAuthRequest request)
        {
            var registerDto = mapper.Map<RegisterAuthDto>(request);
            var result = await authService.Register(registerDto);
            if(result == true)
            {
                return Ok("Success");
            }
            return BadRequest("Failed to register user");
        }

    }
}
