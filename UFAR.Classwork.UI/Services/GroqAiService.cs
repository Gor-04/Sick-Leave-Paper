//using System;
//using System.Threading.Tasks;
//using Groq;
//using Groq.Chat;
//using Microsoft.Extensions.Caching.Memory;
//using Microsoft.Extensions.Logging;

//public class GroqAiService
//{
//    private readonly GroqClient _client;
//    private readonly IMemoryCache _cache;
//    private readonly ILogger<GroqAiService> _logger;
//    private const string DefaultModel = "llama3-70b-8192"; // suggested free model from Groq example

//    public GroqAiService(GroqClient client, IMemoryCache cache, ILogger<GroqAiService> logger)
//    {
//        _client = client;
//        _cache = cache;
//        _logger = logger;
//    }

//    /// <summary>
//    /// Explain an illness description in simple friendly words.
//    /// Uses caching keyed by a hash of the illness text to avoid repeated calls.
//    /// </summary>
//    public async Task<string> ExplainIllnessAsync(string illnessDescription)
//    {
//        if (string.IsNullOrWhiteSpace(illnessDescription))
//            return "No illness description provided.";

//        // Use a short cache key - you could use a hash if descriptions are long.
//        var cacheKey = "explain:" + illnessDescription.Trim().ToLowerInvariant();

//        if (_cache.TryGetValue(cacheKey, out string cached))
//        {
//            return cached;
//        }

//        try
//        {
//            var request = new ChatCompletionRequest()
//            {
//                Model = DefaultModel,
//                Messages =
//                {
//                    new ChatCompletionMessage()
//                    {
//                        Role = ChatCompletionMessageRole.System,
//                        Content = "You are a friendly, concise medical assistant that explains medical notes in plain language for non-medical patients. Focus on clarity, short sentences, and what the patient should know."
//                    },
//                    new ChatCompletionMessage()
//                    {
//                        Role = ChatCompletionMessageRole.User,
//                        Content = $"Explain the following illness in simple friendly words for a patient. Keep it short (2-4 short sentences). If there are any warning signs, mention them briefly.\n\n\"{illnessDescription}\""
//                    }
//                },
//                // Adjust other settings as supported by SDK (temperature/limits)
//            };

//            var response = await _client.Chat.CreateAsync(request);

//            if (response?.Choices?.Count > 0)
//            {
//                var text = response.Choices[0].Message.Content.Trim();

//                // Cache for 24 hours (adjust TTL as you like)
//                _cache.Set(cacheKey, text, TimeSpan.FromHours(24));
//                return text;
//            }

//            return "⚠️ AI returned no explanation.";
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "Groq AI request failed");
//            return $"❌ AI request failed: {ex.Message}";
//        }
//    }
//}
