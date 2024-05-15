using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

Console.WriteLine("Hello, World!");

HttpClient httpClient = new HttpClient();
HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7051/api/blogAdoDotNet");
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
    Console.ReadLine();
}
public class BlogModel
{
    [Key]
    public int BlogId { get; set; }
    public string BlogTitle { get; set; }
    public string BlogAuthor { get; set; }
    public string BlogContent { get; set; }
}