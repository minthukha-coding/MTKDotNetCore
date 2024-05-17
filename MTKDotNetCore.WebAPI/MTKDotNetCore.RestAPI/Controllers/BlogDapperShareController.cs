using Dapper;
using Microsoft.AspNetCore.Mvc;
using MTKDotNetCore.RestAPI.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using ClassLibrary1MTKDotNetCore.Shared;

namespace MTKDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperShareController : ControllerBase
    {
        private readonly DapperService _dapperService = new DapperService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        
        [HttpGet]
        public IActionResult Getblogs()
        {
            string query = "select * from tbl_blog";
            var lst = _dapperService.Query<BlogModel>(query);
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

        [HttpPost]
        public IActionResult CreateBlog(BlogModel model)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                           ([BlogTitle]
                           ,[BlogAuthor]
                           ,[BlogContent])
                     VALUES
                           (@BlogTitle
                           ,@BlogAuthor       
                           ,@BlogContent)";
            int result = _dapperService.Excute(query, model);
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel model)
        {
            var item = FindByID(id);
            if (item == null)
            {
                return BadRequest("NO Blog");
            }
            model.BlogId = id;

            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                         WHERE BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, model);

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel model)
        {
            var item = FindByID(id);
            if (item is null)
            {
                return NotFound("No data found.");
            }

            string conditions = string.Empty;

            if (!string.IsNullOrEmpty(model.BlogTitle))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(model.BlogAuthor))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(model.BlogContent))
            {
                conditions += " [BlogContent] = @BlogContent, ";
            }

            conditions = conditions.Substring(0, conditions.Length - 2);
            model.BlogId = id;

            string query = $@"UPDATE [dbo].[Tbl_Blog]
                           SET {conditions}
                         WHERE BlogId = @BlogId";
            
            int result = _dapperService.Excute(query, model);
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = FindByID(id);
            if (item is null)
            {
                return NotFound("No data found.");
            }

            string query = @"Delete From [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
            int result = _dapperService.Excute(query, new BlogModel { BlogId = id });
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }
        private BlogModel? FindByID(int id)
        {
            string query = "select * from tbl_blog where blogid = @BlogId";
            var item = _dapperService.QueryFirstOrDefault<BlogModel>(query, new BlogModel { BlogId = id });
            return item;
        }
    }   
}
