using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class User
    {
        public string id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public IEnumerable<BorrowOrder> BorrowedBooks { get; set; }
    }
}
