using TaskSystemHub.Domain.Entities;
using TaskSystemHub.Persistence;
using TaskSystemHub.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TaskSystemHub.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;

        public TaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task AddTaskAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        // public async Task UpdateTaskAsync(TaskItem task)
        // {
        //     _context.Tasks.Update(task);
        //     await _context.SaveChangesAsync();
        // }

        // public async Task DeleteTaskAsync(int id)
        // {
        //     var task = await _context.Tasks.FindAsync(id);
        //     if (task != null)
        //     {
        //         _context.Tasks.Remove(task);
        //         await _context.SaveChangesAsync();
        //     }
        // }
    }
}