using Microsoft.AspNetCore.Mvc;
using StudentResultsAPI.CRUD;
using StudentResultsAPI.Models.LecturerModels;

namespace StudentResultsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LecturersController : Controller
{
    [HttpGet(Name = "GetAllLecturers")]
    public IActionResult GetAllLecturers()
    {
        Lecturer[] lecturers = LecturerCRUD.ReadAllLecturers();
        return new ObjectResult(lecturers);
    }

    [HttpGet("{id}", Name = "GetLecturerByID")]
    public IActionResult GetLecturerByID(int id)
    {
        Lecturer lecturer = LecturerCRUD.ReadLecturerByID(id);
        return new ObjectResult(lecturer);
    }

    [HttpPost]
    public IActionResult CreateNewLecturer([Bind("firstname, lastname")] LecturerWithoutID lecturer)
    {
        if (lecturer == null)
        {
            return BadRequest();
        }

        int newLecturerID = LecturerCRUD.CreateLecturer(lecturer);

        return CreatedAtRoute("GetLecturerByID", new { id = newLecturerID }, lecturer);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateLecturer(
        int id,
        [Bind("firstname")] string firstName = "",
        [Bind("lastname")] string lastName = "")
    {
        LecturerWithoutID lecturer = new LecturerWithoutID(firstName, lastName);

        int rowsAffected = LecturerCRUD.UpdateLecturer(id, lecturer);

        return new ObjectResult($"Rows affected: {rowsAffected}");
    }

    [HttpDelete("{id}", Name = "DeleteLecturerByID")]
    public IActionResult DeleteLecturerByID(int id)
    {
        int rowsAffected = LecturerCRUD.DeleteLecturertByID(id);
        return new ObjectResult($"Number of rows affected: {rowsAffected}");
    }
}