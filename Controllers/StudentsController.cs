using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentResultsAPI.Models.StudentModels;

namespace StudentResultsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : Controller
{

    private readonly ILogger<StudentController> _logger;

    public StudentController(ILogger<StudentController> logger)
    {
        _logger = logger;
    }

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
    public IActionResult CreateNewStudent([Bind("firstName, lastName", "dateOfBirth")] StudentWithoutID student)
    {
        if (student == null)
        {
            return BadRequest();
        }

        int newStudentID = StudentCRUD.CreateStudent(student);

        return CreatedAtRoute("GetStudentByID", new { id = newStudentID }, student);
    }


    [HttpPatch("{id}")]
    public IActionResult UpdateStudent(int id,
        [Bind("lastName")] string lastName = "",
        [Bind("firstName")] string firstName = "")
    {
        //if (student == null)
        //{
        //    return BadRequest();
        //}

        //student.id = id;
        //StudentCRUD.UpdateStudent(student);

        Console.WriteLine($"firstName: {firstName}\nlastName: {lastName}");

        return CreatedAtRoute("GetStudentByID", new { id = id }, firstName);
    }

    [HttpDelete("{id}", Name = "DeleteStudentByID")]
    public IActionResult DeleteStudentByID(int id)
    {
        try
        {
            StudentCRUD.DeleteStudentByID(id);
            return new ObjectResult($"Successfully deleted student with id: {id}");
        }
        catch (Exception e)
        {
            return new ObjectResult($"Unable to delete student with id: {id}");
        }


    }
}