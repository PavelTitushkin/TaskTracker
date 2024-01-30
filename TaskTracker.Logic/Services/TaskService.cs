using TaskTracker.Abstractions.Entities;
using TaskTracker.Abstractions.Repositories;
using TaskTracker.Abstractions.Services;
using TaskTracker.Logic.Exceptions;

namespace TaskTracker.Logic.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task<TaskEntity?> GetTaskByIdAsync(int taskId, CancellationToken cancellationToken)
        {
            return _taskRepository.GetByIdAsync(taskId, cancellationToken);
        }

        public Task<List<TaskEntity>?> GetTasksAsync(int offset, int take, CancellationToken cancellationToken)
        {
            return _taskRepository.GetTasksAsync(offset, take, cancellationToken);
        }

        public Task<TaskEntity?> CreateTaskAsync(TaskEntity task, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(task.Name))
            {
                task.CreationDate = task.ModifiedDate = DateTime.UtcNow;

                return _taskRepository.CreateTaskAsync(task, cancellationToken);
            }

            throw new TaskNameIsNullException("Название задачи не может быть пустым.");
        }

        public Task<TaskEntity?> EditTaskAsync(int id, TaskEntity task, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(task.Name))
            {
                task.Id = id;
                task.ModifiedDate = DateTime.UtcNow;

                return _taskRepository.EditTaskAsync(task, cancellationToken);
            }

            throw new TaskNameIsNullException("Название задачи не может быть пустым.");
        }

        public Task DeleteTaskAsync(int id, CancellationToken cancellationToken)
        {
            return _taskRepository.RemoveTaskAsync(id, cancellationToken);
        }
    }
}
