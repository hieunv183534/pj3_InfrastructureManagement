using InfrastructureManagement.Api.Authentication;
using InfrastructureManagement.Core.Dtos;
using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfrastructureManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        protected ITokenAccountService _tokenAccountService;
        protected IAccountService _accountService;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public AccountController(IAccountService accountService, IJwtAuthenticationManager jwtAuthenticationManager, ITokenAccountService tokenAccountService)
        {
            _accountService = accountService;
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _tokenAccountService = tokenAccountService;
        }

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] Account account)
        {
            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            var serviceResult = _accountService.Add(account);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Account account)
        {
            var token = _jwtAuthenticationManager.Authenticate(account.Username, account.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(new ResponseModel(1000, "OK", token));
        }

        [HttpPost("logout")]
        public IActionResult Logout([FromHeader] string? Authorization)
        {
            var serviceResult = _tokenAccountService.DeleteTokenByToken(Authorization);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }
    }
}
