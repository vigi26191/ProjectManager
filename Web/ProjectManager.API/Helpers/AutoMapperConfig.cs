using AutoMapper;
using ProjectManager.Entities.Domain;
using ProjectManager.Entities.DTO;

namespace ProjectManager.API.Helpers
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize((config) =>
            {
                config.CreateMap<User, UserDTO>().ReverseMap();

                config.CreateMap<ProjectDTO, Project>();

                config.CreateMap<Project, ProjectDTO>()
               .ForMember(dest => dest.UserName,
                          opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));

                config.CreateMap<TaskDTO, Task>();

                config.CreateMap<Task, TaskDTO>()
                .ForMember(dest => dest.ParentTaskName,
                           opt => opt.MapFrom(src => src.ParentTask != null ? src.ParentTask.TaskName : ""));
            });
        }
    }
}