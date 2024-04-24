using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MTKDotNetCore.ConsoleApp.EfCoreExamples;

namespace MTKDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController :ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public BlogController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
