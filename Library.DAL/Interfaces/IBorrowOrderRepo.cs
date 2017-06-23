using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public interface IBorrowOrderRepo : IRepo<BorrowOrder>
    {
        bool BorrowBook(int bookId, int userId);

        bool ReturnBook(int bookId, int userId);

        List<BorrowOrder> GetBorrowOrdersByBookId(int bookId);
    }
}
