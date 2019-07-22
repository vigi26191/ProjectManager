using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Entities.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ValidationMessages.USER_FIRST_NAME_REQUIRED)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ValidationMessages.USER_LAST_NAME_REQUIRED)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ValidationMessages.USER_EMPLOYEEID_REQUIRED)]
        public int EmployeeId { get; set; }
    }
}
