using Library.UI.Helpers;
using Library.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;
using System.Text;
using Library.BAL;
using Library.DependencyInjection;
using Library.Infrastructure.ExceptionHandling;
using Library.UI.Attributes;

namespace Library.UI.Controllers
{
    [AuthenticationFilter]
    public class BooksController : Controller
    {
        public const string SortByBookTitle = "Book Title";
        public const string SortByAuther = "Auther";
        public const int PageSize = 5;

        static string orderedBy;
        static bool orderAscending;
        IBookService bookBAL;

        public BooksController()
        {
            bookBAL = new BookService(GlobalValues.ConnectionString);
        }

        public BooksController(IBookService bookBAL)
        {
            this.bookBAL = bookBAL;
        }

        #region Actions

        public ActionResult Index(string orderBy, bool sort = true, int bookFilter = 1, bool? next = null)
        {
            try
            {
                var currentUser = (User)Session["User"];

                SetBookFilterInViewbag(bookFilter);

                int page = 1;
                if (next.HasValue)
                {
                    page = next.Value ? IncrementPage() : DecrementPage();
                }
                
                Session["Page"] = page;
                ViewBag.page = page;
                
                var books = bookBAL.GetAllBooksPaged(bookFilter, currentUser.Id, PageSize, page);               

                var booksModels = MapBooksToViewModels(books, currentUser);

                // Appy default order for the first time
                if (string.IsNullOrWhiteSpace(orderBy))
                {
                    orderBy = SortByBookTitle;
                    orderedBy = null;
                    orderAscending = true;
                }

                booksModels = SortBooks(booksModels, orderBy, sort);

                return View(booksModels);
            }
            catch (Exception ex)
            {
                IocContainer.Resolve<IExceptionHandler>().HandleException(ex);
                throw;
            }           
        }

        public ActionResult BorrowBook(int bookId)
        {
            try
            {
                var currentUser = (User)Session["User"];
                var result = bookBAL.BorrowBook(bookId, currentUser.Id);

                if (result)
                {
                    return RedirectToAction("Index");
                }

                return null;
            }
            catch (Exception ex)
            {
                IocContainer.Resolve<IExceptionHandler>().HandleException(ex);
                throw;
            }
        }

        public ActionResult ReturnBook(int bookId)
        {
            try
            {
                var currentUser = (User)Session["User"];
                var result = bookBAL.ReturnBook(bookId, currentUser.Id);

                if (result)
                {
                    return RedirectToAction("Index");
                }

                return null;
            }
            catch (Exception ex)
            {
                IocContainer.Resolve<IExceptionHandler>().HandleException(ex);
                throw;
            }
        }

        public ActionResult History(int bookId)
        {
            try
            {
                var borrowOrders = bookBAL.GetBookHistory(bookId);

                var borrowOrdersModels = new List<BorrowOrderModel>();

                foreach (var borrowOrder in borrowOrders)
                {
                    borrowOrdersModels.Add(new BorrowOrderModel
                    {
                        BookTitle = borrowOrder.Book.Name,
                        ReaderName = borrowOrder.Reader.Name,
                        ActionType = borrowOrder.ActionType,
                        ActionTime = borrowOrder.ActionTime.ToLocalTime().ToString()
                    });
                }
                return View(borrowOrdersModels);
            }
            catch (Exception ex)
            {
                IocContainer.Resolve<IExceptionHandler>().HandleException(ex);
                throw;
            }
        }

        #endregion

        #region Private methods
        public List<BookModel> MapBooksToViewModels(List<Book> books, User user)
        {
            var booksModels = new List<BookModel>();
            if (books != null)
            {
                foreach (var book in books)
                {
                    booksModels.Add(new BookModel
                    {
                        BookId = book.Id,
                        BookName = book.Name,
                        Authers = ConcatAuthersNames(book.Authers),
                        IsAvailable = book.IsAvailable ? "Yes" : "No",
                        TakenByCurrentUser = book.CurrentReaderId == user.Id
                    });
                }
            }           
            return booksModels;
        }

        public List<BookModel> SortBooks(List<BookModel> booksModels, string orderBy, bool sort)
        {
            if (sort)
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
            }          

            // Set the order in viewbag to be viewed from the view
            ViewBag.OrderBy = orderBy;
            ViewBag.OrderDirection = orderAscending ? "Ascending" : "Descending";

            switch (orderBy)
            {
                case SortByBookTitle:
                    booksModels = orderAscending ?
                        booksModels.OrderBy(b => b.BookName).ToList() :
                        booksModels.OrderByDescending(b => b.BookName).ToList();
                    break;
                case SortByAuther:
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

        private void SetBookFilterInViewbag(int bookFilter)
        {
            ViewBag.bookFilter = bookFilter;
            switch (bookFilter)
            {
                case 1:
                    ViewBag.BookFilterText = "All Books";
                    break;
                case 2:
                    ViewBag.BookFilterText = "Available Books";
                    break;
                case 3:
                    ViewBag.BookFilterText = "My Books";
                    break;
                default:
                    break;
            }
        }

        private int IncrementPage()
        {
            if (Session["Page"] == null)
            {
                return 1;
            }
            var page = (int)Session["Page"];
            page = ++page;

            Session["Page"] = page;
            return page;
        }

        private int DecrementPage()
        {
            if (Session["Page"] == null)
            {
                return 1;
            }
            var page = (int)Session["Page"];
            page = page > 1 ? --page : 1;

            Session["Page"] = page;
            return page;
        }
        #endregion
    }
}