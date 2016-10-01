using Abp.Application.Services;
using SimpleTaskSystem.Tasks.Dtos;

namespace SimpleTaskSystem.Tasks
{
    /// <summary>
    /// Defines an application service for <see cref="Task"/> operations.
    /// 
    /// It extends <see cref="IApplicationService"/>.
    /// Thus, ABP enables automatic dependency injection, validation and other benefits for it.
    /// 
    /// Application services works with DTOs (Data Transfer Objects).
    /// Service methods gets and returns DTOs.
    /// </summary>
    public interface ITaskAppService : IApplicationService
    {
        GetTasksOutput GetTasks(GetTasksQuery input);
        
        void UpdateTask(UpdateTaskInput input);

        void CreateTask(CreateTaskInput input);

        void DeleteTask(DeleteTaskInput input);

        /// <summary>
        /// Switch task state switches a task from active to completed, or vice versa.
        /// Only non deleted task can be updated.
        /// </summary>
        /// <param name="input"></param>
        void SwitchTaskState(SwitchTaskStateInput input);

    }
}
