using Newtonsoft.Json;

namespace MTKDotNetCore.ConsoleApp.HTTPClient
{
    public class HTTPClientExample
    {
        private readonly HttpClient _httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:7051") };
        private readonly string _blogEndPoint = "api/blogAdoDotNet";

        public void Run()
        {
            Edit(7);
            Edit(422);
            Read();
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
    }
}
