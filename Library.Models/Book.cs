using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsAvailable { get; set; }

        public List<Auther> Authers { get; set; }

        public List<BorrowOrder> BorrowOrders { get; set; }
    }
}
