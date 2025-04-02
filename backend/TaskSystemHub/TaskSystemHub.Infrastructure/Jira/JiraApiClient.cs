// using System.Net.Http;
// using System.Net.Http.Headers;
// using System.Text;
// using System.Text.Json;
// using System.Threading.Tasks;
// using TaskSystemHub.Shared.DTOs;
using Microsoft.Extensions.Configuration;
using TaskSystemHub.Application.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using TaskSystemHub.Shared.Constants;
using Newtonsoft.Json;
using TaskSystemHub.Shared.DTOs;
using Serilog;

namespace TaskSystemHub.Infrastructure.Jira
{
    public class JiraApiClient : IJiraApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _jiraBaseUrl;

        public JiraApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _jiraBaseUrl = configuration["Jira:BaseUrl"] ?? throw new ArgumentNullException("JiraBaseUrl is missing in configuration");

            string userName = configuration["Jira:UserName"] ?? throw new ArgumentNullException("JiraUserName is missing in configuration");
            string password = configuration["Jira:Password"] ?? throw new ArgumentNullException("JiraPassword is missing in configuration");
            // string token = configuration["Jira:Token"] ?? throw new ArgumentNullException("JiraToken is missing in configuration");

            string authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{userName}:{password}"));
            _httpClient.BaseAddress = new Uri(_jiraBaseUrl);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
        }

        public async Task<JiraBoardDTO> GetBoardFromJiraAsync()
        {
            var response = await _httpClient.GetAsync(ApiRoutes.Jira.GetBoard);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get board from Jira: {content}");
            }
            JiraBoardDTO result = JsonConvert.DeserializeObject<JiraBoardDTO>(content);
            return result;
        }

        public async Task<string> GetStringBoardFromJiraAsync()
        {
            var response = await _httpClient.GetAsync(ApiRoutes.Jira.GetBoard);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get board from Jira: {content}");
            }
            return content;
        }

        public async Task<JiraBoardResponse> GetBoardResponseFromJiraAsync()
        {
            var response = await _httpClient.GetAsync(ApiRoutes.Jira.GetBoard);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get board from Jira: {content}");
            }
            if (string.IsNullOrEmpty(content))
            {
                throw new Exception("Failed to get board from Jira: Empty response");
            }
            try
            {
                JiraBoardResponse result = JsonConvert.DeserializeObject<JiraBoardResponse>(content);
                Log.Information("Successfully got board from Jira {Content}", content);
                return result;
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to deserialize board response from Jira: {ex.Message}");
                throw new Exception($"Failed to deserialize board response from Jira: {ex.Message}");
            }
        }

        public async Task<string> GetSprintActiveFromJiraAsync(int boardId)
        {
            var response = await _httpClient.GetAsync(ApiRoutes.Jira.GetSprintActive.Replace("{boardId}", boardId.ToString()));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get sprint active from Jira: {content}");
            }
            return content;
        }

        public async Task<string> GetIssueParentFromJiraAsync(int sprintId, int maxResults, string activity)
        {
            string url = ApiRoutes.Jira.GetIssueParent.Replace("{sprintId}", sprintId.ToString()).Replace("{maxResults}", maxResults.ToString()).Replace("{activity}", activity);
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get issue parent from Jira: {content}");
            }
            return content;
        }
    }
}
