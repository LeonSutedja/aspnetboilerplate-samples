using System.Linq;
using Abp.Runtime.Validation;
using Shouldly;
using SimpleTaskSystem.People;
using SimpleTaskSystem.Tasks;
using SimpleTaskSystem.Tasks.Dtos;
using Xunit;

namespace SimpleTaskSystem.Test.Tasks
{
    public class TaskRefAppService_Tests : SimpleTaskSystemTestBase
    {
        private readonly ITaskReferenceAppService _taskRefAppService;

        public TaskRefAppService_Tests()
        {
            //Creating the class which is tested (SUT - Software Under Test)
            _taskRefAppService = LocalIocManager.Resolve<ITaskReferenceAppService>();
        }

        [Fact]
        public void Should_Get_TaskCriticalities()
        {
            //Run SUT
            var output = _taskRefAppService.GetTaskCriticalities();

            //Checking results
            output.Result.TaskCriticalities.Count.ShouldBe(3);
        }       
    }
}
