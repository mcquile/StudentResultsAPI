using Microsoft.AspNetCore.Mvc;
using StudentResultsAPI.CRUD;
using StudentResultsAPI.Models.ExaminationModels;

namespace StudentResultsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ExaminationsController : Controller
{
    [HttpGet(Name = "GetAllExaminations")]
    public IActionResult GetAllExaminations()
    {
        Examination[] examinations = ExaminationCRUD.ReadAllExaminations();
        return new ObjectResult(examinations);
    }

    [HttpGet("{id}", Name = "GetExaminationByID")]
    public IActionResult GetExaminationByID(int id)
    {
        Examination examination = ExaminationCRUD.ReadExaminationByID(id);
        return new ObjectResult(examination);
    }

    [HttpPost]
    public IActionResult CreateNewExamination([Bind("title", "dateandtime", "buildingname")] ExaminationWithoutID examination)
    {
        if (examination == null)
        {
            return BadRequest();
        }

        int newExaminationID =ExaminationCRUD.CreateExamination(examination);

        return CreatedAtRoute("GetExaminationByID", new { id = newExaminationID }, examination);
    }


    [HttpPut("{id}")]
    public IActionResult UpdateExamination(
        int id,
        [Bind("title")] string title = "",
        [Bind("buildingname")] string buildingName = "",
        [Bind("datandtime")] DateTime? dateAndTime = null)
    {
        ExaminationWithoutID examination = new ExaminationWithoutID(title, dateAndTime, buildingName);

        int rowsAffected = ExaminationCRUD.UpdateExamination(id, examination);

        return new ObjectResult($"Rows affected: {rowsAffected}");
    }

    [HttpDelete("{id}", Name = "DeleteExaminationByID")]
    public IActionResult DeleteEWxaminationByID(int id)
    {
        int rowsAffected = ExaminationCRUD.DeleteExaminationByID(id);
        return new ObjectResult($"Number of rows affected: {rowsAffected}");
    }
}