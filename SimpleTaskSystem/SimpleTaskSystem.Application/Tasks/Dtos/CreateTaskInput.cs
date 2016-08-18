using System.ComponentModel.DataAnnotations;

namespace SimpleTaskSystem.Tasks.Dtos
{
    public class CreateTaskInput
    {
        public int? AssignedPersonId { get; set; }

        public int? TaskCriticalityId { get; set; }

        [Required]
        public string Description { get; set; }

        public override string ToString()
        {
            return string.Format("[CreateTaskInput > AssignedPersonId = {0}, TaskCriticalityId = {1}, Description = {2}]", AssignedPersonId, TaskCriticalityId, Description);
        }
    }
}