using TaskSystemHub.Application.Interfaces;
using TaskSystemHub.Shared.DTOs;

namespace TaskSystemHub.Application.Services
{
    public class JiraService : IJiraService
    {
        private readonly IJiraApiClient _jiraApiClient;

        public JiraService(IJiraApiClient jiraApiClient)
        {
            _jiraApiClient = jiraApiClient;
        }

        public async Task<JiraBoardDTO> GetBoardFromJiraAsync()
        {
            return await _jiraApiClient.GetBoardFromJiraAsync();
        }

        public async Task<string> GetStringBoardFromJiraAsync()
        {
            return await _jiraApiClient.GetStringBoardFromJiraAsync();
        }

        public async Task<JiraBoardResponse> GetBoardResponseFromJiraAsync()
        {
            try
            {
                return await _jiraApiClient.GetBoardResponseFromJiraAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get board response from Jira: {ex.Message}");
            }
        }

        public async Task<string> GetSprintActiveFromJiraAsync(int boardId)
        {
            return await _jiraApiClient.GetSprintActiveFromJiraAsync(boardId);
        }

        public async Task<string> GetIssueParentFromJiraAsync(int sprintId, int maxResults, string activity)
        {
            return await _jiraApiClient.GetIssueParentFromJiraAsync(sprintId, maxResults, activity);
        }
    }
}
