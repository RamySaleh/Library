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
        public ActionResult Index()
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
                return RedirectToAction("Index", "Books");
            }

            return RedirectToAction("Index");
        }
    }
}