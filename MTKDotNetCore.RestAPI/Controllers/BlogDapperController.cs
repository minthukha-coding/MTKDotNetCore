using Dapper;
using Microsoft.AspNetCore.Mvc;
using MTKDotNetCore.RestAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace MTKDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        [HttpGet]
        public IActionResult Getblogs()
        {
            string query = "select * from tbl_blog";
            using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            List<BlogModel> lst = db.Query<BlogModel>(query).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult Getblog(int id)
        {
            var item = FindByID(id);
            if (item == null)
            {
                return BadRequest("NO Blog");
            }
            return Ok(item);
        }

        private BlogModel? FindByID(int id)
        {
            string query = "select * from tbl_blog where blogid = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            var item = db.Query<BlogModel>(query,new BlogModel { BlogId = id }).FirstOrDefault();
            return item;
        }
    }
}
