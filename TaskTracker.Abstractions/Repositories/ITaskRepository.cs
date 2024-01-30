using TaskTracker.Abstractions.Entities;

namespace TaskTracker.Abstractions.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<List<TaskEntity>?> GetTasksAsync(int offset, int take, CancellationToken cancellationToken);

        Task<TaskEntity?> CreateTaskAsync(TaskEntity task, CancellationToken cancellationToken);

        Task<TaskEntity?> EditTaskAsync(TaskEntity task, CancellationToken cancellationToken);

        Task RemoveTaskAsync(int id, CancellationToken cancellationToken);
    }
}
