using System.ComponentModel.DataAnnotations;
using SimpleTaskSystem.InputHandler;

namespace SimpleTaskSystem.Tasks.Dtos
{
    /// <summary>
    /// </summary>
    public class DeleteTaskInput
    {
        [Range(1, long.MaxValue)] //Data annotation attributes work as expected.
        public long TaskId { get; set; }
    }    
}
