using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.UI.Helpers
{
    public class HelperMethods
    {
        public static User GetLoggedUser(object sessionUser)
        {
            var user = new User();
            
            if (sessionUser != null)
            {
                user = (User)sessionUser;
            }
            return user;
        }
    }
}