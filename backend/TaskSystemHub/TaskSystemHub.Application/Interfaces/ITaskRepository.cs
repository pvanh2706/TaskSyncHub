using TaskSystemHub.Domain.Entities;

namespace TaskSystemHub.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
        Task<TaskItem> GetTaskByIdAsync(int id);
        Task AddTaskAsync(TaskItem task);
        // Task UpdateTaskAsync(TaskItem task);
        // Task DeleteTaskAsync(int id);
    }
}