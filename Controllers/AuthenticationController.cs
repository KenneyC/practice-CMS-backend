using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System;
using System.Text;
using System.Threading.Tasks;
using practice_CMS_backend.Authentication;
using practice_CMS_backend.CMS;

namespace practice_CMS_backend.Controllers
{
    public class AuthenticationControllers : ControllerBase
    {

        private IUserDataAccessProvider _userDataAccessProvider;
        private readonly IConfiguration _configuration;

        public AuthenticationControllers(IUserDataAccessProvider accessProvider, IConfiguration configuration)
        {
            _userDataAccessProvider = accessProvider;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            bool loginSuccess = await _userDataAccessProvider.Login(login);

            if (loginSuccess)
            {
                List<Claim> authClaims = new List<Claim> {
                    new Claim(ClaimTypes.Name, login.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                SymmetricSecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: "Someone",
                    audience: "Everyone",
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized(new Response
            {
                Status = "Error",
                Message = "Incorrect username or password"
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel register)
        {
            Response registerStatus = await _userDataAccessProvider.Register(register);

            if (registerStatus.Status == "Error")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, registerStatus);
            }
            else
            {
                return Ok(registerStatus);
            }

        }
    }
}