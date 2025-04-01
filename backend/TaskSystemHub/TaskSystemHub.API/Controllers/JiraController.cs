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
   }
}
