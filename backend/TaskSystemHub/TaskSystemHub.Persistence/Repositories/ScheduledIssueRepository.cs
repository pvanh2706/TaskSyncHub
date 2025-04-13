using Microsoft.EntityFrameworkCore;
using TaskSystemHub.Application.Interfaces;
using TaskSystemHub.Domain.Entities;
using TaskSystemHub.Persistence;

public class ScheduledIssueRepository : IScheduledIssueRepository
{
    private readonly TaskDbContext _context;

    public ScheduledIssueRepository(TaskDbContext context)
    {
        _context = context;
    }

    public async Task<List<ScheduledIssue>> GetPendingIssuesAsync()
    {
        return await _context.ScheduledIssues
            .Where(x => x.Status == "Pending" && x.ScheduledAt <= DateTime.Now)
            .ToListAsync();
    }

    public async Task UpdateStatusAsync(int id, string status)
    {
        var item = await _context.ScheduledIssues.FindAsync(id);
        if (item != null)
        {
            item.Status = status;
            await _context.SaveChangesAsync();
        }
    }
}
