using AddressBook.BLL.DTOs.Auth;
using AddressBook.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {

            var result = await _authService.LoginAsync(dto);
            return Ok(result);
        }
    }
}