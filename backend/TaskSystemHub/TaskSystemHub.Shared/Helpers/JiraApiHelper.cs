using Newtonsoft.Json.Linq;
using TaskSystemHub.Domain.Entities;
using TaskSystemHub.Shared.DTOs;

namespace TaskSystemHub.Shared.Helpers
{
    public static class JiraApiHelper
    {
        /// <summary>
        /// Chuyển đổi dữ liệu từ Jira API JSON sang JiraIssueDTO
        /// </summary>
        public static JiraIssueDTO ConvertToJiraIssueDTO(string jsonResponse)
        {
            var json = JObject.Parse(jsonResponse);

            return new JiraIssueDTO
            {
                IssueKey = json["key"]?.ToString(),
                Summary = json["fields"]?["summary"]?.ToString(),
                Description = json["fields"]?["description"]?.ToString(),
                Status = json["fields"]?["status"]?["name"]?.ToString(),
                Assignee = json["fields"]?["assignee"]?["displayName"]?.ToString(),
                CreatedAt = DateTime.Parse(json["fields"]?["created"]?.ToString() ?? DateTime.UtcNow.ToString())
            };
        }

        /// <summary>
        /// Chuyển đổi từ JiraIssueDTO sang Entity JiraIssue để lưu vào DB
        /// </summary>
        // public static JiraIssue ConvertToJiraIssueEntity(JiraIssueDTO dto)
        // {
        //     return new JiraIssue
        //     {
        //         Key = dto.IssueKey,
        //         Summary = dto.Summary,
        //         Description = dto.Description,
        //         Status = dto.Status,
        //         Assignee = dto.Assignee,
        //         Created = dto.CreatedAt
        //     };
        // }
    }
}
