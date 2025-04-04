using TaskSystemHub.Shared.DTOs;

namespace TaskSystemHub.Application.Interfaces
{
    public interface IJiraApiClient
    {
        Task<JiraBoardDTO> GetBoardFromJiraAsync();
        Task<JiraBoardResponse> GetBoardResponseFromJiraAsync();
        Task<string> GetStringBoardFromJiraAsync();
        Task<string> GetSprintActiveFromJiraAsync(int boardId);
        Task<string> GetIssueParentFromJiraAsync(int sprintId, int maxResults, string activity);
    }
}