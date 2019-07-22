using AutoMapper;
using ProjectManager.DAL;
using ProjectManager.DAL.Concretes;
using ProjectManager.Entities.Domain;
using ProjectManager.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManager.BAL
{
    public class UserBAL
    {
        public UserDTO GetUser(int userId)
        {
            UserDTO user = null;

            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                user = Mapper.Map<UserDTO>(unitOfWork.Users.Get(userId));
            }

            return user;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            List<UserDTO> users = null;

            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                users = Mapper.Map<List<UserDTO>>(unitOfWork.Users.GetAll().ToList());
            }

            return users;
        }

        public bool SaveUser(UserDTO user)
        {
            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                var userInDB = unitOfWork.Users.Get(user.UserId);

                if (userInDB == null)
                {
                    var newUser = Mapper.Map<User>(user);
                    unitOfWork.Users.Add(newUser);
                }
                else
                {
                    Mapper.Map(user, userInDB);
                }

                try
                {
                    return unitOfWork.Complete() > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool DeleteUser(int userId)
        {
            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                var userInDB = unitOfWork.Users.Get(userId);

                if (userInDB == null)
                {
                    return false;
                }
                else
                {
                    unitOfWork.Users.Remove(userInDB);
                }

                try
                {
                    return unitOfWork.Complete() > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
