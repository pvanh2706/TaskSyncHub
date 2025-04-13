using TaskSystemHub.Domain.Entities;

public interface IScheduledIssueRepository
{
    Task<List<ScheduledIssue>> GetPendingIssuesAsync();
    Task UpdateStatusAsync(int id, string status);
}
