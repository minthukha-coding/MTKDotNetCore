using Newtonsoft.Json;
using RestSharp;

namespace MTKDotNetCore.ConsoleApp.RestClientExamples
{
    internal class RestClientExample
    {
        private readonly RestClient _restClient = new RestClient(new Uri("https://localhost:7051"));
        private readonly string _blogEndPoint = "api/blogAdoDotNet";
        public async Task Run()
        {
            Read();
            Edit(5);
        }
        private async Task Read()
        {
            RestRequest restRequest = new RestRequest(_blogEndPoint, Method.Get);
            var response = await _restClient.ExecuteAsync(restRequest);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                List<BlogModel> lst = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr)!;
                foreach (var item in lst)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(item));
                    Console.WriteLine($"Title => {item.BlogTitle}");
                    Console.WriteLine($"Author => {item.BlogAuthor}");
                    Console.WriteLine($"Content => {item.BlogContent}");
                }
            }
        }
        private async Task Edit(int id)
        {
            RestRequest restRequest = new RestRequest($"{_blogEndPoint}/{id}", Method.Get);
            var respone = await _restClient.ExecuteAsync(restRequest);
            if (respone.IsSuccessStatusCode)
            {
                string jsonStr = respone.Content!;
                var item = JsonConvert.DeserializeObject<BlogModel>(jsonStr)!;
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}");
            }
        }
    }
}
