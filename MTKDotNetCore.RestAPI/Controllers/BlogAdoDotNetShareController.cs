using Microsoft.AspNetCore.Mvc;
using MTKDotNetCore.RestAPI.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using ClassLibrary1MTKDotNetCore.Shared;
using static ClassLibrary1MTKDotNetCore.Shared.ADOService;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MTKDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetShareController : ControllerBase
    {
        private readonly ADOService _adoService = new ADOService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);

        [HttpGet]
        public IActionResult Get()
        {
            string query = "select * from Tbl_Blog";
            var lst = _adoService.Query<BlogModel>(query);
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            string query = "select * from Tbl_Blog where BlogId = @Blog_Id";
            ADOParameter[] parameters = new ADOParameter[1];
            parameters[0] = new ADOParameter("@Blog_Id", id);
            var item = _adoService.QueryFirstOrDefault<BlogModel>(query, parameters);
            //var item = _adoService.Query<BlogModel>(query, new ADOParameter("@Blog_Id",id));
            if (item is null)
            {
                return NotFound("No data found.");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel model)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                           ([BlogTitle]
                           ,[BlogAuthor]
                           ,[BlogContent])
                     VALUES
                           (@BlogTitle
                           ,@BlogAuthor       
                           ,@BlogContent)";
            int result = _adoService.Execute(query,
                new ADOParameter("@BlogTitle", model.BlogTitle),
                new ADOParameter("@BlogAuthor", model.BlogAuthor),
                new ADOParameter("@BlogContent", model.BlogContent)
                );

            string message = result > 0 ? "Create Successful." : "Create Failed.";
            //return StatusCode(500, message);
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult PutBlogUpdate(int id, BlogModel model)
        {
            string updateQuery = @"UPDATE [dbo].[Tbl_Blog]
                                   SET [BlogTitle] = @BlogTitle,
                                       [BlogAuthor] = @BlogAuthor,
                                       [BlogContent] = @BlogContent
                                   WHERE BlogId = @BlogId";
            model.BlogId = id;
            int result = _adoService.Execute(updateQuery,
                        new ADOParameter("@BlogId", model.BlogId),
                        new ADOParameter("@BlogTitle", model.BlogTitle),
                        new ADOParameter("@BlogAuthor", model.BlogAuthor),
                        new ADOParameter("@BlogContent", model.BlogContent));
            string message = result > 0 ? "Update Blog success" : "Update Blog Fail";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string getQuery = "SELECT COUNT(*) FROM Tbl_Blog  WHERE BlogId = @BlogId";
            int result = _adoService.Execute(getQuery, new ADOParameter("@BlogId",id));
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel model)
        {
            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(model.BlogTitle))
            {
                conditions += "[BlogTitle]=@BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(model.BlogAuthor))
            {
                conditions += "[BlogAuthor]=@BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(model.BlogContent))
            {
                conditions += "[BlogContent]=@BlogContent, ";
            }
            conditions = conditions.Substring(0, conditions.Length - 2);
            if (conditions.Length == 0)
            {
                return NotFound("No Data Found");
            }
            model.BlogId = id;
            string query = $@"UPDATE [dbo].[Tbl_Blog]
                            SET {conditions} 
                            WHERE BlogId=@BlogId";
            int result = _adoService.Execute(query, new ADOParameter("@BlogId", model.BlogId),
                                                         new ADOParameter("@BlogTitle", model.BlogTitle),
                                                         new ADOParameter("@BlogAuthor", model.BlogAuthor),
                                                         new ADOParameter("@BlogContent", model.BlogContent));
            string message = result > 0 ? "Blog update successful" : "Blog update fail";
            return Ok(message);
        }
    }
}