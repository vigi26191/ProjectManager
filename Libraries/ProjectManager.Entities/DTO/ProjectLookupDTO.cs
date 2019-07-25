using System.Collections.Generic;

namespace ProjectManager.Entities.DTO
{
    public class ProjectLookupDTO
    {
        public List<KeyValuePair<int, string>> Users { get; set; }

        public List<ProjectDTO> Projects { get; set; }
    }
}
