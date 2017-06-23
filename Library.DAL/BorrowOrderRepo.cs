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
    public class BorrowOrderRepo : IBorrowOrderRepo
    {
        private ADOHelper dbHelper;
        private IBookRepo bookRepo;
        private IUserRepo userRepo;

        private const string sp_GetBorrowOrdersByBookId = "GetBorrowOrdersByBookId";
        private const string sp_BorrowBook = "BorrowBook";
        private const string sp_ReturnBook = "ReturnBook";

        public BorrowOrderRepo(string connectionString)
        {
            dbHelper = new ADOHelper(connectionString);
            bookRepo = new BookRepo(connectionString);
            userRepo = new UserRepo(connectionString);
        }

        public bool BorrowBook(int bookId, int userId)
        {
            var sqlParameters = new SqlParametersHelper()
               .AddParameter("@bookId", bookId, SqlDbType.Int)
               .AddParameter("@userId", userId, SqlDbType.Int)
               .GetParameters();

            var result = dbHelper.ExecuteProcedure(sp_BorrowBook, sqlParameters);

            return result;
        }

        public bool ReturnBook(int bookId, int userId)
        {
            var sqlParameters = new SqlParametersHelper()
              .AddParameter("@bookId", bookId, SqlDbType.Int)
              .AddParameter("@userId", userId, SqlDbType.Int)
              .GetParameters();

            var result = dbHelper.ExecuteProcedure(sp_ReturnBook, sqlParameters);

            return result;
        }

        public List<BorrowOrder> GetBorrowOrdersByBookId(int bookId)
        {
            var borrowOrder = new List<BorrowOrder>();
            var sqlParameters = new SqlParametersHelper()
                .AddParameter("@bookId", bookId, SqlDbType.Int)
                .GetParameters();

            dbHelper.ExecuteProcedure(sp_GetBorrowOrdersByBookId, (reader) =>
            {
                while (reader.Read())
                {
                    borrowOrder.Add(new BorrowOrder
                    {
                        Reader = userRepo.GetUserById((int)reader[0]),
                        Book = bookRepo.GetBookById(bookId),
                        ActionTime = (DateTime)reader[2],
                        ActionType = reader[3].ToString()
                    });
                }
            }, sqlParameters);

            return borrowOrder;
        }       
    }
}
