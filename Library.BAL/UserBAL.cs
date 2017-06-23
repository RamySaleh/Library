using Library.DAL;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BAL
{
    public class UserBAL
    {
        string connectionString;
        public UserBAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public bool RegisterUser(User user)
        {
            var userRepo = new UserRepo(connectionString);
            return userRepo.Register(user);
        }
    }
}
