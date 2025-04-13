using TaskSystemHub.Shared.DTOs;
using TaskSystemHub.Shared.Builders;


namespace TaskSystemHub.Application.Interfaces
{
    public interface IJiraApiClient
    {
        Task<JiraBoardDTO> GetBoardFromJiraAsync();
        Task<JiraBoardResponse> GetBoardResponseFromJiraAsync();
        Task<string> GetStringBoardFromJiraAsync();
        Task<string> GetSprintActiveFromJiraAsync(int boardId);
        Task<string> GetIssueParentFromJiraAsync(int sprintId, int maxResults, string activity);
        Task<JiraIssueCreatedResponse?> CreateIssueAsync(CreateIssueRequestDto createDto);
        Task<bool> TransitionIssueAsync(string issueKey, TransitionRequestDto transitionDto);
    }
}