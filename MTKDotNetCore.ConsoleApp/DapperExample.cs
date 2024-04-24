using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTKDotNetCore.ConsoleApp
{
    public class DapperExample
    {
        public void Run()
        {
            //Create("Lemon", "Lemon", "Lemon");
            //Update(2,"Lemon", "Lemon", "Lemon");
            Read();
            //Delete(2);
            //Edit(3);
            //Update(3, "32", "334", "23");
        }

        #region Read

        private void Read()
        {
            using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            List<BlogDto> lst = db.Query<BlogDto>("select * from tbl_blog").ToList();

            foreach (BlogDto item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("-----------------------");
            }
        }

        #endregion

        #region Edit

        private void Edit(int id)
        {
            using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            var item = db.Query<BlogDto>("select * from tbl_blog where blogid = @BlogId", new BlogDto { BlogId = id }).FirstOrDefault();
            if (item is null)
            {
                Console.WriteLine("No Data Not Found");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("-----------------------");
        }

        #endregion


        #region Create

        private void Create(string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                            ([BlogTitle],
                            [BlogAuthor],
                            [BlogContent])
                VALUES
                    (@BlogTitle,
                    @BlogAuthor,
                    @BlogContent)";

            using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            string message = result > 0 ? "New Blog Creation Successful" : "New Blog Creation Fail";
            Console.WriteLine(message);
        }

        #endregion

        #region Update


        public void Update(int id, string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            string query = @"UPDATE [dbo].[Tbl_Blog]
                            SET [BlogTitle] = @BlogTitle
                            ,[BlogAuthor] = @BlogAuthor
                            ,[BlogContent] = @BlogContent
                            WHERE BlogId=@BlogId";

            using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            string message = result > 0 ? "Blog Update Successful" : "Blog Update Fail";
            Console.WriteLine(message);
        }

        #endregion

        #region Delete

        private void Delete(int id)
        {
            var item = new BlogDto
            {
                BlogId = id
            };

            string query = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId=@BlogId";

            using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            string message = result > 0 ? "Blog Delete Successful" : "Blog Delete Fail";
            Console.WriteLine(message);
        }

        #endregion
    }
}
