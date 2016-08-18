using System;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using AutoMapper;
using SimpleTaskSystem.People;
using SimpleTaskSystem.Tasks.Dtos;

namespace SimpleTaskSystem.Tasks
{
    /// <summary>
    /// Implements <see cref="ITaskAppService"/> to perform task related application functionality.
    /// 
    /// Inherits from <see cref="ApplicationService"/>.
    /// <see cref="ApplicationService"/> contains some basic functionality common for application services (such as logging and localization).
    /// </summary>
    public class TaskAppService : ApplicationService, ITaskAppService
    {
        //These members set in constructor using constructor injection.
        
        private readonly ITaskRepository _taskRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<TaskCriticality> _taskCriticalityRepository;

        /// <summary>
        ///In constructor, we can get needed classes/interfaces.
        ///They are sent here by dependency injection system automatically.
        /// </summary>
        public TaskAppService(ITaskRepository taskRepository, IRepository<Person> personRepository, IRepository<TaskCriticality> taskCriticalityRepository)
        {
            _taskRepository = taskRepository;
            _personRepository = personRepository;
            _taskCriticalityRepository = taskCriticalityRepository;
        }
        
        public GetTasksOutput GetTasks(GetTasksInput input)
        {
            //Called specific GetAllWithPeople method of task repository.
            var tasks = _taskRepository.GetAllWithPeople(input.AssignedPersonId, input.State);

            //Used AutoMapper to automatically convert List<Task> to List<TaskDto>.
            return new GetTasksOutput
                   {
                       Tasks = Mapper.Map<List<TaskDto>>(tasks)
                   };
        }
        
        public void UpdateTask(UpdateTaskInput input)
        {
            //We can use Logger, it's defined in ApplicationService base class.
            Logger.Info("Updating a task for input: " + input);

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

        public void SwitchTaskState(SwitchTaskStateInput input)
        {
            //We can use Logger, it's defined in ApplicationService base class.
            Logger.Info("Switch a task state: " + input);

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

        public void CreateTask(CreateTaskInput input)
        {
            //We can use Logger, it's defined in ApplicationService class.
            Logger.Info("Creating a task for input: " + input);

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

        public void DeleteTask(DeleteTaskInput input)
        {
            Logger.Info("Deleting Task with id: " + input.TaskId);
            var task = _taskRepository.Get(input.TaskId);

            if (task.State != TaskState.Completed) throw new Exception("Cannot delete incomplete task.");

            task.State = TaskState.Deleted;

            //Saving entity with standard Update method of repositories.
            _taskRepository.Update(task);            
        }         
    }
}