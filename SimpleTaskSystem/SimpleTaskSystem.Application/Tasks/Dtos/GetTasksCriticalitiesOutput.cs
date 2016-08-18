using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskSystem.Tasks.Dtos
{
    public class GetTasksCriticalitiesOutput
    {
        public List<TaskCriticalityDto> TaskCriticalities { get; set; }
    }
}
