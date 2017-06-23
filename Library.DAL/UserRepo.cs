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

        public UserRepo(string connectionString)
        {
            dbHelper = new ADOHelper(connectionString);            
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Register(User entity)
        {
            var authers = new List<Auther>();
            var sqlParameters = new SqlParametersHelper()
                .AddParameter("@userName", entity.Name, SqlDbType.NVarChar)
                .AddParameter("@email", entity.Email, SqlDbType.NVarChar)
                .GetParameters();

            var result = dbHelper.ExecuteProcedure(sp_RegisterUser, sqlParameters);

            return result;
        }
    }
}
