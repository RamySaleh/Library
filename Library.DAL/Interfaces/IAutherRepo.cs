using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public interface IAutherRepo : IRepo<Auther>
    {
        List<Auther> GetAuthersByBookId(int bookId);
    }
}
