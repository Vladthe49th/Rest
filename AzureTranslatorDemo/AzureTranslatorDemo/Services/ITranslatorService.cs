namespace AzureTranslatorDemo.Services;

public interface ITranslatorService
{
    Task<string> TranslateAsync(string text, string targetLanguage);
}