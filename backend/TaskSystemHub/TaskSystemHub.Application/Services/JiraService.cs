using TaskSystemHub.Application.Interfaces;
using TaskSystemHub.Shared.DTOs;

namespace TaskSystemHub.Application.Services
{
    public class JiraService : IJiraService
    {
        private readonly IJiraApiClient _jiraApiClient;
        private readonly ISlackService _slackService;

        public JiraService(IJiraApiClient jiraApiClient, ISlackService slackService)
        {
            _jiraApiClient = jiraApiClient;
            _slackService = slackService;
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

        public async Task<bool> CreateIssueAndTransitionAsync(CreateIssueRequestDto createDto, string transitionId)
        {
             // 1. Tạo issue
            var issueKey = await _jiraApiClient.CreateIssueAsync(createDto);
            if (string.IsNullOrEmpty(issueKey))
            {
                // await _slackService.SendMessageAsync("Failed to create Jira issue.");
                return false;
            }

            // 2. Chuyển trạng thái
            var transitionDto = new TransitionRequestDto
            {
                transition = new TransitionDto { id = transitionId }
            };

            var result = await _jiraApiClient.TransitionIssueAsync(issueKey, transitionDto);
            if (!result)
            {
                // await _slackService.SendMessageAsync($"Created issue *{issueKey}* but failed to transition.");
                return false;
            }

            // 3. Gửi thông báo Slack
            // await _slackService.SendMessageAsync($"Created and transitioned Jira issue: *{issueKey}*.");
            return true;
        }
    }
}
