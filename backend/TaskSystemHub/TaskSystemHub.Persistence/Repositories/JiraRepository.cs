// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
// using TaskSystemHub.Application.Interfaces;
// using TaskSystemHub.Domain.Entities;

// namespace TaskSystemHub.Persistence.Repositories
// {
//     public class JiraRepository : ITaskRepository
//     {
//         private readonly TaskDbContext _context;

//         public JiraRepository(TaskDbContext context)
//         {
//             _context = context;
//         }

//          public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
//         {
//             return await _context.Tasks.ToListAsync();
//         }

//         public async Task<TaskItem> GetTaskByIdAsync(int id)
//         {
//             return await _context.Tasks.FindAsync(id);
//         }

//         public async Task AddTaskAsync(TaskItem task)
//         {
//             await _context.Tasks.AddAsync(task);
//             await _context.SaveChangesAsync();
//         }
//     }
// }
