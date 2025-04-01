namespace TaskSystemHub.Shared.DTOs
{
    public class JiraBoardDTO
    {
        public string id { get; set; }  // ID của board
        public string self { get; set; } // URL của board
        public string name { get; set; } // Tên của board
        public string type { get; set; } // Loại board
    }
    public class JiraBoardResponse
{
        public int maxResults { get; set; }
        public int startAt { get; set; }
        public int total { get; set; }
        public bool isLast { get; set; }
        public List<JiraBoardDTO> values { get; set; }
    }
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
