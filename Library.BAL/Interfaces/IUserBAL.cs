using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BAL
{
    public interface IUserBAL
    {
        bool RegisterUser(User user);

        User Login(User user);
    }
}
