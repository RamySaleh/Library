﻿using Library.DAL;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BAL
{
    public class UserService : IUserService
    {
        string connectionString;
        public UserService(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public bool RegisterUser(User user)
        {
            var userRepo = new UserRepo(connectionString);
            return userRepo.Register(user);
        }

        public User Login(User user)
        {
            var userRepo = new UserRepo(connectionString);
            return userRepo.Login(user);
        }
    }
}
