// using System.Collections.Generic;
// using System.Threading.Tasks;
// using TaskSystemHub.Domain.Entities;
// using TaskSystemHub.Infrastructure.Jira;
// using TaskSystemHub.Application.Interfaces;


// namespace TaskSystemHub.Application.Services
// {
//     public class JiraService
//     {
//         private readonly JiraApiClient _jiraApiClient;
//        //  private readonly ITaskRepository _taskRepository;

//         public JiraService(JiraApiClient jiraApiClient)
//         {
//             _jiraApiClient = jiraApiClient;
//         }

//         // public async Task SyncTasksFromJiraAsync()
//         // {
//         //     var jiraIssues = await _jiraApiClient.GetIssuesFromJiraAsync();
//         //     if (jiraIssues == null) return;

//         //     var tasks = new List<TaskEntity>();

//         //     foreach (var issue in jiraIssues)
//         //     {
//         //         var task = new TaskEntity
//         //         {
//         //             Title = issue.Fields.Summary,
//         //             Description = issue.Fields.Description,
//         //             JiraIssueKey = issue.Key,
//         //             Status = issue.Fields.Status.Name
//         //         };
//         //         tasks.Add(task);
//         //     }

//         //     await _taskRepository.AddTasksAsync(tasks);
//         // }
//     }
// }
