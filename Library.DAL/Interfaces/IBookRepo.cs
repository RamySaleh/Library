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
        Book GetBookById(int bookId);
    }
}
