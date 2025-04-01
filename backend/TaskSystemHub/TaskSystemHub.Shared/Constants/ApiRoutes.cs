namespace TaskSystemHub.Shared.Constants
{
    public static class ApiRoutes
    {
        public static class Jira
        {
            public const string BaseUrl = "https://jira.ezcloudhotel.com";
            public const string GetBoard = "/rest/agile/1.0/board";
            public const string CreateIssue = "/rest/api/2/issue";
        }

        public static class Slack
        {
            public const string BaseUrl = "https://slack.com/api";
            public const string SendMessage = "/chat.postMessage";
        }
    }
}
