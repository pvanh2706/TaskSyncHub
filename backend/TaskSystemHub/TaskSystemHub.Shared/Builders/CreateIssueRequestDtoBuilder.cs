using TaskSystemHub.Shared.DTOs;
// Fluent Builder pattern
namespace TaskSystemHub.Shared.Builders
{
    public class CreateIssueRequestDtoBuilder
    {
        private readonly JiraIssueRequestDto _dto = new()
        {
            fields = new FieldsDto(),
            update = new UpdateDto(),
            project = new ProjectMetaDto()
        };

        public CreateIssueRequestDtoBuilder WithIssueType(string id)
        {
            _dto.fields.issuetype = new IssueTypeDto { id = id };
            return this;
        }

        public CreateIssueRequestDtoBuilder WithCustomField12815(string value)
        {
            _dto.fields.customfield_12815 = new CustomFieldDto { value = value };
            return this;
        }

        public CreateIssueRequestDtoBuilder WithCustomField13419(string value)
        {
            _dto.fields.customfield_13419 = new CustomFieldDto { value = value };
            return this;
        }

        public CreateIssueRequestDtoBuilder WithCustomField14332(string value)
        {
            _dto.fields.customfield_14332 = new CustomFieldDto { value = value };
            return this;
        }

        public CreateIssueRequestDtoBuilder WithComponent(string componentId)
        {
            _dto.fields.components = new[] { new ComponentDto { id = componentId } };
            return this;
        }

        public CreateIssueRequestDtoBuilder WithCustomField14338(string value)
        {
            _dto.fields.customfield_14338 = new CustomFieldDto { value = value };
            return this;
        }

        public CreateIssueRequestDtoBuilder WithAssignee(string name)
        {
            _dto.fields.assignee = new AssigneeDto { name = name };
            return this;
        }

        public CreateIssueRequestDtoBuilder WithCustomField14018(string value)
        {
            _dto.fields.customfield_14018 = new CustomFieldDto { value = value };
            return this;
        }

        public CreateIssueRequestDtoBuilder WithParentKey(string key)
        {
            _dto.fields.parent = new ParentDto { key = key };
            return this;
        }

        public CreateIssueRequestDtoBuilder WithProjectKey(string key, string id)
        {
            _dto.fields.project = new ProjectKeyDto { key = key };
            _dto.project = new ProjectMetaDto { id = id };
            return this;
        }

        public CreateIssueRequestDtoBuilder WithDateFields(string date1, string date2, string date3)
        {
            _dto.fields.customfield_12412 = date1;
            _dto.fields.customfield_12413 = date2;
            _dto.fields.customfield_13630 = date3;
            return this;
        }

        public CreateIssueRequestDtoBuilder WithSummary(string summary)
        {
            _dto.fields.summary = summary;
            return this;
        }

        public CreateIssueRequestDtoBuilder WithWorklog(string started, string timeSpent, string comment)
        {
            _dto.update.worklog = new[]
            {
                new WorklogWrapper
                {
                    add = new WorklogAdd
                    {
                        started = started,
                        timeSpent = timeSpent,
                        comment = comment
                    }
                }
            };
            return this;
        }

        public JiraIssueRequestDto Build() => _dto;
    }
}
