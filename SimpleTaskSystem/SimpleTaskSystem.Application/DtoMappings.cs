using AutoMapper;
using SimpleTaskSystem.Tasks;
using SimpleTaskSystem.Tasks.Dtos;

namespace SimpleTaskSystem
{
    internal static class DtoMappings
    {
        public static void Map()
        {
            Mapper.CreateMap<Task, TaskDto>();
            Mapper.CreateMap<TaskCriticality, TaskCriticalityDto>().ForMember(t => t.TaskCriticality, opts => opts.MapFrom(t => t.Value));
        }
    }
}
