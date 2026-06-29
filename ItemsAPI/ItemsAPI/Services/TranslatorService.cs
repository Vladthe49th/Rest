using System.Text;
using System.Text.Json;

using Microsoft.Extensions.Options;

using ItemsAPI.Configuration;

namespace ItemsAPI.Services
{
    public class TranslatorService : ITranslatorService
    {
        private readonly HttpClient _httpClient;
        private readonly AzureTranslatorOptions _options;

        public TranslatorService(
            HttpClient httpClient,
            IOptions<AzureTranslatorOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<string> TranslateAsync(string text, string toLanguage)
        {
            var endpoint = _options.Endpoint.TrimEnd('/');

            var route =
                $"{endpoint}/translator/text/v3.0/translate?api-version=3.0&to={toLanguage}";

            var body = new[]
            {
                new
                {
                    Text = text
                }
            };

            var json = JsonSerializer.Serialize(body);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            _httpClient.DefaultRequestHeaders.Clear();

            _httpClient.DefaultRequestHeaders.Add(
                "Ocp-Apim-Subscription-Key",
                _options.Key);

            _httpClient.DefaultRequestHeaders.Add(
                "Ocp-Apim-Subscription-Region",
                _options.Region);

            var response = await _httpClient.PostAsync(route, content);

            response.EnsureSuccessStatusCode();

            var responseJson =
                await response.Content.ReadAsStringAsync();

            using var document = JsonDocument.Parse(responseJson);

            var translatedText =
                document.RootElement
                        .EnumerateArray()
                        .First()
                        .GetProperty("translations")
                        .EnumerateArray()
                        .First()
                        .GetProperty("text")
                        .GetString();

            return translatedText ?? string.Empty;
        }
    }
}