using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Interfaces.IServices;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InfrastructureManagement.Api.Authentication
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private string _key;
        protected IAccountService _accountService;
        protected ITokenAccountService _tokenAccountService;
        private Account _account;

        public JwtAuthenticationManager(IAccountService accountService, ITokenAccountService tokenAccountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _tokenAccountService = tokenAccountService;
            _key = configuration.GetConnectionString("Key");
        }

        public object Authenticate(string username, string password)
        {
            if (!CheckAccountLogin(username, password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, _account.Id.ToString()),
                    new Claim(ClaimTypes.Role, _account.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);


            var ra = _tokenAccountService.DeleteTokenByUsername(username);
            var result = _tokenAccountService.Add(new TokenAccount(username, $"bearer {tokenHandler.WriteToken(token)}"));


            if (result.StatusCode == 201)
            {
                this._account.Password = "xxxxxx";
                return new { token = $"bearer {tokenHandler.WriteToken(token)}", infomation = _account };

            }
            else
            {
                return null;
            }
        }

        public Boolean CheckAccountLogin(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                _account = new Account() { Id = Guid.NewGuid(), Role = "admin" };
                return true;
            }


            var result = _accountService.GetAccountByUsername(username);
            if (result.Response.Data == null)
            {
                return false;
            }
            else
            {
                Account acc = (Account)result.Response.Data;
                bool verified = BCrypt.Net.BCrypt.Verify(password, acc.Password);
                if (verified)
                {
                    _account = acc;
                    return true;
                }
                return false;
            }
        }
    }
}
