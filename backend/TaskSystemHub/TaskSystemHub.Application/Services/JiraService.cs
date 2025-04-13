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

        public async Task<CreateIssueResult> CreateIssueAndTransitionAsync(CreateIssueRequestDto createDto, string transitionId)
        {
            var result = new CreateIssueResult();

            // 1. Tạo issue
            var issueResponse = await _jiraApiClient.CreateIssueAsync(createDto);
            if (issueResponse == null)
            {
                result.IsIssueCreated = false;
                result.Message = "❌ Failed to create Jira issue.";
                await _slackService.SendMessageAsync(result.Message);
                return result;
            }

            result.IsIssueCreated = true;
            result.IssueInfo = issueResponse;

            // 2. Chuyển trạng thái
            var transitionDto = new TransitionRequestDto
            {
                transition = new TransitionDto { id = transitionId }
            };

            var isTransitioned = await _jiraApiClient.TransitionIssueAsync(issueResponse.key, transitionDto);
            result.IsTransitioned = isTransitioned;

            if (isTransitioned)
            {
                result.Message = $"✅ Created and transitioned Jira issue: *{issueResponse.key}*.";
            }
            else
            {
                result.Message = $"⚠️ Created issue *{issueResponse.key}* but failed to transition.";
            }

            await _slackService.SendMessageAsync(result.Message);
            return result;
        }

    }
}
