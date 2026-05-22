using Business.Interfaces.Scenario;
using Domain.Scenario;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ScenarioControllers;

[ApiController]
[Route("api/[controller]")]
public class ScenarioController : ControllerBase
{
    
    private readonly IScenarioService<ScenarioModel> _service;

    public ScenarioController(IScenarioService<ScenarioModel> service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Add(ScenarioModel scenarioModel)
    {
        var result = await _service.AddAsync(scenarioModel);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _service.DeleteAsync(id);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        var result = await _service.DeleteAllAsync();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);  
    }

    [HttpPut]
    public async Task<IActionResult> Update(ScenarioModel scenarioModel)
    {
        var result = await _service.UpdateAsync(scenarioModel);
        return Ok(result);
    }


    [HttpGet("GetByName/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var result = await _service.GetByNameAsync(name);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

}
