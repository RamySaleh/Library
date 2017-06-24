using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BAL
{
    public interface IBookService
    {
        List<Book> GetAllBooks(int bookFilter, int userId);

        List<Book> GetAllBooksPaged(int bookFilter, int userId, int pageSize, int Page);

        bool BorrowBook(int bookId, int userId);

        bool ReturnBook(int bookId, int userId);

        List<BorrowOrder> GetBookHistory(int bookId);
    }
}
