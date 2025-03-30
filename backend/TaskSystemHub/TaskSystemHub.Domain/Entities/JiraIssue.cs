namespace TaskSystemHub.Domain.Entities
{
    public class JiraIssue
    {
        // [Key]
        public string IssueKey { get; set; }
        public string Summary { get; set; }
        public string Status { get; set; }
    }
}
