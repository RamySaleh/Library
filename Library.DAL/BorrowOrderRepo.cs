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
        private const string sp_GetBorrowOrdersByBookId = "GetBorrowOrdersByBookId";
        private const string sp_BorrowBook = "BorrowBook";        

        public BorrowOrderRepo(string connectionString)
        {
            dbHelper = new ADOHelper(connectionString);
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
                        
                    });
                }
            }, sqlParameters);

            return borrowOrder;
        }
    }
}
