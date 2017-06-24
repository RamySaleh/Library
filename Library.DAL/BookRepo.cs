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
        private IAutherRepo autherRepo;
        private const string sp_GetAllBooks = "GetAllBooks";
        private const string sp_GetBookById = "GetBookById";

        public BookRepo(string connectionString)
        {
            dbHelper = new ADOHelper(connectionString);
            autherRepo = new AutherRepo(connectionString);
        }

        public List<Book> GetAllBook(int bookFilter, int userId)
        {
            var books = new List<Book>();
            var sqlParameters = new SqlParametersHelper()
            .AddParameter("@bookFilter", bookFilter, SqlDbType.Int)
            .AddParameter("@userId", userId, SqlDbType.Int)
            .GetParameters();

            dbHelper.ExecuteProcedure(sp_GetAllBooks, (reader) =>
            {
                while (reader.Read())
                {
                    books.Add(new Book
                    {
                        Id = (int)reader[0],
                        Name = reader[1].ToString(),
                        IsAvailable = bool.Parse(reader[2].ToString()),
                        Authers = autherRepo.GetAuthersByBookId((int)reader[0]),
                        CurrentReaderId = reader[3] != DBNull.Value ? (int)reader[3] : -1
                    });
                }
            }, sqlParameters);

            return books;
        }

        public Book GetBookById(int bookId)
        {
            var book = (Book)null;
            var sqlParameters = new SqlParametersHelper()
             .AddParameter("@bookId", bookId, SqlDbType.Int)
             .GetParameters();

            dbHelper.ExecuteProcedure(sp_GetBookById, (reader) =>
            {
                while (reader.Read())
                {
                    book = new Book
                    {
                        Id = (int)reader[0],
                        Name = reader[1].ToString(),
                        IsAvailable = bool.Parse(reader[2].ToString()),
                        Authers = autherRepo.GetAuthersByBookId((int)reader[0])
                    };
                }
            }, sqlParameters);

            return book;
        }
    }
}
