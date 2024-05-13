// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

string jsonstr = await File.ReadAllTextAsync("data.json");
var model = JsonConvert.DeserializeObject<MinTheinKha>(jsonstr);

foreach (var answer in model.answers)
{
    Console.WriteLine(answer.answerNo);
}

Console.ReadKey();
public class MinTheinKha
{
    public Question[] questions { get; set; }
    public Answer[] answers { get; set; }
    public string[] numberList { get; set; }
}

public class Question
{
    public int questionNo { get; set; }
    public string questionName { get; set; }
}

public class Answer
{
    public int questionNo { get; set; }
    public int answerNo { get; set; }
    public string answerResult { get; set; }
}