using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTKDotNetCore.ConsoleApp.RefitExample
{
    public class RefitExample
    {
        private readonly IBlogApi _service = RestService.For<IBlogApi>("https://localhost:7051");
        public async Task Run()
        {
            await Read();
            await Edit(8);
            await Update(8, "YOYOYO", "YOUOYO", "YOYOUO");
            await Edit(8);
            await Delete(8);
            await Create("HEHEHEHEHE", "HEHEHEHEHE", "HEHEHEH");
        }
        private async Task Read()
        {
            var lst = await _service.GetBlogs();
            foreach (var item in lst)
            {
                Console.WriteLine($"Id => {item.BlogId}");
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}");
                Console.WriteLine("---------------------------------");
            }
        }
        private async Task Edit(int id)
        {
            try
            {
                var item = await _service.GetBlog(id);
                Console.WriteLine($"Id => {item.BlogId}");
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}");
                Console.WriteLine("---------------------------------");
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.StatusCode.ToString());
                Console.WriteLine(ex.Content);
            }   
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async Task Create(string title , string author,string content)
        {
            BlogModel reqModel = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            var response = await _service.CreateBlog(reqModel);
            Console.WriteLine(response);
        }
        private async Task Update(int id, string title, string author, string content)
        {
            BlogModel reqModel = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            var response = await _service.UpdateBlog(id,reqModel);
            Console.WriteLine(response);
        }
        private async Task Delete(int id)
        {
            var response = await _service.DeleteBlog(id);
        }
    }
}
