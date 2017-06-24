using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.DAL;
using System.Configuration;
using System.Linq;
using System.IO;
using Library.Tests.Helpers;

namespace Library.Tests
{
    [TestClass]
    public class BookRepoTests
    {
        static string connectionString;
        static ADOHelper adoHelper;

        public BookRepoTests()
        {
            connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnection"].ToString();
            adoHelper = new ADOHelper(connectionString);
        }

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            string script = File.ReadAllText(@"..\..\Scripts\RemoveBooks.sql");
            adoHelper.ExecuteScript(script);

            script = File.ReadAllText(@"..\..\Scripts\InsertBooks.sql");
            adoHelper.ExecuteScript(script);
        }

        [TestMethod]
        public void GetBookById_ReturnsCorrectBook()
        {
            var bookRepo = new BookRepo(connectionString);

            var book = bookRepo.GetBookById(99991);

            Assert.IsTrue(book != null && book.Id == 99991);
        }

        [TestMethod]
        public void GetBookById_NotExistBook()
        {
            var bookRepo = new BookRepo(connectionString);

            var book = bookRepo.GetBookById(999919);

            Assert.IsNull(book);
        }

        [TestMethod]
        public void GetAllBooks_AllBooksFilter_ReturnsData()
        {
            var bookRepo = new BookRepo(connectionString);

            var books = bookRepo.GetAllBook(1, 0).Where(b => b.Name.StartsWith("test_"));

            Assert.IsTrue(books != null && books.Count() == 5);
        }

        [TestMethod]
        public void GetAllBooks_AvailableBooks_ReturnsData()
        {
            var bookRepo = new BookRepo(connectionString);

            var books = bookRepo.GetAllBook(2, 0).Where(b => b.Name.StartsWith("test_"));

            Assert.IsTrue(books != null && books.Count() == 3);
        }

        [TestMethod]
        public void GetAllBooks_TakenByUser_ReturnsData()
        {
            var bookRepo = new BookRepo(connectionString);

            var books = bookRepo.GetAllBook(3, 2).Where(b => b.Name.StartsWith("test_"));

            Assert.IsTrue(books != null && books.Count() == 2);
        }

        [TestMethod]
        public void GetAllBooks_WrongUser_DoesNotReturnsData()
        {
            var bookRepo = new BookRepo(connectionString);

            var books = bookRepo.GetAllBook(3, 4);

            Assert.IsTrue(books != null && books.Count() == 0);
        }

        [TestMethod]
        public void GetAllBooks_NotExistFilter_DoesNotReturnsData()
        {
            var bookRepo = new BookRepo(connectionString);

            var books = bookRepo.GetAllBook(5, 0);

            Assert.IsTrue(books != null && books.Count() == 0);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            string script = File.ReadAllText(@"..\..\Scripts\RemoveBooks.sql");

            adoHelper.ExecuteScript(script);
        }
    }

}
