using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Entities.Domain
{
    public class Task
    {
        public int TaskId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ValidationMessages.TASK_NAME_REQUIRED)]
        public string TaskName { get; set; }

        [Range(minimum: 0, maximum: 30, ErrorMessage = ValidationMessages.PRIORITY_RANGE)]
        public int? TaskPriority { get; set; }

        public bool? IsParentTask { get; set; }

        public DateTime? TaskStartDate { get; set; }

        public DateTime? TaskEndDate { get; set; }

        public bool? IsTaskComplete { get; set; }

        #region Navigation Properties

        public Project Project { get; set; }

        public int ProjectId { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public Task ParentTask { get; set; }

        public int? ParentTaskId { get; set; }

        #endregion
    }
}
