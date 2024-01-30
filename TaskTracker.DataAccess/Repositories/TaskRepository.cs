using Microsoft.EntityFrameworkCore;
using TaskTracker.Abstractions.Entities;
using TaskTracker.Abstractions.Repositories;
using TaskTracker.Db;

namespace TaskTracker.DataAccess.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskTrackerContext _context;

        public TaskRepository(TaskTrackerContext context)
        {
            _context = context;
        }

        public Task<TaskEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _context.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<TaskEntity>?> GetTasksAsync(int offset, int take, CancellationToken cancellationToken)
        {
            return await _context.Tasks
                .AsNoTracking()
                .OrderBy(t => t.Id)
                .Skip(offset)
                .Take(take)
                .ToListAsync(cancellationToken);
        }

        public async Task<TaskEntity?> CreateTaskAsync(TaskEntity task, CancellationToken cancellationToken)
        {
            await _context.Tasks.AddAsync(task, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return task;
        }

        public async Task<TaskEntity?> EditTaskAsync(TaskEntity task, CancellationToken cancellationToken)
        {
            var entity = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == task.Id, cancellationToken);

            if (entity != null)
            {
                entity.Name = task.Name;
                entity.Description = task.Description;
                entity.ModifiedDate = task.ModifiedDate;

                _context.Tasks.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity;
            }

            return null;
        }

        public async Task RemoveTaskAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

            if (entity != null)
            {
                _context.Tasks.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
