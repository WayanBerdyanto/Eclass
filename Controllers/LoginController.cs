using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Eclass.Models;
using Eclass.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Eclass.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly LoginServices _userService;
    IConfiguration configuration;

    public LoginController(IConfiguration configuration, LoginServices userServices)
    {
        this.configuration = configuration;
        _userService = userServices;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> AuthLogin([FromBody] Users users)
    {
        IActionResult response = Unauthorized();

        var registeredUser = await _userService.AuthLogin(users.Username);

        if (users != null)
        {
            if (
                users.Username.Equals(registeredUser.Username)
                && BCrypt.Net.BCrypt.Verify(users.Password, registeredUser.Password)
            )
            {
                var issuer = configuration["Jwt:Issuer"];
                var audience = configuration["Jwt:Audience"];
                var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
                var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature
                );

                var subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, users.Username),
                        new Claim(JwtRegisteredClaimNames.Email, users.Password),
                    }
                );

                var expires = DateTime.UtcNow.AddMinutes(10);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = subject,
                    Expires = expires,
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = signingCredentials
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                return new ObjectResult(
                    new
                    {
                        success = true,
                        message = "Login successfully",
                        token = jwtToken
                    }
                )
                {
                    StatusCode = 201
                };
            }
            return new ObjectResult(new { error = true, message = "Login Gagal" })
            {
                StatusCode = 400
            };
        }
        return new ObjectResult(new { error = true, message = "data kosong" }) { StatusCode = 400 };
    }
}
