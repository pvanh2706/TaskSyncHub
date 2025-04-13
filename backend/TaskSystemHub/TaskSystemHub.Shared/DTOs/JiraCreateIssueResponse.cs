public class JiraIssueCreatedResponse
{
    public string id { get; set; }
    public string key { get; set; }
    public string self { get; set; }
}

public class CreateIssueResult
{
    public bool IsIssueCreated { get; set; }
    public bool IsTransitioned { get; set; }
    public string? Message { get; set; }
    public JiraIssueCreatedResponse? IssueInfo { get; set; }
}
