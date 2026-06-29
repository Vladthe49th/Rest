namespace ItemsAPI.Models
{
    public class TranslateRequest
    {
        public string Text { get; set; } = string.Empty;

        public string ToLanguage { get; set; } = "uk";
    }
}