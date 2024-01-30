using AutoMapper;
using TaskTracker.Abstractions.Entities;
using TaskTracker.WebApi.Contracts;

namespace TaskTracker.MappingProfiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskEntity, TaskDto>();
            CreateMap<TaskDto, TaskEntity>();

            CreateMap<TaskCreationDto, TaskEntity>();
        }
    }
}
