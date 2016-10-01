namespace SimpleTaskSystem.Tasks.Dtos
{
    using AutoMapper;
    using Shared;
    using System.Collections.Generic;

    public class GetTasksQuery
    {
        public TaskState? State { get; set; }

        public int? AssignedPersonId { get; set; }
    }

    public class GetTasksOutput
    {
        public List<TaskDto> Tasks { get; set; }
    }

    public class GetTasksQueryHandler : IQueryHandler<GetTasksQuery, GetTasksOutput>
    {
        private ITaskRepository _taskRepository;

        public GetTasksQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public GetTasksOutput Handle(GetTasksQuery input)
        {
            //Called specific GetAllWithPeople method of task repository.
            var tasks = _taskRepository.GetAllWithPeople(input.AssignedPersonId, input.State);

            //Used AutoMapper to automatically convert List<Task> to List<TaskDto>.
            return new GetTasksOutput
            {
                Tasks = Mapper.Map<List<TaskDto>>(tasks)
            };
        }
    }
}