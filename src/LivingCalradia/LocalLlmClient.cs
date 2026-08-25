using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LivingCalradia
{
    public static class LocalLlmClient
    {
        private static readonly HttpClient Client = new HttpClient();

        public static async Task<LlmResponse> SendPromptAsync(string prompt)
        {
            var request = new
            {
                model = "Qwen/Qwen3-8B-GGUF:Q4_K_M",
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = prompt
                    }
                }
            };

            string json = JsonConvert.SerializeObject(request);

            using (var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"))
            {
                HttpResponseMessage response = await Client.PostAsync(
                    "http://127.0.0.1:8080/v1/chat/completions",
                    content
                );

                string responseJson = await response.Content.ReadAsStringAsync();

                JObject result = JObject.Parse(responseJson);

                string responseText =
                     result["choices"]?[0]?["message"]?["content"]?.ToString();

                return JsonConvert.DeserializeObject<LlmResponse>(responseText);
            }
        }

        public static async Task<AiDecisionResponse> SendDecisionAsync(string prompt)
        {
            var request = new
            {
                model = "Qwen/Qwen3-8B-GGUF:Q4_K_M",
                messages = new[]
                {
            new
            {
                role = "user",
                content = prompt
            }
        }
            };

            string json = JsonConvert.SerializeObject(request);

            using (var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"))
            {
                HttpResponseMessage response = await Client.PostAsync(
                    "http://127.0.0.1:8080/v1/chat/completions",
                    content
                );

                string responseJson = await response.Content.ReadAsStringAsync();

                JObject result = JObject.Parse(responseJson);

                string responseText =
                    result["choices"]?[0]?["message"]?["content"]?.ToString();

                return JsonConvert.DeserializeObject<AiDecisionResponse>(responseText);
            }
        }


    }
}