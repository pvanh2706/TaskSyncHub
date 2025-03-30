namespace TaskSystemHub.Shared.DTOs
{
    public class JiraIssueDTO
    {
        public string IssueKey { get; set; }  // Mã issue (VD: "JIRA-123")
        public string Summary { get; set; }   // Tiêu đề của issue
        public string Description { get; set; }  // Mô tả issue
        public string Status { get; set; }    // Trạng thái (To Do, In Progress, Done)
        public string Assignee { get; set; }  // Người được giao
        public DateTime CreatedAt { get; set; } // Ngày tạo
    }
}
