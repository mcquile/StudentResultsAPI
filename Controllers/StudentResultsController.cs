using Microsoft.AspNetCore.Mvc;
using StudentResultsAPI.CRUD;
using StudentResultsAPI.Models.StudentResultsModels;

namespace StudentResultsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentResultsController : Controller
{
    [HttpGet(Name = "GetAllStudentResults")]
    public IActionResult GetAllStudentResults()
    {
        StudentResult[] studentResults = StudentResultsCRUD.ReadAllStudentResults();
        return new ObjectResult(studentResults);
    }

    [HttpGet("{id}", Name = "GetStudentResultByID")]
    public IActionResult GetStudentResultByID(int id)
    {
        StudentResult studentResult = StudentResultsCRUD.ReadStudentResultByID(id);
        return new ObjectResult(studentResult);
    }

    [HttpPost]
    public IActionResult CreateNewStudentResult([Bind("studentid", "moduleexaminationsid", "mark")] StudentResultWithoutID studentResult)
    {
        if (studentResult == null)
        {
            return BadRequest();
        }

        int newStudentResultID =StudentResultsCRUD.CreateStudentResult(studentResult);

        return CreatedAtRoute("GetStudentResultByID", new { id = newStudentResultID }, studentResult);
    }


    [HttpPut("{id}")]
    public IActionResult UpdateStudentResult(
        int id,
        [Bind("studentid")] int studentID = -1,
        [Bind("moduleexaminationsid")] int moduleExaminationsID = -1,
        [Bind("mark")] decimal mark = -1)
    {
        StudentResultWithoutID studentResult = new StudentResultWithoutID(studentID, moduleExaminationsID, mark);

        int rowsAffected = StudentResultsCRUD.UpdateStudentResult(id, studentResult);

        return new ObjectResult($"Rows affected: {rowsAffected}");
    }

    [HttpDelete("{id}", Name = "DeleteStudentResultByID")]
    public IActionResult DeleteEWxaminationByID(int id)
    {
        int rowsAffected = StudentResultsCRUD.DeleteStudentResultByID(id);
        return new ObjectResult($"Number of rows affected: {rowsAffected}");
    }
}