using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Auther
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Book> WrittenBooks { get; set; }
    }
}
