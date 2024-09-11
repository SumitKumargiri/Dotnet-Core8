using crudoperation.Interface;
using crudoperation.Model;
using crudoperation.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crudoperation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILogin _loginService;
        public LoginController(ILogin loginService)
        {
            _loginService = loginService;
        }
        [HttpPost("logindata")]
        public async Task<IActionResult> LoginData(Login login)
        {
            var result = await _loginService.LoginAsync(login);
            return Ok(result);
        }


        [HttpPost("registerdata")]
        public async Task<IActionResult> RegisterData(Register register)
        {
            var result = await _loginService.RegisterAsync(register);
            return Ok(result);
        }
        [HttpPost("removechachedata")]
        public async Task<IActionResult> Removedata(string Username)
        {
            await _loginService.RemoveSession(Username);
            return Ok(new { Success = true, Message = "Logged out successfully" });
        }
        [HttpPost("clearchachedata")]
        public async Task<IActionResult>ClearChache(string key)
        {
            var result = await _loginService.clearchache(key);
            return Ok(result);
        }
    }
    
}
