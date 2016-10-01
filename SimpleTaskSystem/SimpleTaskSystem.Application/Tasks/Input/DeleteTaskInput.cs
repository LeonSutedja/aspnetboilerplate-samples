namespace SimpleTaskSystem.Tasks.Dtos
{
    using Shared;
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// </summary>
    public class DeleteTaskInput
    {
        [Range(1, long.MaxValue)] //Data annotation attributes work as expected.
        public long TaskId { get; set; }
    }

    public class DeleteTaskInputHandler : IInputHandler<DeleteTaskInput>
    {
        private ITaskRepository _taskRepository;

        public DeleteTaskInputHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public void Handle(DeleteTaskInput input)
        {
            var task = _taskRepository.Get(input.TaskId);

            if (task.State != TaskState.Completed) throw new Exception("Cannot delete incomplete task.");

            task.State = TaskState.Deleted;

            //Saving entity with standard Update method of repositories.
            _taskRepository.Update(task);
        }
    }
}
