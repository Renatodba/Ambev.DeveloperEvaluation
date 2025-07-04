using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Dtos;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Controllers
{
    public class SaleController : Controller
    {
        [ApiController]
        [Route("api/[controller]")]
        public class SalesController : ControllerBase
        {
            private readonly ISaleService _saleService;

            public SalesController(ISaleService saleService)
            {
                _saleService = saleService;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<SaleDto>>> GetAll()
            {
                var sales = await _saleService.GetAllAsync();
                return Ok(sales);
            }

            [HttpGet("{id:guid}")]
            public async Task<ActionResult<SaleDto>> GetById(Guid id)
            {
                var sale = await _saleService.GetByIdAsync(id);
                if (sale == null)
                    return NotFound();
                return Ok(sale);
            }

            [HttpPost]
            public async Task<ActionResult<SaleDto>> Create([FromBody] CreateSaleDto dto)
            {
                var created = await _saleService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }

            [HttpPut("{id:guid}")]
            public async Task<ActionResult<SaleDto>> Update(Guid id, [FromBody] UpdateSaleDto dto)
            {
                var updated = await _saleService.UpdateAsync(id, dto);
                if (updated == null)
                    return NotFound();
                return Ok(updated);
            }

            [HttpDelete("{id:guid}")]
            public async Task<IActionResult> Delete(Guid id)
            {
                var exists = await _saleService.GetByIdAsync(id);
                if (exists == null)
                    return NotFound();

                await _saleService.DeleteAsync(id);
                return NoContent();
            }
        }
    }
}
