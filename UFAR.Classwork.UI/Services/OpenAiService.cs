using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using UFAR.Classwork.UI.Components.Pages;

public class OpenAiService
{
    private readonly HttpClient _http;
//    private readonly string _apiKey = "myKey";

    public OpenAiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> ExplainIllnessAsync(string illnessDescription)
    {
        if (string.IsNullOrWhiteSpace(illnessDescription))
            return "No illness description provided.";

        try
        {
//            var url = "https://api.openai.com/v1/chat/completions";
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);
            var body = new
            {
                model = "gpt-3.5-turbo", // reliable and low-cost
                messages = new[]
                {
                    new { role = "system", content = "You are a friendly doctor assistant." },
                    new { role = "user", content = $"Explain this illness in very simple words: {illnessDescription}" }
                },
                max_tokens = 150
            };

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await _http.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                if ((int)response.StatusCode == 429)
                    return $"⚠️ OpenAI rate limit reached. Please wait a moment and try again.{(int)response.StatusCode} {response.ReasonPhrase}";
                return $"❌ OpenAI returned error: {response.StatusCode} {response.ReasonPhrase}";
            }


            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            return doc.RootElement
                      .GetProperty("choices")[0]
                      .GetProperty("message")
                      .GetProperty("content")
                      .GetString() ?? "No explanation received.";
        }
        catch (Exception ex)
        {
            return $"❌ Exception while calling OpenAI: {ex.Message}";
        }
    }



   

}
