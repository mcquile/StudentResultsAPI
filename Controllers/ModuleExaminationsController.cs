using Microsoft.AspNetCore.Mvc;
using StudentResultsAPI.CRUD;
using StudentResultsAPI.Models.ExaminationModels;
using StudentResultsAPI.Models.ModuleExaminationsModels;

namespace StudentResultsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ModuleExaminationsController : Controller
{
    [HttpGet(Name = "GetAllModuleExaminations")]
    public IActionResult GetAllModuleExaminations()
    {
        ModuleExamination[] moduleExaminations = ModuleExaminationCRUD.ReadAllModuleExamination();
        return new ObjectResult(moduleExaminations);
    }

    [HttpGet("{id}", Name = "GetModuleExaminationByID")]
    public IActionResult GetModuleExaminationByID(int id)
    {
        ModuleExamination moduleExamination = ModuleExaminationCRUD.ReadModuleExaminationByID(id);
        return new ObjectResult(moduleExamination);
    }

    [HttpPost]
    public IActionResult CreateNewModuleExamination([Bind("moduleid", "examid")] ModuleExaminationWithoutID moduleExamination)
    {
        if (moduleExamination == null)
        {
            return BadRequest();
        }

        int newModuleExaminationID = ModuleExaminationCRUD.CreateModuleExamination(moduleExamination);

        return CreatedAtRoute("GetModuleExaminationByID", new { id = newModuleExaminationID }, moduleExamination);
    }


    [HttpPut("{id}")]
    public IActionResult UpdateModuleExamination(
        int id,
        [Bind("moduleid")] int moduleID = -1,
        [Bind("examinationid")] int examinationID = -1)
    {
        ModuleExaminationWithoutID examination = new ModuleExaminationWithoutID(moduleID, examinationID);

        int rowsAffected = ModuleExaminationCRUD.UpdateModuleExamination(id, examination);

        return new ObjectResult($"Rows affected: {rowsAffected}");
    }

    [HttpDelete("{id}", Name = "DeleteModuleExaminationByID")]
    public IActionResult DeleteModuleExaminationByID(int id)
    {
        int rowsAffected = ModuleExaminationCRUD.DeleteModuleExaminationByID(id);
        return new ObjectResult($"Number of rows affected: {rowsAffected}");
    }
}