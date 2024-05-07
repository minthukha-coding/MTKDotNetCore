using Microsoft.AspNetCore.Mvc;
using MTKDotNetCore.RestAPI.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MTKDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            string query = "select * from Tbl_Blog";
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            connection.Close();

            List<BlogModel> lst = dt.AsEnumerable().Select(dr => new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            }).ToList();

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            string query = "select * from Tbl_Blog where BlogId = @Blog_Id";
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            connection.Close();
            if (dt.Rows.Count == 0)
            {
                return NotFound("No data found.");
            }
            DataRow dr = dt.Rows[0];
            var item = new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            };
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
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", model.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", model.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", model.BlogContent);
            int result = cmd.ExecuteNonQuery();
            connection.Close ();
            string message = result > 0 ? "Create Successful." : "Create Failed.";
            //return StatusCode(500, message);
            return Ok(message);
        }

        [HttpPut]
        public IActionResult PutBlogUpdate(int id, BlogModel model)
        {
            string getQuery = "SELECT COUNT(*) FROM Tbl_Blog  WHERE BlogId = @BlogId";
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand checkBlogCMD = new SqlCommand(getQuery, connection);
            checkBlogCMD.Parameters.AddWithValue("@BlogId", id);
            var count = (int)checkBlogCMD.ExecuteScalar();
            if (count == 0) return NotFound("No Blog");
            
            string updateQuery = @"UPDATE [dbo].[Tbl_Blog]
                                   SET [BlogTitle] = @BlogTitle,
                                       [BlogAuthor] = @BlogAuthor,
                                       [BlogContent] = @BlogContent
                                   WHERE BlogId = @BlogId";
            
            SqlCommand cmd = new SqlCommand(updateQuery, connection);
            
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", model.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", model.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", model.BlogContent);
            int result = cmd.ExecuteNonQuery();
            
            connection.Close();
            string message = result > 0 ? "Update Blog success" : "Update Blog Fail";
            return Ok(message);
        }
    }
}