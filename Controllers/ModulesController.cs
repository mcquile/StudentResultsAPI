using Microsoft.AspNetCore.Mvc;
using StudentResultsAPI.CRUD;
using StudentResultsAPI.Models.Modules;

namespace StudentResultsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ModulesController : Controller
{
    [HttpGet(Name = "GetAllModules")]
    public IActionResult GetAllModules()
    {
        Module[] modules = ModuleCRUD.ReadAllModules();
        return new ObjectResult(modules);
    }

    [HttpGet("{id}", Name = "GetModuleByID")]
    public IActionResult GetModuleByID(int id)
    {
        Module module = ModuleCRUD.ReadModuleByID(id);
        return new ObjectResult(module);
    }

    [HttpPost]
    public IActionResult CreateNewModule([Bind("modulename")] ModuleWithoutID module)
    {
        if (module == null)
        {
            return BadRequest();
        }

        int moduleID = ModuleCRUD.CreateModule(module);

        return CreatedAtRoute("GetModuleByID", new { id = moduleID }, module);
    }


    [HttpPut("{id}")]
    public IActionResult UpdateModule(
        int id,
        [Bind("modulename")] string moduleName = "")
    {
        ModuleWithoutID module = new ModuleWithoutID(moduleName);

        int rowsAffected = ModuleCRUD.UpdateModule(id, module);

        return new ObjectResult($"Rows affected: {rowsAffected}");
    }

    [HttpDelete("{id}", Name = "DeleteModuleByID")]
    public IActionResult DeleteModuleByID(int id)
    {
        int rowsAffected = ModuleCRUD.DeleteModuleByID(id);
        return new ObjectResult($"Number of rows affected: {rowsAffected}");
    }
}