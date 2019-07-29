using AutoMapper;
using ProjectManager.Entities.Domain;
using ProjectManager.Entities.DTO;

namespace ProjectManager.API.Tests
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<ProjectDTO, Project>();

            CreateMap<Project, ProjectDTO>()
           .ForMember(dest => dest.UserName,
                      opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));

            CreateMap<TaskDTO, Task>();

            CreateMap<Task, TaskDTO>()
            .ForMember(dest => dest.ParentTaskName,
                       opt => opt.MapFrom(src => src.ParentTask != null ? src.ParentTask.TaskName : ""));
        }
    }
}
