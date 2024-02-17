using Eclass.Models;
using Eclass.Services;
using Microsoft.AspNetCore.Mvc;

namespace Eclass.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserServices _userService;

    public AuthController(UserServices userServices) => 
    _userService = userServices;

    [HttpGet]
    public async Task<List<Users>> Get() => await _userService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Users>> Get(string id)
    {
        var users = await _userService.GetAsync(id);

        if (users is null)
        {
            return NotFound();
        }

        return users;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Users newUsers)
    {
        await _userService.CreateAsync(newUsers);

        return CreatedAtAction(nameof(Get), new { id = newUsers.Id }, newUsers);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Users updateUsers)
    {
        var Users = await _userService.GetAsync(id);

        if (Users is null)
        {
            return NotFound();
        }
        updateUsers.Id = Users.Id;

        await _userService.UpdateAsync(id, updateUsers);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var users = await _userService.GetAsync(id);

        if (users is null)
        {
            return NotFound();
        }

        await _userService.RemoveAsync(id);
        return NoContent();
    }
}
