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
    public IActionResult CreateNewExamination(
        [Bind("title")] String title, 
        [Bind("dateAndTime")] DateTime dateAndTime,
        [Bind("buildingname")] String buildingName)
    {
        ExaminationWithoutID examination = new ExaminationWithoutID(title, dateAndTime, buildingName);

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
        [Bind("datandtime")] DateTime? dateAndTime = null,
        [Bind("buildingname")] string buildingName = "")
    {
        ExaminationWithoutID examination = new ExaminationWithoutID(title, dateAndTime, buildingName);

        int rowsAffected = ExaminationCRUD.UpdateExamination(id, examination);

        return new ObjectResult($"Rows affected: {rowsAffected}");
    }

    [HttpDelete("{id}", Name = "DeleteExaminationByID")]
    public IActionResult DeleteExaminationByID(int id)
    {
        int rowsAffected = ExaminationCRUD.DeleteExaminationByID(id);
        return new ObjectResult($"Number of rows affected: {rowsAffected}");
    }
}