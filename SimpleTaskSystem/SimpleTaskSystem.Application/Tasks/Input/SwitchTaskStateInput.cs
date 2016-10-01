namespace SimpleTaskSystem.Tasks.Dtos
{
    using System.ComponentModel.DataAnnotations;
    using SimpleTaskSystem.Shared;

    /// <summary>
    /// </summary>
    public class SwitchTaskStateInput
    {
        [Range(1, long.MaxValue)] //Data annotation attributes work as expected.
        public long TaskId { get; set; }
    }

    public class SwitchTaskStateInputHandler : IInputHandler<SwitchTaskStateInput>
    {
        private ITaskRepository _taskRepository;

        public SwitchTaskStateInputHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public void Handle(SwitchTaskStateInput input)
        {
            //Retrieving a task entity with given id using standard Get method of repositories.
            var task = _taskRepository.Get(input.TaskId);

            //Updating changed properties of the retrieved task entity.
            if (task.State == TaskState.Active)
                task.State = TaskState.Completed;
            else if (task.State == TaskState.Completed)
                task.State = TaskState.Active;

            //We even do not call Update method of the repository.
            //Because an application service method is a 'unit of work' scope as default.
            //ABP automatically saves all changes when a 'unit of work' scope ends (without any exception).
        }
    }
}
