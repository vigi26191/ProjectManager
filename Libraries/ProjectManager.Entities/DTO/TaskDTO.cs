using System;

namespace ProjectManager.Entities.DTO
{
    public class TaskDTO
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public int? TaskPriority { get; set; }

        public bool? IsParentTask { get; set; }

        public DateTime? TaskStartDate { get; set; }

        public DateTime? TaskEndDate { get; set; }

        public bool? IsTaskComplete { get; set; }

        public int ProjectId { get; set; }

        public int UserId { get; set; }

        public int? ParentTaskId { get; set; }

        public string ParentTaskName { get; set; }
    }
}
