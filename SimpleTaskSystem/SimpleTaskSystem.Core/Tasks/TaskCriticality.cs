using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace SimpleTaskSystem.Tasks
{
    /// <summary>
    /// </summary>
    [Table("StsTaskCriticalities")]
    public class TaskCriticality : Entity
    {
        /// <summary>
        /// Describe the criticality of the task. This is for the Telstra test requirement.
        /// </summary>
        public virtual string Value { get; set; }

        /// <summary>
        /// Default empty constructor.
        /// </summary>
        public TaskCriticality()
        {
        }
    }
}
