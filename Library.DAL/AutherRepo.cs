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
    public class AutherRepo : IAutherRepo
    {
        private ADOHelper dbHelper;
        private const string sp_GetAuthersByBookId = "GetAuthersByBookId";

        public AutherRepo(string connectionString)
        {
            dbHelper = new ADOHelper(connectionString);
        }       

        public List<Auther> GetAuthersByBookId(int bookId)
        {
            var authers = new List<Auther>();
            var sqlParameters = new SqlParametersHelper()
                .AddParameter("@bookId", bookId, SqlDbType.Int)
                .GetParameters();

            dbHelper.ExecuteProcedure(sp_GetAuthersByBookId, (reader) =>
            {
                while (reader.Read())
                {
                    authers.Add(new Auther {
                        Id = (int)reader[0],
                        Name = reader[1].ToString()
                    });
                }
            }, sqlParameters);

            return authers;
        }
    }
}
