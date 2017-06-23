using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.UI.Models
{
    public class BorrowOrderModel
    {
        public string BookTitle { get; set; }
        public string ReaderName { get; set; }
        public string ActionType { get; set; }
        public string ActionTime { get; set; }
    }
}