using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MTKDotNetCore.ConsoleApp.Modles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTKDotNetCore.ConsoleApp.EfCoreExamples
{
    public class EfCoreExample
    {
        private readonly AppDbContext db = new AppDbContext();
        public void Run()
        {
            //Create("H1N1", "H@N2", "H@21q");
            //Read();
            //Update(3, "GOGO", "GOGO", "GOGO");
            //Edit(3);
            Delete(3);
        }

        #region Read

        private void Read()
        {
            var lst = db.Blogs.ToList();
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
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                Console.WriteLine("No data not found");
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

        private void Create(string title,string author,string content)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            db.Blogs.Add(item);
            int result = db.SaveChanges();
            string message = result > 0 ? "New Blog Creation Successful" : "New Blog Creation Fail";
            Console.WriteLine(message);
        }

        #endregion

        #region Update

        private void Update(int id,string title,string author,string content)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null) {
                Console.WriteLine("Blog Was Not Found");
            }

            item.BlogTitle = title;
            item.BlogAuthor = author;
            item.BlogContent = content;

            int result = db.SaveChanges();
            string message = result > 0 ? "Blog Update Successful" : "Blog Update Fail";
            Console.WriteLine(message);
        }

        #endregion

        #region Delete

        private void Delete(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId ==id);
            if(item is null)
            {
                Console.WriteLine("Blog was not found");
                return;
            }
            db.Blogs.Remove(item);
            int result = db.SaveChanges();
            string message = result > 0 ? "Blog Delete Successful" : "Blog Delete Fail";
            Console.WriteLine(message);
        }

        #endregion
    }
}
