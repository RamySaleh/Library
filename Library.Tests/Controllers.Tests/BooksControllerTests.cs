using Library.BAL;
using Library.Models;
using Library.UI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Library.Tests.Controllers.Tests
{
    [TestClass]
    public class BooksControllerTests
    {
        static List<Book> fakeBooksList;
        static BookBAL bookBalMoqObj;
        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            fakeBooksList = FillFakeBooksList();

            var bookBALMoq = new Mock<BookBAL>();
            bookBALMoq.Setup(x => x.GetAllBooks(1,1)).Returns(() => fakeBooksList);
            bookBalMoqObj = bookBALMoq.Object;
        }

        private static List<Book> FillFakeBooksList()
        {
            var booksList = new List<Book>();
            booksList.Add(new Book
            {
                Id= 99991,
                Name= "test_book1",
                CurrentReaderId = -1,
                IsAvailable = true
            });

            booksList.Add(new Book
            {
                Id = 99992,
                Name = "test_book2",
                CurrentReaderId = -1,
                IsAvailable = true
            });

            booksList.Add(new Book
            {
                Id = 99993,
                Name = "test_book3",
                CurrentReaderId = 2,
                IsAvailable = false
            });

            booksList.Add(new Book
            {
                Id = 99994,
                Name = "test_book4",
                CurrentReaderId = 2,
                IsAvailable = false
            });

            booksList.Add(new Book
            {
                Id = 99995,
                Name = "test_book5",
                CurrentReaderId = -1,
                IsAvailable = true
            });

            return booksList;
        }

        [TestMethod]
        public void IndexAction_ReturnsCorrectView()
        {
            var bookController = new BooksController(bookBalMoqObj);

            var result = bookController.Index(null) as ViewResult;

            Assert.IsTrue(result != null && result.ViewName == "Index");
        }
    }
}
