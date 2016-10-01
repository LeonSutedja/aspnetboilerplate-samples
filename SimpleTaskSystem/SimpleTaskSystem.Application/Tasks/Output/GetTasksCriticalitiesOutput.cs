namespace SimpleTaskSystem.Tasks.Dtos
{
    using System.Collections.Generic;

    public class GetTasksCriticalitiesOutput
    {
        public List<TaskCriticalityDto> TaskCriticalities { get; set; }
    }
}
