using AzureTranslatorDemo.Models;
using AzureTranslatorDemo.Services;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var settings = new TranslatorSettings();

configuration
    .GetSection("Translator")
    .Bind(settings);

ITranslatorService translator =
    new TranslatorService(settings);

Console.WriteLine(" Azure Translator");
Console.WriteLine();

Console.Write("Введіть текст: ");

string? text = Console.ReadLine();

Console.WriteLine();
Console.WriteLine("Оберіть мову:");

Console.WriteLine("en - English");
Console.WriteLine("de - German");
Console.WriteLine("fr - French");
Console.WriteLine("es - Spanish");
Console.WriteLine("uk - Ukrainian");

Console.Write("Ваш вибір: ");

string? language = Console.ReadLine();

try
{
    string result =
        await translator.TranslateAsync(
            text ?? string.Empty,
            language ?? "en");

    Console.WriteLine();
    Console.WriteLine("Переклад:");
    Console.WriteLine(result);
}
catch (Exception ex)
{
    Console.WriteLine();
    Console.WriteLine("Помилка:");
    Console.WriteLine(ex.Message);
}