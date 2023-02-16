using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiExample;

namespace WebApiExample.Controllers;

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
        ConnectDB connectDb = new ConnectDB().OpenConnection();
        Student student = StudentCRUD.ReadStudentByID(id);
        connectDb.CloseConnection();
        return new ObjectResult(student);
    }

    [HttpPost]
    public IActionResult CreateNewStudent([Bind("firstName, lastName", "dateOfBirth")] Student student)
    {
        if (student == null)
        {
            return BadRequest();
        }

        int newStudentID = StudentCRUD.CreateStudent(student);
        student.id = newStudentID;

        return CreatedAtRoute("GetStudentByID", new { id = newStudentID }, student);
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateStudent(int id, [FromBody] Student student)
    {
        if (student == null)
        {
            return BadRequest();
        }

        student.id = id;
        StudentCRUD.UpdateStudent(student);

         return CreatedAtRoute("GetStudentByID", new { id = id }, student);
    }
}