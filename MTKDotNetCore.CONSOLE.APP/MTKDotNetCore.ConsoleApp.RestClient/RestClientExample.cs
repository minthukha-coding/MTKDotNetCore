using RestSharp;

namespace MTKDotNetCore.ConsoleApp.RestClientExamples
{
    internal class RestClientExample
    {
        private readonly RestClient _restClient = new RestClient(new Uri("https://localhost:7051"));
        private readonly string _blogEndPoint = "api/blogAdoDotNet";
        public async Task Run()
        {

        }
        private async Task Read()
        {

        }
    }
}
