public class ScheduledIssue
{
    public int Id { get; set; }
    public string JsonContent { get; set; } // Chứa dữ liệu dạng JSON (CreateIssueRequestDto)
    public string Status { get; set; } = "Pending"; // Pending, Completed, Failed
    public DateTime ScheduledAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
