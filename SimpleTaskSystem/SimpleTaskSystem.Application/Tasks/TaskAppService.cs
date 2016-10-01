namespace SimpleTaskSystem.Tasks
{
    using System.Collections.Generic;
    using Abp.Application.Services;
    using AutoMapper;
    using SimpleTaskSystem.Tasks.Dtos;
    using SimpleTaskSystem.Shared;

    /// <summary>
    /// Implements <see cref="ITaskAppService"/> to perform task related application functionality.
    /// 
    /// Inherits from <see cref="ApplicationService"/>.
    /// <see cref="ApplicationService"/> contains some basic functionality common for application services (such as logging and localization).
    /// </summary>
    public class TaskAppService : ApplicationService, ITaskAppService
    {
        //These members set in constructor using constructor injection.
        
        private readonly IQueryHandlerFactory _queryHandlerFactory;
        private readonly IInputHandlerFactory _inputHandlerFactory;

        /// <summary>
        ///In constructor, we can get needed classes/interfaces.
        ///They are sent here by dependency injection system automatically.
        /// </summary>
        public TaskAppService(
            IInputHandlerFactory inputHandlerFactory,
            IQueryHandlerFactory queryHandlerFactory)
        {
            _inputHandlerFactory = inputHandlerFactory;
            _queryHandlerFactory = queryHandlerFactory;
        }
        
        public GetTasksOutput GetTasks(GetTasksQuery input)
        {
            var handler = _queryHandlerFactory.CreateHandler<GetTasksQuery, GetTasksOutput>();
            return handler.Handle(input);
        }
        
        public void UpdateTask(UpdateTaskInput input)
        {
            var handler = _inputHandlerFactory.CreateInputHandler<UpdateTaskInput>();
            handler.Handle(input);

            //We can use Logger, it's defined in ApplicationService base class.
            Logger.Info("Updated a task for input: " + input);            
        }

        public void SwitchTaskState(SwitchTaskStateInput input)
        {
            var handler = _inputHandlerFactory.CreateInputHandler<SwitchTaskStateInput>();
            handler.Handle(input);

            //We can use Logger, it's defined in ApplicationService base class.
            Logger.Info("Switched a task state: " + input);
        }

        public void CreateTask(CreateTaskInput input)
        {
            var handler = _inputHandlerFactory.CreateInputHandler<CreateTaskInput>();
            handler.Handle(input);

            //We can use Logger, it's defined in ApplicationService class.
            Logger.Info("Created a task for input: " + input);
        }

        public void DeleteTask(DeleteTaskInput input)
        {
            var handler = _inputHandlerFactory.CreateInputHandler<DeleteTaskInput>();
            handler.Handle(input);

            Logger.Info("Deleted Task with id: " + input.TaskId);
                     
        }         
    }
}