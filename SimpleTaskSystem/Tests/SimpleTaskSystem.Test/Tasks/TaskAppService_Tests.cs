using System.Linq;
using Abp.Runtime.Validation;
using Shouldly;
using SimpleTaskSystem.People;
using SimpleTaskSystem.Tasks;
using SimpleTaskSystem.Tasks.Dtos;
using Xunit;
using System.Collections.Generic;
using System;

namespace SimpleTaskSystem.Test.Tasks
{
    public class TaskAppService_Tests : SimpleTaskSystemTestBase
    {
        private readonly ITaskAppService _taskAppService;

        public TaskAppService_Tests()
        {
            //Creating the class which is tested (SUT - Software Under Test)
            _taskAppService = LocalIocManager.Resolve<ITaskAppService>();
        }

        [Fact]
        public void Should_Get_Tasks()
        {
            //Run SUT
            var output = _taskAppService.GetTasks(new GetTasksQuery { State = TaskState.Completed });

            //Checking results
            output.Tasks.Count.ShouldBe(2);
            output.Tasks.All(t => t.State == (byte)TaskState.Completed).ShouldBe(true);
        }

        [Fact]
        public void Should_Create_New_Tasks()
        {
            //Prepare for test
            var initialTaskCount = UsingDbContext(context => context.Tasks.Count());
            var thomasMore = GetPerson("Thomas More");

            //Run SUT
            _taskAppService.CreateTask(
                new CreateTaskInput
                {
                    Description = "my test task 1"
                });
            _taskAppService.CreateTask(
                new CreateTaskInput
                {
                    Description = "my test task 2",
                    AssignedPersonId = thomasMore.Id
                });
            
            //Check results
            UsingDbContext(context =>
            {
                context.Tasks.Count().ShouldBe(initialTaskCount + 2);
                context.Tasks.FirstOrDefault(t => t.AssignedPersonId == null && t.Description == "my test task 1").ShouldNotBe(null);
                var task2 = context.Tasks.FirstOrDefault(t => t.Description == "my test task 2");
                task2.ShouldNotBe(null);
                task2.AssignedPersonId.ShouldBe(thomasMore.Id);
            });
        }

        [Fact]
        public void Should_Create_New_Tasks_With_Criticality()
        {
            //Prepare for test
            var initialTaskCount = UsingDbContext(context => context.Tasks.Count());
            var tasksCriticalityId = GetTaskCriticalities().First().Id;
            var taskCriticalityDescription = "My test task criticality description";

            //Run SUT            
            _taskAppService.CreateTask(
                new CreateTaskInput
                {
                    Description = taskCriticalityDescription,
                    TaskCriticalityId = tasksCriticalityId
                });

            //Check results
            UsingDbContext(context =>
            {
                context.Tasks.Count().ShouldBe(initialTaskCount + 1);                
                var taskCriticality = context.Tasks.FirstOrDefault(t => t.Description == taskCriticalityDescription);
                taskCriticality.ShouldNotBe(null);
                taskCriticality.TaskCriticalityId.ShouldBe(tasksCriticalityId);
            });
        }

        [Fact]
        public void Should_Not_Create_Task_Without_Description()
        {
            //Description is not set
            Assert.Throws<AbpValidationException>(() => _taskAppService.CreateTask(new CreateTaskInput()));
        }
        
        //Trying to assign a task of Isaac Asimov to Thomas More
        [Fact]
        public void Should_Change_Assigned_People()
        {
            //We can work with repositories instead of DbContext
            var taskRepository = LocalIocManager.Resolve<ITaskRepository>();

            //Obtain test data
            var isaacAsimov = GetPerson("Isaac Asimov");
            var thomasMore = GetPerson("Thomas More");
            var targetTask = taskRepository.FirstOrDefault(t => t.AssignedPersonId == isaacAsimov.Id);
            targetTask.ShouldNotBe(null);

            //Run SUT
            _taskAppService.UpdateTask(
                new UpdateTaskInput
                {
                    TaskId = targetTask.Id,
                    AssignedPersonId = thomasMore.Id
                });

            //Check result
            taskRepository.Get(targetTask.Id).AssignedPersonId.ShouldBe(thomasMore.Id);
        }

        [Fact]
        public void Should_Delete_Completed_Task()
        {
            //We can work with repositories instead of DbContext
            var taskRepository = LocalIocManager.Resolve<ITaskRepository>();

            //Obtain test data
            var targetTask = taskRepository.FirstOrDefault(t => t.State == TaskState.Completed);
            targetTask.ShouldNotBe(null);

            var taskId = targetTask.Id;
            
            //Run SUT
            _taskAppService.DeleteTask(
                new DeleteTaskInput
                {
                    TaskId = taskId
                });

            //Check result
            taskRepository.Get(taskId).State.ShouldBe(TaskState.Deleted);
        }
        
        [Fact]
        public void Should_Throw_Exception_On_Incomplete_Task_Delete()
        {
            //We can work with repositories instead of DbContext
            var taskRepository = LocalIocManager.Resolve<ITaskRepository>();

            //Obtain test data
            var targetTask = taskRepository.FirstOrDefault(t => t.State == TaskState.Active);
            targetTask.ShouldNotBe(null);

            var taskId = targetTask.Id;

            //Run SUT
            Should.Throw<Exception>(() =>
            {
                _taskAppService.DeleteTask(
                        new DeleteTaskInput
                        {
                            TaskId = taskId
                        });
            });
        }

        [Fact]
        public void Should_Switch_Task_State_From_Completed_To_Active()
        {
            //We can work with repositories instead of DbContext
            var taskRepository = LocalIocManager.Resolve<ITaskRepository>();

            //Obtain test data
            var targetTask = taskRepository.FirstOrDefault(t => t.State == TaskState.Completed);
            targetTask.ShouldNotBe(null);

            //Run SUT
            _taskAppService.SwitchTaskState(
                new SwitchTaskStateInput
                {
                    TaskId = targetTask.Id
                });

            //Check result
            taskRepository.Get(targetTask.Id).State.ShouldBe(TaskState.Active);
        }

        [Fact]
        public void Should_Switch_Task_State_From_Active_To_Completed()
        {
            //We can work with repositories instead of DbContext
            var taskRepository = LocalIocManager.Resolve<ITaskRepository>();

            //Obtain test data
            var targetTask = taskRepository.FirstOrDefault(t => t.State == TaskState.Active);
            targetTask.ShouldNotBe(null);

            //Run SUT
            _taskAppService.SwitchTaskState(
                new SwitchTaskStateInput
                {
                    TaskId = targetTask.Id
                });

            //Check result
            taskRepository.Get(targetTask.Id).State.ShouldBe(TaskState.Completed);
        }

        private Person GetPerson(string name)
        {
            return UsingDbContext(context => context.People.Single(p => p.Name == name));
        }

        private List<TaskCriticality> GetTaskCriticalities()
        {
            return UsingDbContext(context => context.TaskCriticalities.ToList());
        }
    }
}
