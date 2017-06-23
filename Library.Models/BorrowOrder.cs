using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BorrowOrder
    {
        public Book Book { get; set; }

        public User Reader { get; set; }

        public DateTime ActionTime { get; set; }

        public string ActionType { get; set; }
    }
}
