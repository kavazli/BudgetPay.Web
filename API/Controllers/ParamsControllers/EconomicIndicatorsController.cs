using Business.Interfaces.Params;
using Domain.TaxParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ParamsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EconomicIndicatorsController : ControllerBase
    {
        
        private readonly IParamsService<EconomicIndicators> _service;

        public EconomicIndicatorsController(IParamsService<EconomicIndicators> service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Add(EconomicIndicators entity)
        {
            var result = await _service.AddAsync(entity);
            return CreatedAtAction(nameof(GetByYear), new { year = result.Year }, result);
        }

        [HttpGet("{year}")]
        public async Task<IActionResult> GetByYear(int year)
        {
            var result = await _service.GetByYearAsync(year);
            return Ok(result);
        }

        [HttpGet("list/{year}")]
        public async Task<IActionResult> GetByListYear(int year)
        {
            var result = await _service.GetByListYearAsync(year);
            return Ok(result);
        }


         [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, EconomicIndicators entity)
        {
            var result = await _service.UpdateAsync(id, entity);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(result);
        }
       
    }
}
