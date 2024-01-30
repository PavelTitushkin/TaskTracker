using TaskTracker.Abstractions.Entities;

namespace TaskTracker.Abstractions.Services
{
    public interface ITaskService
    {
        Task<TaskEntity?> GetTaskByIdAsync(int taskId, CancellationToken cancellationToken);
        Task<List<TaskEntity>?> GetTasksAsync(int offset, int take, CancellationToken cancellationToken);
        Task<TaskEntity?> CreateTaskAsync(TaskEntity task, CancellationToken cancellationToken);
        Task<TaskEntity?> EditTaskAsync(int id, TaskEntity task, CancellationToken cancellationToken);
        Task DeleteTaskAsync(int id, CancellationToken cancellationToken);
    }
}
