using Eclass.Models;
using Eclass.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Eclass.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
    private readonly UserServices _userService;

    public RegisterController(UserServices userServices) => _userService = userServices;

    [HttpPost]
    public async Task<IActionResult> Post(Users newUsers)
    {
        var keys = Builders<Users>.IndexKeys.Ascending(s => s.Username);
        var options = new CreateIndexOptions { Unique = true };
        var model = new CreateIndexModel<Users>(keys, options);

        if (!IsValidGmailAddress(newUsers.Email))
        {
            throw new ArgumentException("Email must end with @gmail.com");
        }
        if(newUsers.Password.Length < 8){
            return new ObjectResult(
                new{
                    error = true,
                    message = "Password must be at least 8 characters"
                }
            );
        }

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newUsers.Password);

        newUsers.Password = hashedPassword;

        try
        {
            await _userService.CreateAsync(newUsers);
            return new ObjectResult(new { error = false, success = "User created successfully" })
            {
                StatusCode = 201
            };
        }
        catch (MongoWriteException ex)
        {
            if (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                return new ObjectResult(
                    new
                    {
                        error = true,
                        message = "username Sudah Digunakan Silahkan Menggunakan username yang berbeda"
                    }
                )
                {
                    StatusCode = 400
                };
            }
            else
            {
                throw;
            }
        }
    }

    private bool IsValidGmailAddress(string email)
    {
        // Check if the email ends with @gmail.com
        return email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase)
            || email.EndsWith("@si.ukdw.ac.id", StringComparison.OrdinalIgnoreCase);
    }
}
