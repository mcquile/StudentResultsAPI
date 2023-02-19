using Microsoft.AspNetCore.Mvc;
using StudentResultsAPI.CRUD;
using StudentResultsAPI.Models.ModuleExaminations;

namespace StudentResultsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentLecturerModulesController : Controller
{
    [HttpGet(Name = "GetAlStudentLecturerModules")]
    public IActionResult GetAllStudentLecturerModules()
    {
        StudentLecturerModules[] studentLecturerModules = StudentLecturerModulesCRUD.ReadAllStudentLecturerModules();
        return new ObjectResult(studentLecturerModules);
    }

    [HttpGet("{id}", Name = "GetStudentLecturerModulesByID")]
    public IActionResult GetStudentLecturerModulesByID(int id)
    {
        StudentLecturerModules studentLecturerModules = StudentLecturerModulesCRUD.ReadStudentLecturerModulesByID(id);
        return new ObjectResult(studentLecturerModules);
    }

    [HttpPost]
    public IActionResult CreateNewStudentLecturerModules([Bind("studentid", "lecturermodulesid")] StuLecModsWithoutID studentLecturerModules)
    {
        if (studentLecturerModules == null)
        {
            return BadRequest();
        }

        int newStudentLecturerModulesID = StudentLecturerModulesCRUD.CreateStudentLecturerModules(studentLecturerModules);

        return CreatedAtRoute("GetStudentLecturerModulesByID", new { id = newStudentLecturerModulesID }, studentLecturerModules);
    }


    [HttpPut("{id}")]
    public IActionResult UpdateStudentLecturerModules(
        int id,
        [Bind("studentid")] int studentID = -1,
        [Bind("lecturermodulesid")] int lecturerModulesID = -1)
    {
        StuLecModsWithoutID studentLecturerModules = new StuLecModsWithoutID(studentID, lecturerModulesID);

        int rowsAffected = StudentLecturerModulesCRUD.UpdateStudentLecturerModules(id, studentLecturerModules);

        return new ObjectResult($"Rows affected: {rowsAffected}");
    }

    [HttpDelete("{id}", Name = "DeleteStudentLecturerModulesByID")]
    public IActionResult DeletStudentLecturerModulesByID(int id)
    {
        int rowsAffected = StudentLecturerModulesCRUD.DeleteStudentLecturerModulesByID(id);
        return new ObjectResult($"Number of rows affected: {rowsAffected}");
    }
}