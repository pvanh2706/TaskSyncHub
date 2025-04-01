using TaskSystemHub.Shared.DTOs;

namespace TaskSystemHub.Application.Interfaces
{
    public interface IJiraService
    {
        Task<JiraBoardDTO> GetBoardFromJiraAsync();
        Task<string> GetStringBoardFromJiraAsync();
        Task<JiraBoardResponse> GetBoardResponseFromJiraAsync();
    }
}
