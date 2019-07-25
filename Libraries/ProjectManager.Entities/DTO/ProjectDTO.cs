using System;

namespace ProjectManager.Entities.DTO
{
    public class ProjectDTO
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public DateTime? ProjectStartDate { get; set; }

        public DateTime? ProjectEndDate { get; set; }

        public int ProjectPriority { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public bool? IsProjectSuspended { get; set; }
    }
}
