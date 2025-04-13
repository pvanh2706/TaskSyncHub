using TaskSystemHub.Shared.DTOs;

namespace TaskSystemHub.Application.Interfaces
{
    public interface IJiraService
    {
        Task<JiraBoardDTO> GetBoardFromJiraAsync();
        Task<string> GetStringBoardFromJiraAsync();
        Task<JiraBoardResponse> GetBoardResponseFromJiraAsync();
        Task<string> GetSprintActiveFromJiraAsync(int boardId);
        Task<string> GetIssueParentFromJiraAsync(int sprintId, int maxResults, string activity);
        Task<bool> CreateIssueAndTransitionAsync(CreateIssueRequestDto createDto, string transitionId);

    }
}
