using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class UserService : IService<User>
    {
        private IRepository<UserInfo> _userRepository = null!;

        public UserService(string connectionString)
        {
            this._userRepository = new UserRepository(connectionString);
        }

        private User TranslateToUserModel(UserInfo user)
        {
            var mapObject = new MapperConfiguration(map => map.CreateMap<UserInfo, User>())
                .CreateMapper();
            return mapObject.Map<UserInfo, User>(user);
        }

        private UserInfo TranslateToUserInfo(User user)
        {
            var mapObject = new MapperConfiguration(map => map.CreateMap<User, UserInfo>())
               .CreateMapper();
            return mapObject.Map<User, UserInfo>(user);
        }

        public void Add(User value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            this._userRepository.Add(TranslateToUserInfo(value));
        }

        public IEnumerable<User> GetAll()
        {
            return this._userRepository.GetAll().Select(u =>  TranslateToUserModel(u));
        }

        public void Remove(User value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            this._userRepository.Remove(TranslateToUserInfo(value));
        }

        public void Update(User value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            this._userRepository.Update(TranslateToUserInfo(value));
        }

        public bool LoginExists(string login)
        {
            return GetAll().Select(u => u.Login).Contains(login);
        }
    }
}
