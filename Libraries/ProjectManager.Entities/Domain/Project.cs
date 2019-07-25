using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Entities.Domain
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ValidationMessages.PROJECT_NAME_REQUIRED)]
        public string ProjectName { get; set; }

        public DateTime? ProjectStartDate { get; set; }

        public DateTime? ProjectEndDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ValidationMessages.PRIORITY_REQUIRED)]
        [Range(minimum: 0, maximum: 30, ErrorMessage = ValidationMessages.PRIORITY_RANGE)]
        public int ProjectPriority { get; set; }

        public bool? IsProjectSuspended { get; set; }

        #region Navigation Properties

        public User User { get; set; }

        public int UserId { get; set; }

        public ICollection<Task> Tasks { get; set; }

        #endregion
    }
}