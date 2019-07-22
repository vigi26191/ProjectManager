using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Entities.Domain
{
    public class User
    {
        public int UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ValidationMessages.USER_FIRST_NAME_REQUIRED)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ValidationMessages.USER_LAST_NAME_REQUIRED)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ValidationMessages.USER_EMPLOYEEID_REQUIRED)]
        public int EmployeeId { get; set; }

        #region Navigation Properties

        public Project Project { get; set; }

        public int? ProjectId { get; set; }

        #endregion
    }
}
