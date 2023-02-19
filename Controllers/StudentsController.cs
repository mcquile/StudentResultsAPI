using Microsoft.AspNetCore.Mvc;
using StudentResultsAPI.CRUD;
using StudentResultsAPI.Models.Students;

namespace StudentResultsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : Controller
{
    [HttpGet(Name = "GetAllStudents")]
    public IActionResult GetAllStudents()
    {
        Student[] students = StudentCRUD.ReadAllStudents();
        return new ObjectResult(students);
    }

    [HttpGet("{id}", Name = "GetStudentByID")]
    public IActionResult GetStudentByID(int id)
    {
        Student student = StudentCRUD.ReadStudentByID(id);
        return new ObjectResult(student);
    }

    [HttpPost]
    public IActionResult CreateNewStudent([Bind("firstname", "lastname", "dateofbirth")] StudentWithoutID student)
    {
        if (student == null)
        {
            return BadRequest();
        }

        int newStudentID = StudentCRUD.CreateStudent(student);

        return CreatedAtRoute("GetStudentByID", new { id = newStudentID }, student);
    }


    [HttpPut("{id}")]
    public IActionResult UpdateStudent(
        int id,
        [Bind("firstName")] string firstName = "",
        [Bind("lastName")] string lastName = "",
        [Bind("dateOfBirth")] DateTime? dateOfBirth = null)
    {
        StudentWithoutID student = new StudentWithoutID(firstName, lastName, dateOfBirth);

        int rowsAffected = StudentCRUD.UpdateStudent(id, student);

        return new ObjectResult($"Rows affected: {rowsAffected}");
    }

    [HttpDelete("{id}", Name = "DeleteStudentByID")]
    public IActionResult DeleteStudentByID(int id)
    {
        int rowsAffected = StudentCRUD.DeleteStudentByID(id);
        return new ObjectResult($"Number of rows affected: {rowsAffected}");
    }
}