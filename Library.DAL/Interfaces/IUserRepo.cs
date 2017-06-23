using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public interface IUserRepo : IRepo<User>
    {
        bool Register(User entity);
    }
}
