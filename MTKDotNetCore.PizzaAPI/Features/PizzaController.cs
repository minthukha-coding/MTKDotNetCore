using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MTKDotNetCore.PizzaAPI.Database;

namespace MTKDotNetCore.PizzaAPI.Features
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public PizzaController()
        {
            _appDbContext = new AppDbContext();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var lst = await _appDbContext.Pizzas.ToListAsync();
            return Ok(lst);
        } 

        [HttpGet("Extras")]
        public async Task<IActionResult> GetExtras()
        {
            var lst = await _appDbContext.PizzaExtrass.ToListAsync();
            return Ok(lst); 
        }

        [HttpPost("Order")]
        public async Task<IActionResult> Order(OrderRequest reqModel)
        {
            return Ok(reqModel);
        }
    }
}
