using Eclass.Models;
using Eclass.Services;
using Microsoft.AspNetCore.Mvc;

namespace Eclass.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsContoller : ControllerBase
{
    private readonly StudentsServices _studentsService;

    public StudentsContoller(StudentsServices studentsService) =>
        _studentsService = studentsService;

    [HttpGet]
    public async Task<List<Students>> Get() => await _studentsService.GetAsync();

    [HttpGet("{id:length(24)}")]
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
    public async Task<IActionResult> Post(Students newStudents)
    {
        await _studentsService.CreateAsync(newStudents);

        return CreatedAtAction(nameof(Get), new { id = newStudents.Id }, newStudents);
    }
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Students updateStudents){

        var students = await _studentsService.GetAsync(id);

        if(students is null){
            return NotFound();
        }
        updateStudents.Id = students.Id;

        await _studentsService.UpdateAsync(id, updateStudents);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id){
        var students = await _studentsService.GetAsync(id);

        if(students is null){
            return NotFound();
        }

        await _studentsService.RemoveAsync(id);
        return NoContent();
    }
}
