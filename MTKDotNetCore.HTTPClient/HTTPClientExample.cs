using Newtonsoft.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace MTKDotNetCore.ConsoleApp.HTTPClient
{
    public class HTTPClientExample
    {
        private readonly HttpClient _httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:7051") };
        private readonly string _blogEndPoint = "api/blogAdoDotNet";

        public void Run()
        {
            //Read();
            //Edit(422);
            Update(7, "w3wf", "23r23", "ewrw");
            Edit(7);
        }

        private async Task Read()
        {
            var response = await _httpClient.GetAsync(_blogEndPoint);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                List<BlogModel> lst = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr)!;
                foreach (var blog in lst)
                {
                    //Console.WriteLine(JsonConvert.SerializeObject(blog));
                    Console.WriteLine($"Title - {blog.BlogTitle}");
                    Console.WriteLine($"Author - {blog.BlogAuthor}");
                    Console.WriteLine($"Content - {blog.BlogContent}");
                    Console.WriteLine("_____________");
                }
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        private async Task Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_blogEndPoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<BlogModel>(jsonStr)!;
                Console.WriteLine($"Title - {item.BlogTitle}");
                Console.WriteLine($"Author - {item.BlogAuthor}");
                Console.WriteLine($"Content - {item.BlogContent}");
                Console.WriteLine("_____________");
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        private async Task Create(string title, string author, string content)
        {
            BlogModel model = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            string blogJson = JsonConvert.SerializeObject(model);
            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            var response = await _httpClient.PostAsync(_blogEndPoint, httpContent);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        private async Task Update(int id, string title, string author, string content)
        {
            BlogModel model = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            string blogJson = JsonConvert.SerializeObject(model);
            HttpContent httpcontent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            var response = await _httpClient.PutAsync($"{_blogEndPoint}/{id}", httpcontent);
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }
        private async Task Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_blogEndPoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }
    }
}