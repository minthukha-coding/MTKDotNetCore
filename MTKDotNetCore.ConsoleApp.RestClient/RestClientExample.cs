using Newtonsoft.Json;
using RestSharp;

namespace MTKDotNetCore.ConsoleApp.RestClientExamples
{
    public class RestClientExample
    {
        private readonly RestClient _restClient = new RestClient(new Uri("https://localhost:7051"));
        private readonly string _blogEndPoint = "api/blogAdoDotNet";
        public async Task Run()
        {
            //await Read();
            await Update(1024, "123123", "23423", "2423");
            await Edit(1024);
            //Delete(24);
            //Edit(24);
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
                    //Console.WriteLine($"Title => {item.BlogTitle}");
                    //Console.WriteLine($"Author => {item.BlogAuthor}");
                    //Console.WriteLine($"Content => {item.BlogContent}");
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
        public async Task Create(string title,string author,string content)
        {
            BlogModel model = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            var request = new RestRequest(_blogEndPoint, Method.Post);
            request.AddJsonBody(model);
            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }
        public async Task Update(int id,string title,string author,string content)
        {
            BlogModel model = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            var request = new RestRequest($"{_blogEndPoint}/{id}",Method.Put);
            request.AddJsonBody(model);
            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }
        public async Task Delete(int id)
        {
            var request = new RestRequest($"{_blogEndPoint}/{id}", Method.Delete);
            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
            else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }
    }
}
