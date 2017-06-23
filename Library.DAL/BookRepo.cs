using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;
using System.Data.SqlClient;
using System.Data;
using Library.DAL.Helpers;

namespace Library.DAL
{
    public class BookRepo : IBookRepo
    {
        private ADOHelper dbHelper;
        private AutherRepo autherRepo;
        private const string sp_GetAllBooks = "GetAllBooks";

        public BookRepo(string connectionString)
        {
            dbHelper = new ADOHelper(connectionString);
            autherRepo = new AutherRepo(connectionString);
        }
        public List<Book> GetAll()
        {
            var books = new List<Book>();
            
            dbHelper.ExecuteProcedure(sp_GetAllBooks, (reader) =>
            {
                while (reader.Read())
                {
                    books.Add(new Book
                    {
                        Id = (int)reader[0],
                        Name = reader[1].ToString(),
                        Authers = autherRepo.GetAuthersByBookId((int)reader[0])
                    });
                }
            });
            
            return books;
        }     
    }
}
