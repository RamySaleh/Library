using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BAL
{
    public interface IBookBAL
    {
        List<Book> GetAllBooks(int bookFilter, int userId);

        bool BorrowBook(int bookId, int userId);

        bool ReturnBook(int bookId, int userId);

        List<BorrowOrder> GetBookHistory(int bookId);
    }
}
