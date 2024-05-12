using Microsoft.AspNetCore.Mvc;
using MTKDotNetCore.RestAPI.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using ClassLibrary1MTKDotNetCore.Shared;
using static ClassLibrary1MTKDotNetCore.Shared.ADOService;

namespace MTKDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetShareController : ControllerBase
    {
        private readonly ADOService _adoService = new ADOService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);

        [HttpGet]
        //public IActionResult Get()
        //{
        //    string query = "select * from Tbl_Blog";
        //    var lst = _adoService.Query<BlogModel>(query);
        //    return Ok(lst);
        //}

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
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", model.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", model.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", model.BlogContent);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Create Successful." : "Create Failed.";
            //return StatusCode(500, message);
            return Ok(message);
        }

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string getQuery = "SELECT COUNT(*) FROM Tbl_Blog  WHERE BlogId = @BlogId";
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand checkBlogCMD = new SqlCommand(getQuery, connection);
            checkBlogCMD.Parameters.AddWithValue("@BlogId", id);
            var count = (int)checkBlogCMD.ExecuteScalar();
            if (count == 0) return NotFound("No Blog");

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                               WHERE BlogId = @BlogId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BlogId", id);

            int result = command.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel model)
        {
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = "select * from Tbl_Blog where BlogId = @BlogId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BlogId", id);
            var count = (int)command.ExecuteScalar();
            if (count == 0) return NotFound();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            List<BlogModel> lst = new List<BlogModel>();
            if (dt.Rows.Count == 0)
            {
                var respnose = new { IsSuccess = false, Message = "No data not found" };
                return NotFound(respnose);
            }
            DataRow row = dt.Rows[0];
            BlogModel item = new BlogModel
            {
                BlogId = Convert.ToInt32(row["BlogId"]),
                BlogTitle = Convert.ToString(row["BlogTitle"]),
                BlogAuthor = Convert.ToString(row["BlogAuthor"]),
                BlogContent = Convert.ToString(row["BlogContent"]),
            };
            lst.Add(item);
            string conditions = "";
            List<SqlParameter> parameters = new List<SqlParameter>();

            #region Patch Validation Conditions

            if (!string.IsNullOrEmpty(model.BlogTitle))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
                parameters.Add(new SqlParameter("@BlogTitle", SqlDbType.NVarChar) { Value = model.BlogTitle });
            }


            if (!string.IsNullOrEmpty(model.BlogAuthor))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
                parameters.Add(new SqlParameter("@BlogAuthor", SqlDbType.NVarChar) { Value = model.BlogAuthor });
                item.BlogAuthor = model.BlogAuthor;
            }

            if (!string.IsNullOrEmpty(model.BlogContent))
            {
                conditions += " [BlogContent] = @BlogContent, ";
                parameters.Add(new SqlParameter("@BlogContent", SqlDbType.NVarChar) { Value = model.BlogContent });
                item.BlogContent = model.BlogContent;
            }

            if (conditions.Length == 0)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }
            #endregion

            conditions = conditions.TrimEnd(',', ' ');
            query = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions} WHERE BlogId = @BlogId";
            SqlCommand cmd2 = new SqlCommand(query, connection);
            cmd2.Parameters.AddWithValue("@BlogId", id);
            cmd2.Parameters.AddRange(parameters.ToArray());

            int result = cmd2.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Patch Updating Successful." : "Patch Updating Failed.";
            return Ok(message);
        }
    }
}