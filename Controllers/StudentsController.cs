using Eclass.Models;
using Eclass.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eclass.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsContoller : ControllerBase
{
    private readonly StudentsServices _studentsService;

    public StudentsContoller(StudentsServices studentsService) =>
        _studentsService = studentsService;

    [HttpGet]
    [Authorize]
    public async Task<List<Students>> Get() => await _studentsService.GetAsync();

    [HttpGet("{id:length(24)}")]
    [Authorize]
    public async Task<ActionResult<Students>> Get(string id)
    {
        var student = await _studentsService.GetAsync(id);

        if (student is null)
        {
            return NotFound();
        }

        return student;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post(Students newStudents)
    {
        await _studentsService.CreateAsync(newStudents);

        return CreatedAtAction(nameof(Get), new { id = newStudents.Id }, newStudents);
    }

    [HttpPut("{id:length(24)}")]
    [Authorize]
    public async Task<IActionResult> Update(string id, [FromBody] Students updateStudents)
    {
        var students = await _studentsService.GetAsync(id);

        if (students is null)
        {
            return new ObjectResult(new { error = true, message = "Id Tidak Ditemukan" })
            {
                StatusCode = 404
            };
        }
        updateStudents.Id = students.Id;
        await _studentsService.UpdateAsync(id, updateStudents);

        return new ObjectResult(new { success = true, message = "Update successfully", })
        {
            StatusCode = 200
        };
    }

    [HttpDelete("{id:length(24)}")]
    [Authorize]
    public async Task<IActionResult> Delete(string id)
    {
        var students = await _studentsService.GetAsync(id);

        if (students is null)
        {
            return new ObjectResult(new { error = true, message = "Id Tidak Ditemukan" })
            {
                StatusCode = 404
            };
        }

        await _studentsService.RemoveAsync(id);
        return new ObjectResult(new { success = true, message = "Delete successfully", })
        {
            StatusCode = 200
        };
    }
}
