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
            //await Read();
            await Update(103412, "123", "123", "YOYOUO");
            //await Edit(1034);
            //await Edit(9999999);
            //await Delete(1028);
            //await Delete(12941324);
            //await Edit(1031);
            //await Edit(103112);
            //await Create("9999999", "9999999", "9999999");
            //await Read();
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
        private async Task Create(string title, string author, string content)
        {
            try
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
        private async Task Update(int id, string title, string author, string content)
        {
            BlogModel reqModel = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            var message = await _service.UpdateBlog(id, reqModel);
            Console.WriteLine(message);
        }
        private async Task Delete(int id)
        {
            var response = await _service.DeleteBlog(id);
            Console.WriteLine(response);
        }
    }
}
