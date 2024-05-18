using MTKDotNetCore.ConsoleApp.RestClientExamples;

Console.WriteLine("Hello World");

RestClientExample restClientExample = new RestClientExample();
await restClientExample.Run();
Console.ReadLine();