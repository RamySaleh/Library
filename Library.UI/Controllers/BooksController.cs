using Library.UI.Helpers;
using Library.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;
using System.Text;

namespace Library.UI.Controllers
{
    public class BooksController : Controller
    {
        static string orderedBy;
        static bool orderAscending;
        // GET: Books
        public ActionResult Index(string orderBy)
        {
            if (Session["User"] == null)
            {
                //return RedirectToAction("Login", "User");
            }

            var books = new BAL.BookBAL(GlobalValues.ConnectionString).GetAllBooks();

            var booksModels = new List<BookModel>();

            foreach (var book in books)
            {
                booksModels.Add(new BookModel
                {
                    BookId = book.Id,
                    BookName = book.Name,
                    Authers = ConcatAuthersNames(book.Authers)
                });
            }

            // Appy default order for the first time
            if (string.IsNullOrWhiteSpace(orderBy))
            {
                orderBy = "Book Title";
                orderedBy = null;
                orderAscending = true;
            }

            booksModels = SortBooks(booksModels, orderBy);

            return View(booksModels);
        }

        private List<BookModel> SortBooks(List<BookModel> booksModels, string orderBy)
        {
            if (orderedBy != orderBy)
            {
                orderAscending = true;
            }
            else
            {
                orderAscending = !orderAscending;
            }

            orderedBy = orderBy;

            // Set the order in viewbag to be viewed from the view
            ViewBag.OrderBy = orderBy;
            ViewBag.OrderDirection = orderAscending ? "Ascending" : "Descending";

            switch (orderBy)
            {
                case "Book Title":
                    booksModels = orderAscending ?
                        booksModels.OrderBy(b => b.BookName).ToList() :
                        booksModels.OrderByDescending(b => b.BookName).ToList();
                    break;
                case "Auther":
                    booksModels = orderAscending ?
                        booksModels.OrderBy(b => b.Authers).ToList() :
                        booksModels.OrderByDescending(b => b.Authers).ToList();
                    break;
            }

            return booksModels;
        }

        private string ConcatAuthersNames(List<Auther> authers)
        {
            var authersConcatenated = string.Join(" , ", authers.Select(auther => auther.Name));
                       
            return authersConcatenated;
        }
    }
}