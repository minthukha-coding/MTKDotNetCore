using Microsoft.AspNetCore.Mvc;
using MTKDotNetCore.RestAPI.Models;
using System.Data;
using System.Data.SqlClient;

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
            string query = "select * from Tbl_Blog where Blog_Id = @Blog_Id";
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BlogId", id);
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
            return Ok();
        }
    }
}