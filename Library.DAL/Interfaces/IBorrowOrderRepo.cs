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
        bool AddBorrowOrder(Book book, User user);

        List<BorrowOrder> GetBorrowOrdersByBookId(int bookId);
    }
}
