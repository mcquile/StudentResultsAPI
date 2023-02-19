using Microsoft.AspNetCore.Mvc;
using StudentResultsAPI.CRUD;
using StudentResultsAPI.Models.LecturerModules;

namespace StudentResultsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LectureModulesController : Controller
{
    [HttpGet(Name = "GetAllLecturerModules")]
    public IActionResult GetAllLectureModules()
    {
        LecturerModule[] modules = LecturerModuleCRUD.ReadAllLecturerModules();
        return new ObjectResult(modules);
    }

    [HttpGet("{id}", Name = "GetLecturerModuleByID")]
    public IActionResult GetLecturerModuleByID(int id)
    {
        LecturerModule lecturerModule = LecturerModuleCRUD.ReadLecturerModuleByID(id);
        return new ObjectResult(lecturerModule);
    }

    [HttpPost]
    public IActionResult CreateNewLecturerModule([Bind("lecturerid", "moduleid", "activestatus")] LecturerModuleWithoutID lecturerModule)
    {
        if (lecturerModule == null)
        {
            return BadRequest();
        }

        int lecturerModuleID = LecturerModuleCRUD.CreateLecturerModule(lecturerModule);

        return CreatedAtRoute("GetModuleByID", new { id = lecturerModuleID }, lecturerModule);
    }


    [HttpPut("{id}")]
    public IActionResult UpdateModule(
        int id,
        [Bind("lecturerid")] int lecturerID = -1,
        [Bind("moduleid")] int moduleID = -1)
    {
        LecturerModuleWithoutID lecturerModule = new LecturerModuleWithoutID(lecturerID, moduleID);

        int rowsAffected = LecturerModuleCRUD.UpdateLecturerModule(id, lecturerModule);

        return new ObjectResult($"Rows affected: {rowsAffected}");
    }

    [HttpDelete("{id}", Name = "DeleteLecturerModuleByID")]
    public IActionResult DeleteLecturerModuleByID(int id)
    {
        int rowsAffected = LecturerModuleCRUD.DeleteLecturerModuleByID(id);
        return new ObjectResult($"Number of rows affected: {rowsAffected}");
    }
}