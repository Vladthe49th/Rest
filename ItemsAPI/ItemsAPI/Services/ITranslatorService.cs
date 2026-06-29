namespace ItemsAPI.Services
{
    public interface ITranslatorService
    {
        Task<string> TranslateAsync(string text, string toLanguage);
    }
}