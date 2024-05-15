// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

HttpClient httpClient = new HttpClient();
HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7279/api/movieticket/getmovies");
if (response.IsSuccessStatusCode)
{
    string jsonStr = await response.Content.ReadAsStringAsync();
    Console.WriteLine(jsonStr);
    Console.ReadKey();  
}   