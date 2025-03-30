// using System.Net.Http;
// using System.Net.Http.Headers;
// using System.Text;
// using System.Text.Json;
// using System.Threading.Tasks;
// using TaskSystemHub.Shared.DTOs;

// namespace TaskSystemHub.Infrastructure.Jira
// {
//     public class JiraApiClient
//     {
//         private readonly HttpClient _httpClient;
//         private readonly string _jiraBaseUrl = "https://your-jira-instance.atlassian.net";
//         private readonly string _jiraToken = "your-api-token"; // Nên lấy từ cấu hình

//         public JiraApiClient(HttpClient httpClient)
//         {
//             _httpClient = httpClient;
//             _httpClient.DefaultRequestHeaders.Authorization = 
//                 new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
//                     Encoding.ASCII.GetBytes($"your-email:your-api-token")));
//         }

//         // public async Task<List<JiraIssueDto>> GetIssuesFromJiraAsync()
//         // {
//         //     var url = $"{_jiraBaseUrl}/rest/api/3/search?jql=project=YOUR_PROJECT";
//         //     var response = await _httpClient.GetAsync(url);

//         //     if (!response.IsSuccessStatusCode)
//         //         return null;

//         //     var json = await response.Content.ReadAsStringAsync();
//         //     var result = JsonSerializer.Deserialize<JiraSearchResultDto>(json);
//         //     return result.Issues;
//         // }
//     }
// }
