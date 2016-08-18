using SimpleTaskSystem.Tasks.Dtos;
using Abp.Domain.Repositories;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleTaskSystem.Tasks
{
    public class TaskReferenceAppService : ITaskReferenceAppService
    {
        private readonly IRepository<TaskCriticality> _taskCriticalityRepository;

        /// <summary>
        ///In constructor, we can get needed classes/interfaces.
        ///They are sent here by dependency injection system automatically.
        /// </summary>
        public TaskReferenceAppService(IRepository<TaskCriticality> taskCriticalityRepository)
        {
            _taskCriticalityRepository = taskCriticalityRepository;
        }

        //This method uses async pattern that is supported by ASP.NET Boilerplate
        public async Task<GetTasksCriticalitiesOutput> GetTaskCriticalities()
        {
            var taskCriticalities = await _taskCriticalityRepository.GetAllListAsync();
            var output = new GetTasksCriticalitiesOutput
            {
                TaskCriticalities = Mapper.Map<List<TaskCriticalityDto>>(taskCriticalities)
            };
            return output;
        }
    }
}
