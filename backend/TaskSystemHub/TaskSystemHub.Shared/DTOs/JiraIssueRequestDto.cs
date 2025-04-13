namespace TaskSystemHub.Shared.DTOs
{
    public class JiraIssueRequestDto
    {
        public FieldsDto fields { get; set; }
        public ProjectMetaDto project { get; set; }
        public UpdateDto update { get; set; }
    }

    public class FieldsDto
    {
        public IssueTypeDto issuetype { get; set; }
        public CustomFieldDto customfield_12815 { get; set; }
        public CustomFieldDto customfield_13419 { get; set; }
        public CustomFieldDto customfield_14332 { get; set; }
        public ComponentDto[] components { get; set; }
        public CustomFieldDto customfield_14338 { get; set; }
        public AssigneeDto assignee { get; set; }
        public CustomFieldDto customfield_14018 { get; set; }
        public ParentDto parent { get; set; }
        public ProjectKeyDto project { get; set; }
        public string customfield_12412 { get; set; }
        public string customfield_12413 { get; set; }
        public string customfield_13630 { get; set; }
        public string summary { get; set; }
    }

    public class IssueTypeDto
    {
        public string id { get; set; }
    }

    public class CustomFieldDto
    {
        public string value { get; set; }
    }

    public class ComponentDto
    {
        public string id { get; set; }
    }

    public class AssigneeDto
    {
        public string name { get; set; }
    }

    public class ParentDto
    {
        public string key { get; set; }
    }

    public class ProjectKeyDto
    {
        public string key { get; set; }
    }

    public class ProjectMetaDto
    {
        public string id { get; set; }
    }

    public class UpdateDto
    {
        public WorklogWrapper[] worklog { get; set; }
    }

    public class WorklogWrapper
    {
        public WorklogAdd add { get; set; }
    }

    public class WorklogAdd
    {
        public string started { get; set; }
        public string timeSpent { get; set; }
        public string comment { get; set; }
    }
}