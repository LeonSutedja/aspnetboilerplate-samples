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
        /// Describe the criticality of the task.
        /// </summary>
        public virtual string Value { get; set; }

        /// <summary>
        /// Default costructor.
        /// </summary>
        public TaskCriticality()
        {
        }
    }
}
