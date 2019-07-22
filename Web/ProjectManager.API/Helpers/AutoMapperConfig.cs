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
            });
        }
    }
}