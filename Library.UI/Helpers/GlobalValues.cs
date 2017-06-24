using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.UI.Helpers
{
    public class GlobalValues
    {
        public static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["LibraryDBConnection"].ConnectionString;
            }
        }       
    }
}