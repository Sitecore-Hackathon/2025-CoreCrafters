using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class ChatGptService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = ""; // Make sure to use a valid API key

    public ChatGptService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> GetChatGptResponseAsync(string request)
    {
        // Format the request to ChatGPT to generate the PowerShell script
        string prompt = $@"
            Based on the following request, generate template details in the following format:
            
            string templateName = ""TemplateName"";
            string sectionName = ""Section"";
            string fieldsCsv = ""Field1,Field2,Field3"";
            string fieldTypesPipe = ""Type1|Type2|Type3"";
            
            Request: {request}

            For example, if the request is to create a Promo Component template, the response should look like:
            string templateName = ""Promo Component"";
            string sectionName = ""Content"";
            string fieldsCsv = ""Title,Description,Author"";
            string fieldTypesPipe = ""Single-Line Text|Rich Text|Multi-Line Text"";
        ";

        var requestBody = new
        {
            model = "gpt-4o-mini",  // Can use "gpt-4" or "gpt-3.5-turbo"
            messages = new[]
            {
                new { role = "user", content = prompt  }
            }
        };

        var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

        // Add Authorization header with Bearer token
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

        // Make the request to the OpenAI Chat API endpoint
        var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

        // Handle non-successful responses
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"OpenAI API request failed with status code {response.StatusCode}: {response.ReasonPhrase}");
        }

        var responseString = await response.Content.ReadAsStringAsync();
        var responseJson = JsonConvert.DeserializeObject<dynamic>(responseString);

        // Clean the response by removing markdown/code block and extra newlines
        string rawResponse = responseJson.choices[0].message.content.ToString();

        // Remove the markdown/code block (if any) and newlines
        string cleanedResponse = rawResponse.Replace("```csharp", "").Replace("```", "").Replace("\n", "").Trim();

        return cleanedResponse;

    }
}
