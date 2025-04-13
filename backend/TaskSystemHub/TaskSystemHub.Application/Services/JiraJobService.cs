using TaskSystemHub.Application.Interfaces;
using TaskSystemHub.Shared.DTOs;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

public class JiraJobService
{
    private readonly IJiraService _jiraService;
    private readonly IScheduledIssueRepository _issueRepo;
    private readonly ILogger _logger;

    public JiraJobService(IJiraService jiraService, IScheduledIssueRepository issueRepo, ILogger logger)
    {
        _jiraService = jiraService;
        _issueRepo = issueRepo;
        _logger = logger;
    }

    public async Task RunCreateAndTransitionJobAsync()
    {
        var scheduledItems = await _issueRepo.GetPendingIssuesAsync();

        foreach (var item in scheduledItems)
        {
            try
            {
                var dto = JsonConvert.DeserializeObject<CreateIssueRequestDto>(item.JsonContent);
                var result = await _jiraService.CreateIssueAndTransitionAsync(dto, "61");

                await _issueRepo.UpdateStatusAsync(item.Id, result != null ? "Completed" : "Failed");

                Console.WriteLine($"[Hangfire] Processed item {item.Id} - Result: {(result != null ? result.IssueInfo.key : "Failed")}");
                _logger.LogInformation("[Hangfire] Created and transitioned issue: {IssueKey}", result.IssueInfo.key);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Hangfire] Exception for item {item.Id}: {ex.Message}");
                await _issueRepo.UpdateStatusAsync(item.Id, "Failed");
            }
        }
    }
}