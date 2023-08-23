﻿using DataAccessLayer.DataContext;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class UserRepository : IRepository<UserInfo>
    {
        private UserContext _userContext = null!;

        public UserRepository(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder().UseSqlServer(connectionString);
            this._userContext = new UserContext(optionsBuilder.Options);
        }

        public void Add(UserInfo value)
        {
            this._userContext.Users.Add(value);
            this._userContext.SaveChanges();
        }

        public IEnumerable<UserInfo> GetAll()
        {
            return this._userContext.Users;
        }

        public void Remove(UserInfo value)
        {
            this._userContext.Remove(value);
        }

        public void Update(UserInfo value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var temp = this._userContext.Users.Find(value.Id);
            if (temp is null)
                throw new ArgumentException($"There is no record with this ID {value.Id}", nameof(value));

            temp.Id = value.Id;
            temp.Login = value.Login;
            temp.Password = value.Password;
            temp.Name = value.Name;
            temp.LastName = value.LastName;
            temp.IsAdmin = value.IsAdmin;
            this._userContext.SaveChanges();
        }
    }
}