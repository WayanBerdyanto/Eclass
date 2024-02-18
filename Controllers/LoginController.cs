using Eclass.Models;
using Eclass.Services;
using Microsoft.AspNetCore.Mvc;

namespace Eclass.Controllers;

[ApiController]
[Route("api/controllers")]
public class LoginController : ControllerBase {
    
    private readonly UserServices _userService;
    IConfiguration configuration;

    public LoginController(UserServices userServices, IConfiguration configuration){
        this.configuration = configuration;
        _userService = userServices;
    }

    // [AllowAnonymous]
    // [HttpPost]
    // public async IActionResult Post([FromBody] Users user){

    //     IActionResult response = Unauthorized();

    // }
}