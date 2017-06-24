using Library.UI.Attributes;
using Library.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.UI.Controllers
{
    public class HomeController : Controller
    {
        [AuthenticationFilter]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            Session["User"] = null;

            return RedirectToAction("Login", "User");
        }

        public ActionResult Error()
        {           
            return View("Error");
        }
    }
}