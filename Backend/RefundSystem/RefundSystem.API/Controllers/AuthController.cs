using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefundSystem.API.Requests.Auth;
using RefundSystem.API.Responses;
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

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginAuthRequest request )
        {
            var loginAuthDto = mapper.Map<LoginAuthDto>(request);
            var result = await authService.Login(loginAuthDto);
            if (result != null)
            {
                var response = new LoginResponseDto
                {
                    Token = result,
                };
                return Ok(response);
            }
            return BadRequest("Invalid username or password");
        }

    }
}
