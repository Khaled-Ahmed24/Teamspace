using Teamspace.Models;
using System.Text.Json;

namespace AIQAAssistant.Services
{
    public interface IAIGradingService
    {
        Task<GradingResult> GradeAnswerAsync(Question question, QuestionAns answer);
    }

    public class AIGradingService : IAIGradingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly ILogger<AIGradingService> _logger;

        public AIGradingService(HttpClient httpClient, IConfiguration configuration, ILogger<AIGradingService> logger)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GoogleGemini:ApiKey"] ?? throw new ArgumentException("Google Gemini API key not found");
            _logger = logger;
        }

        public async Task<GradingResult> GradeAnswerAsync(Question question, QuestionAns answer)
        {
            try
            {
                var prompt = CreateGradingPrompt(question, answer);
                var response = await CallGeminiApiAsync(prompt);

                return ParseGradingResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error grading answer for question {QuestionId}", question.Id);
                throw;
            }
        }

        private string CreateGradingPrompt(Question question, QuestionAns answer)
        {
            return $@"
You are an expert teacher grading student answers. Please analyze the following:

Question: {question.Title}
Model Answer: {question.CorrectAns}
Student Answer: {answer.StudentAns}
Full Grade: {question.Grade}

Instructions:
1. First, understand the question thoroughly
2. Analyze the model answer to understand the expected response
3. Compare the student's answer with the model answer
4. Assign a grade between 0 and {question.Grade} based on correctness, completeness, and understanding
5. Provide clear reasoning for the grade

Please respond in exactly this JSON format:
{{
  ""grade"": [numerical grade],
  ""reasoning"": ""[detailed explanation of why this grade was given]""
}}";
        }

        private async Task<string> CallGeminiApiAsync(string prompt)
        {
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent?key={_apiKey}";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };

            var response = await _httpClient.PostAsJsonAsync(url, requestBody);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonDocument.Parse(responseContent);

            return jsonResponse.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString() ?? string.Empty;
        }

        private GradingResult ParseGradingResponse(string response)
        {
            try
            {
                // Extract JSON from the response (remove any markdown formatting)
                var jsonStart = response.IndexOf('{');
                var jsonEnd = response.LastIndexOf('}') + 1;
                var jsonString = response.Substring(jsonStart, jsonEnd - jsonStart);

                var gradingData = JsonSerializer.Deserialize<GradingResponse>(jsonString);

                return new GradingResult
                {
                    Grade = gradingData?.grade ?? 0,
                    Reasoning = gradingData?.reasoning ?? "Unable to parse grading response"
                };
            }
            catch (Exception)
            {
                return new GradingResult
                {
                    Grade = 0,
                    Reasoning = "Error parsing AI response. Please review manually."
                };
            }
        }

        private class GradingResponse
        {
            public double grade { get; set; }
            public string reasoning { get; set; } = string.Empty;
        }
    }
}
