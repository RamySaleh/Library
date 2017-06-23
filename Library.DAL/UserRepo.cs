using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;
using Library.DAL.Helpers;
using System.Data;

namespace Library.DAL
{
    public class UserRepo : IUserRepo
    {
        private ADOHelper dbHelper;        
        private const string sp_RegisterUser = "RegisterUser";
        private const string sp_LoginUser = "LoginUser";
        private const string sp_GetUserById = "GetUserById";

        public UserRepo(string connectionString)
        {
            dbHelper = new ADOHelper(connectionString);            
        }       

        public bool Register(User user)
        {
            var authers = new List<Auther>();
            var sqlParameters = new SqlParametersHelper()
                .AddParameter("@userName", user.Name, SqlDbType.NVarChar)
                .AddParameter("@email", user.Email, SqlDbType.NVarChar)
                .GetParameters();

            var result = dbHelper.ExecuteProcedure(sp_RegisterUser, sqlParameters);

            return result;
        }

        public User Login(User user)
        {
            var loggedInUser = (User)null;
            var sqlParameters = new SqlParametersHelper()             
               .AddParameter("@email", user.Email, SqlDbType.NVarChar)
               .GetParameters();

            dbHelper.ExecuteProcedure(sp_LoginUser, (reader) =>
            {
                while (reader.Read())
                {
                    loggedInUser = new User
                    {
                        Id = (int)reader[0],
                        Name = reader[1].ToString(),
                        Email = reader[2].ToString()
                    };
                }
            }, sqlParameters);

            return loggedInUser;
        }

        public User GetUserById(int userId)
        {
            var user = (User)null;
            var sqlParameters = new SqlParametersHelper()
               .AddParameter("@userId", userId, SqlDbType.Int)
               .GetParameters();

            dbHelper.ExecuteProcedure(sp_GetUserById, (reader) =>
            {
                while (reader.Read())
                {
                    user = new User
                    {
                        Id = (int)reader[0],
                        Name = reader[1].ToString(),
                        Email = reader[2].ToString()
                    };
                }
            }, sqlParameters);

            return user;
        }
    }
}
