using System.Text;
using System.Text.Json;
using AzureTranslatorDemo.Models;

namespace AzureTranslatorDemo.Services;

public class TranslatorService : ITranslatorService
{
    private readonly HttpClient _httpClient;
    private readonly TranslatorSettings _settings;

    public TranslatorService(TranslatorSettings settings)
    {
        _settings = settings;
        _httpClient = new HttpClient();
    }

    public async Task<string> TranslateAsync(string text, string targetLanguage)
    {
        string route =
            $"/translate?api-version=3.0&to={targetLanguage}";

        string requestUri =
            _settings.Endpoint.TrimEnd('/') + route;

        object[] body =
        {
            new { Text = text }
        };

        string requestBody =
            JsonSerializer.Serialize(body);

        using var request =
            new HttpRequestMessage(
                HttpMethod.Post,
                requestUri);

        request.Content =
            new StringContent(
                requestBody,
                Encoding.UTF8,
                "application/json");

        request.Headers.Add(
            "Ocp-Apim-Subscription-Key",
            _settings.Key);

        request.Headers.Add(
            "Ocp-Apim-Subscription-Region",
            _settings.Region);

        HttpResponseMessage response =
            await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        string json =
            await response.Content.ReadAsStringAsync();

        using JsonDocument document =
            JsonDocument.Parse(json);

        return document
            .RootElement[0]
            .GetProperty("translations")[0]
            .GetProperty("text")
            .GetString()!;
    }
}