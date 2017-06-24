using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.UI.Models
{
    public class BookModel
    {
        public int BookId { get; set; }

        public string BookName { get; set; }

        public string Authers { get; set; }

        public string IsAvailable { get; set; }

        public bool TakenByCurrentUser { get; set; }
    }
}