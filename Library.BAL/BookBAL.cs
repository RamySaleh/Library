using Library.DAL;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BAL
{
    public class BookBAL
    {
        string connectionString;
        public BookBAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public List<Book> GetAllBooks()
        {
            return new BookRepo(connectionString).GetAllBook();
        }

        public bool BorrowBook(int bookId, int userId)
        {
            return new BorrowOrderRepo(connectionString).BorrowBook(bookId, userId);
        }

        public bool ReturnBook(int bookId, int userId)
        {
            return new BorrowOrderRepo(connectionString).ReturnBook(bookId, userId);
        }

        public List<BorrowOrder> GetBookHistory(int bookId)
        {
            return new BorrowOrderRepo(connectionString).GetBorrowOrdersByBookId(bookId);
        }
    }
}
