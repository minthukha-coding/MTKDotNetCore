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
    }
}
