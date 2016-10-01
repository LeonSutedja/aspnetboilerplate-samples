namespace SimpleTaskSystem.Tasks.Dtos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Abp.Runtime.Validation;
    using Shared;
    using Abp.Domain.Repositories;
    using People;    /// <summary>
                     /// This DTO class is used to send needed data to <see cref="ITaskAppService.UpdateTask"/> method.
                     /// 
                     /// Implements <see cref="ICustomValidate"/> for additional custom validation.
                     /// </summary>
    public class UpdateTaskInput : ICustomValidate
    {
        [Range(1, long.MaxValue)] //Data annotation attributes work as expected.
        public long TaskId { get; set; }

        public int? AssignedPersonId { get; set; }

        public int? TaskCriticalityId { get; set; }

        public TaskState? State { get; set; }

        //Custom validation method. It's called by ABP after data annotation validations.
        public void AddValidationErrors(List<ValidationResult> results)
        {
            if (AssignedPersonId == null && State == null)
            {
                results.Add(new ValidationResult("Both of AssignedPersonId and State can not be null in order to update a Task!", new[] { "AssignedPersonId", "State" }));
            }
        }

        public override string ToString()
        {
            return string.Format("[UpdateTaskInput > TaskId = {0}, AssignedPersonId = {1}, State = {2}]", TaskId, AssignedPersonId, State);
        }
    }


    public class UpdateTaskInputHandler : IInputHandler<UpdateTaskInput>
    {
        private ITaskRepository _taskRepository;
        private IRepository<Person> _personRepository;

        public UpdateTaskInputHandler(ITaskRepository taskRepository,
            IRepository<Person> personRepository)
        {
            _taskRepository = taskRepository;
            _personRepository = personRepository;
        }
        public void Handle(UpdateTaskInput input)
        {
            //Retrieving a task entity with given id using standard Get method of repositories.
            var task = _taskRepository.Get(input.TaskId);

            //Updating changed properties of the retrieved task entity.

            if (input.State.HasValue)
            {
                task.State = input.State.Value;
            }

            if (input.AssignedPersonId.HasValue)
            {
                task.AssignedPerson = _personRepository.Load(input.AssignedPersonId.Value);
            }

            //We even do not call Update method of the repository.
            //Because an application service method is a 'unit of work' scope as default.
            //ABP automatically saves all changes when a 'unit of work' scope ends (without any exception).
        }
    }
}
