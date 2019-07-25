namespace ProjectManager.Entities
{
    public class Messages
    {
        public const string SAVE_SUCCESS = "has been saved successfully.";

        public const string DELETE_SUCCESS = "Deleted successfully.";

        public const string USER_DELTE_FAILURE = "Cannot delete user. User is tagged to active projects/tasks.";

        public const string PROJECT_SUSPENDED_SUCCESS = "Project suspended successfully.";

        public const string PROJECT_SUSPENDED_FAILURE = "Cannot suspend project. Project is mapped to active tasks.";

        public const string TASK_END_SUCCESS = "Task has been ended.";
    }

    public class ValidationMessages
    {
        public const string PROJECT_NAME_REQUIRED = "Project Name is required.";

        public const string USER_FIRST_NAME_REQUIRED = "Firstname is required.";
        public const string USER_LAST_NAME_REQUIRED = "Lastname is required.";
        public const string USER_EMPLOYEEID_REQUIRED = "EmployeeID is required.";

        public const string TASK_NAME_REQUIRED = "Task name is required.";

        public const string PRIORITY_REQUIRED = "Priority is required.";
        public const string PRIORITY_RANGE = "Priority value is out of range, must be between 0 and 30.";
    }
}
