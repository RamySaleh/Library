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
        // GET: Books
        public ActionResult Index()
        {
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

            return View(booksModels);
        }

        private string ConcatAuthersNames(List<Auther> authers)
        {
            var authersConcatenated = string.Join(" ,", authers.Select(auther => auther.Name));
                       
            return authersConcatenated;
        }
    }
}