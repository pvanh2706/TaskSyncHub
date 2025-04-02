using Microsoft.AspNetCore.Mvc;
using TaskSystemHub.Application.Interfaces;

namespace TaskSystemHub.API.Controllers
{
    [Route("api/jira")]
    [ApiController]
    public class JiraController : ControllerBase
    {
        private readonly IJiraService _jiraService;

        public JiraController(IJiraService jiraService)
        {
            _jiraService = jiraService;
        }

        [HttpGet("board")]
        public async Task<IActionResult> GetBoardFromJiraAsync()
        {
            var board = await _jiraService.GetBoardFromJiraAsync();
            return Ok(board);
        }

        [HttpGet("board-string")]
        public async Task<IActionResult> GetStringBoardFromJiraAsync()
        {
            var board = await _jiraService.GetStringBoardFromJiraAsync();
            return Ok(board);
        }

        [HttpGet("board-response")]
        public async Task<IActionResult> GetBoardResponseFromJiraAsync()
        {   
            try
            {
                var board = await _jiraService.GetBoardResponseFromJiraAsync();
                return Ok(board);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get board response from Jira: {ex.Message}");
            }
        }

        [HttpGet("sprint-active")] 
        public async Task<IActionResult> GetSprintActiveFromJiraAsync(int boardId)
        {
            try
            {
                var sprintActive = await _jiraService.GetSprintActiveFromJiraAsync(boardId);
                return Ok(sprintActive);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get sprint active from Jira: {ex.Message}");
            }
        }

        [HttpGet("issue-parent")] 
        public async Task<IActionResult> GetIssueParentFromJiraAsync(int sprintId, int maxResults, string activity)
        {
            try
            {
                var issueParent = await _jiraService.GetIssueParentFromJiraAsync(sprintId, maxResults, activity);
                return Ok(issueParent);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get issue parent from Jira: {ex.Message}");
            }
        }
   }
}
