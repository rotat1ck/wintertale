using AuthService.Application.DTOs.Requests;
using AuthService.Application.DTOs.Responses;
using AuthService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.WebApi.Controllers {
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : Controller {
        private readonly IAuthService service;

        public AuthController(IAuthService service) {
            this.service = service;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LoginAsync(LoginRequest request) {
            var response = await service.LoginAsync(request);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<LoginResponse>> RegisterAsync(RegisterRequest request) {
            var response = await service.RegisterAsync(request);
            return Ok(response);
        }
    }
}
