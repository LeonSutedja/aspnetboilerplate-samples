namespace SimpleTaskSystem.Tasks.Dtos
{
    using System.ComponentModel.DataAnnotations;
    using Abp.Domain.Repositories;
    using SimpleTaskSystem.People;
    using SimpleTaskSystem.Shared;

    public class CreateTaskInput
    {
        public int? AssignedPersonId { get; set; }

        public int? TaskCriticalityId { get; set; }

        [Required]
        public string Description { get; set; }

        public override string ToString()
        {
            return string.Format("[CreateTaskInput > AssignedPersonId = {0}, TaskCriticalityId = {1}, Description = {2}]", AssignedPersonId, TaskCriticalityId, Description);
        }
    }

    public class CreateTaskInputHandler : IInputHandler<CreateTaskInput>
    {
        private ITaskRepository _taskRepository;
        private IRepository<Person> _personRepository;
        private IRepository<TaskCriticality> _taskCriticalityRepository;

        public CreateTaskInputHandler(ITaskRepository taskRepository,
            IRepository<Person> personRepository,
            IRepository<TaskCriticality> taskCriticalityRepository)
        {
            _taskRepository = taskRepository;
            _personRepository = personRepository;
            _taskCriticalityRepository = taskCriticalityRepository;
        }
        public void Handle(CreateTaskInput input)
        {
            //Creating a new Task entity with given input's properties
            var task = new Task { Description = input.Description };

            if (input.AssignedPersonId.HasValue)
            {
                task.AssignedPerson = _personRepository.Load(input.AssignedPersonId.Value);
            }

            if (input.TaskCriticalityId.HasValue)
            {
                task.TaskCriticality = _taskCriticalityRepository.Load(input.TaskCriticalityId.Value);
            }

            //Saving entity with standard Insert method of repositories.
            _taskRepository.Insert(task);
        }
    }
}