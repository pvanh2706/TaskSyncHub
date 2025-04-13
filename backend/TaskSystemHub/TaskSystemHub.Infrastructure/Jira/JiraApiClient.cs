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
using System.Net;
using Newtonsoft.Json.Linq;

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
        // Lấy danh sách dự án trong Jira
        public async Task<JiraBoardDTO> GetBoardFromJiraAsync()
        {
            string url = ApiRoutes.Jira.GetBoard;
            var response = await _httpClient.GetAsync(url);
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
            string url = ApiRoutes.Jira.GetBoard;
            var response = await _httpClient.GetAsync(url);
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
            string url = ApiRoutes.Jira.GetIssueParent
                .Replace("{sprintId}", sprintId.ToString())
                .Replace("{maxResults}", maxResults.ToString())
                .Replace("{activity}", activity);
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get issue parent from Jira: {content}");
            }
            return content;
        }
        // var createDto = new CreateIssueRequestDto
        // {
        //     IssueTypeId = "6",
        //     CustomField12815 = "Development",
        //     CustomField13419 = "New Development",
        //     CustomField14332 = "ezFolio",
        //     ComponentId = "15690",
        //     CustomField14338 = "SX_Development",
        //     AssigneeName = "anh.phamviet",
        //     CustomField14018 = "Rất phức tạp (4.5h <= EST <= 6h)",
        //     ParentKey = "EAS-31325",
        //     ProjectKey = "EAS",
        //     ProjectId = "175",
        //     CustomField12412 = "2025-04-10",
        //     CustomField12413 = "2025-04-10",
        //     CustomField13630 = "2025-04-10",
        //     Summary = "Trực, xử lý iss trực",
        //     WorklogStarted = "2025-04-10T00:00:00.000+0000",
        //     WorklogTimeSpent = "4.7h",
        //     WorklogComment = "Trực, xử lý iss trực"
        // };

        // await _jiraService.CreateIssueAsync(createDto);

        public async Task<JiraIssueCreatedResponse?> CreateIssueAsync(CreateIssueRequestDto createDto)
        {
            JiraIssueRequestDto requestDto = new JiraIssueRequestDto
            {
                fields = new FieldsDto
                {
                    issuetype = new IssueTypeDto { id = createDto.IssueTypeId },
                    customfield_12815 = new CustomFieldDto { value = createDto.CustomField12815 },
                    customfield_13419 = new CustomFieldDto { value = createDto.CustomField13419 },
                    customfield_14332 = new CustomFieldDto { value = createDto.CustomField14332 },
                    components = new[] { new ComponentDto { id = createDto.ComponentId } },
                    customfield_14338 = new CustomFieldDto { value = createDto.CustomField14338 },
                    assignee = new AssigneeDto { name = createDto.AssigneeName },
                    customfield_14018 = new CustomFieldDto { value = createDto.CustomField14018 },
                    parent = new ParentDto { key = createDto.ParentKey },
                    project = new ProjectKeyDto { key = createDto.ProjectKey },
                    customfield_12412 = createDto.CustomField12412,
                    customfield_12413 = createDto.CustomField12413,
                    customfield_13630 = createDto.CustomField13630,
                    summary = createDto.Summary
                }
            };

            var json = JsonConvert.SerializeObject(requestDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://jira.ezcloudhotel.com/rest/api/2/issue", content);

            if (response.StatusCode == HttpStatusCode.Created) // 201
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<JiraIssueCreatedResponse>(responseString);
                return result;
            }

            return null;
        }

        public async Task<bool> TransitionIssueAsync(string issueKey, TransitionRequestDto transitionDto)
        {
            var url = $"https://jira.ezcloudhotel.com/rest/api/2/issue/{issueKey}/transitions";

            var jsonContent = JsonConvert.SerializeObject(transitionDto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            return response.StatusCode == HttpStatusCode.NoContent; // 204
        }

    }
}
