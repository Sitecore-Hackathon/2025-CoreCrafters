using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Hackathon.Repository
{
    public class ChatRepository
    {
        private readonly string apiKey = "";  // Replace with actual API key
        private readonly string apiUrl = "https://api.openai.com/v1/chat/completions";

        // Stores conversation history to maintain context
        private static List<object> conversationHistory = new List<object>();

        public async Task<string> GetChatResponseAsync(string userMessage)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                    // Maintain chat history
                    conversationHistory.Add(new { role = "user", content = userMessage });

                    var requestBody = new
                    {
                        model = "gpt-4o-mini",
                        messages = new List<object>
                        {
                            new { role = "system", content = "You are an expert on Sitecore XP. Provide concise, well-structured answers (≤ 200 words). If an answer is long, summarize instead of cutting it off." }
                        }.Concat(conversationHistory).ToList(),
                        temperature = 0.7,
                        max_tokens = 300
                    };

                    var jsonRequest = JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                    if (!response.IsSuccessStatusCode)
                    {
                        return "Error: Unable to fetch response from AI service.";
                    }

                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    dynamic responseData = JsonConvert.DeserializeObject(jsonResponse);
                    string botResponse = responseData.choices[0].message.content;

                    // Add AI response to history
                    conversationHistory.Add(new { role = "assistant", content = botResponse });

                    return FormatBotResponse(botResponse);
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        private string FormatBotResponse(string response)
        {
            if (string.IsNullOrEmpty(response))
                return response;

            // Convert **bold** to <b>...</b>
            response = Regex.Replace(response, @"\*\*(.*?)\*\*", "<b>$1</b>");

            // Convert *italic* to <i>...</i>
            response = Regex.Replace(response, @"\*(.*?)\*", "<i>$1</i>");

            // Convert bullet points (- item) into <ul><li>...</li></ul>
            response = Regex.Replace(response, @"- (.*?)(\r?\n|$)", "<li>$1</li>");

            // Wrap <li> elements inside <ul>
            if (response.Contains("<li>"))
                response = "<ul>" + response + "</ul>";

            // Replace new lines with <br>
            response = response.Replace("\n", "<br>");

            return "<div class='bot-text'>" + response + "</div>";
        }
    }
}
