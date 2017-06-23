using Library.BAL;
using Library.Models;
using Library.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.UI.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            var userBAL = new UserBAL(GlobalValues.ConnectionString);

            var result = userBAL.RegisterUser(user);

            if (result)
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var userBAL = new UserBAL(GlobalValues.ConnectionString);

            var loggedInUser = userBAL.Login(user);

            if (loggedInUser != null)
            {
                Session["User"] = loggedInUser;
                return RedirectToAction("Index", "Books");
            }

            return View();
        }
    }
}