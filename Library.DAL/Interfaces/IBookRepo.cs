using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public interface IBookRepo : IRepo<Book>
    {
        List<Book> GetAllBook(int bookFilter, int userId);
        List<Book> GetAllBookPaged(int bookFilter, int userId, int page, int pageSize);
        Book GetBookById(int bookId);
    }
}
