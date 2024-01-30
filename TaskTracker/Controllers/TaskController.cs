using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Abstractions.Entities;
using TaskTracker.Abstractions.Services;
using TaskTracker.WebApi.Contracts;

namespace TaskTracker.WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TaskController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("tasks/{id}")]
        public async Task<IActionResult> GetTaskById(int id, CancellationToken cancellationToken)
        {
            var task = await _taskService.GetTaskByIdAsync(id, cancellationToken);

            return task != null ? Ok(_mapper.Map<TaskDto>(task)) : NotFound();
        }

        [HttpGet]
        [Route("tasks")]
        public async Task<IActionResult> GetTasks(int? offset, int? take, CancellationToken cancellationToken)
        {
            var tasks = await _taskService.GetTasksAsync(offset ?? 0, take ?? int.MaxValue, cancellationToken);

            return Ok((tasks ?? new List<TaskEntity>()).Select(x => _mapper.Map<TaskDto>(x)).ToList());
        }

        [HttpPost]
        [Route("tasks")]
        public async Task<IActionResult> CreateTask(TaskCreationDto task, CancellationToken cancellationToken)
        {
            var taskEntity = _mapper.Map<TaskEntity>(task);
            var createdTask = await _taskService.CreateTaskAsync(taskEntity, cancellationToken);

            return Ok(_mapper.Map<TaskDto>(createdTask));
        }

        [HttpPut]
        [Route("tasks/{id}")]
        public async Task<IActionResult> EditTask(int id, TaskCreationDto task, CancellationToken cancellationToken)
        {
            var taskEntity = _mapper.Map<TaskEntity>(task);
            var modifiedTask = await _taskService.EditTaskAsync(id, taskEntity, cancellationToken);

            return modifiedTask != null ? Ok(_mapper.Map<TaskDto>(modifiedTask)) : NotFound();
        }

        [HttpDelete]
        [Route("tasks/{id}")]
        public async Task<IActionResult> DeleteTask(int id, CancellationToken cancellationToken)
        {
            await _taskService.DeleteTaskAsync(id, cancellationToken);

            return Ok();
        }
    }
}
