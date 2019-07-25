using System.Collections.Generic;

namespace ProjectManager.Entities.DTO
{
    public class TaskLookupDTO
    {
        public List<KeyValuePair<int, string>> Projects { get; set; }

        public List<KeyValuePair<int, string>> ParentTasks { get; set; }

        public List<KeyValuePair<int, string>> Users { get; set; }
    }
}
