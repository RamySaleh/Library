﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Book
    {
        public string id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Auther> Authers { get; set; }

        public IEnumerable<BorrowOrder> BorrowOrders { get; set; }
    }
}