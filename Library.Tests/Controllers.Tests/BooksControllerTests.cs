﻿using Library.BAL;
using Library.Models;
using Library.UI.Controllers;
using Library.UI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Library.Tests.Controllers.Tests
{
    [TestClass]
    public class BooksControllerTests
    {
        static List<Book> fakeBooksList;        
        static User fakeUser;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            FillFakeObjects();
        }

        private static void FillFakeObjects()
        {

            var auther1 = new Auther
            {
                Id = 1,
                Name = "test_auther1"
            };

            var auther2 = new Auther
            {
                Id = 2,
                Name = "test_auther2"
            };

            var auther3 = new Auther
            {
                Id = 3,
                Name = "test_auther3"
            };

            fakeBooksList = new List<Book>();
            fakeBooksList.Add(new Book
            {
                Id = 99991,
                Name = "test_book1",
                CurrentReaderId = -1,
                IsAvailable = true,
                Authers = new List<Auther> { auther1 }
            });            

            fakeBooksList.Add(new Book
            {
                Id = 99993,
                Name = "test_book3",
                CurrentReaderId = 2,
                IsAvailable = false,
                Authers = new List<Auther> { auther3 }
            });

            fakeBooksList.Add(new Book
            {
                Id = 99992,
                Name = "test_book2",
                CurrentReaderId = -1,
                IsAvailable = true,
                Authers = new List<Auther> { auther2 }
            });

            fakeBooksList.Add(new Book
            {
                Id = 99995,
                Name = "test_book5",
                CurrentReaderId = -1,
                IsAvailable = true,
                Authers = new List<Auther> { auther2, auther3 }
            });

            fakeBooksList.Add(new Book
            {
                Id = 99994,
                Name = "test_book4",
                CurrentReaderId = 2,
                IsAvailable = false,
                Authers = new List<Auther> { auther1, auther2 }
            });           

            fakeUser = new User
            {
                Id = 2,
                Name = "test_user",
                Email = "test@email.com",
            };
        }

        private static BooksController CreateControllerWithFakeUser(IBookBAL bookBAL)
        {
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["User"]).Returns(fakeUser);

            var bookController = new BooksController(bookBAL);
            bookController.ControllerContext = controllerContext.Object;
            return bookController;
        }

        [TestMethod]
        public void IndexAction_NotLoggedIn_RedirectToLogin()
        {
            // Arrange
            var bookBALMoq = new Mock<IBookBAL>();
            bookBALMoq.Setup(x => x.GetAllBooks(1, fakeUser.Id)).Returns(() => fakeBooksList);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["User"]).Returns(null);

            var bookController = new BooksController(bookBALMoq.Object);
            bookController.ControllerContext = controllerContext.Object;

            // Act
            var result = bookController.Index(null) as RedirectToRouteResult;
            
            // Assert
            Assert.IsTrue(result != null && result.RouteValues["action"].ToString() == "Login");
        }

        [TestMethod]
        public void IndexAction_LoggedIn_ReturnsView()
        {
            // Arrange
            var bookBALMoq = new Mock<IBookBAL>();
            bookBALMoq.Setup(x => x.GetAllBooks(1, fakeUser.Id)).Returns(() => fakeBooksList);

            BooksController bookController = CreateControllerWithFakeUser(bookBALMoq.Object);

            // Act
            var result = bookController.Index(null) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }       

        [TestMethod]
        public void IndexAction_ReturnAllBook()
        {
            // Arrange
            var bookBALMoq = new Mock<IBookBAL>();
            bookBALMoq.Setup(x => x.GetAllBooks(1, fakeUser.Id)).Returns(() => fakeBooksList);

            BooksController bookController = CreateControllerWithFakeUser(bookBALMoq.Object);

            // Act
            var result = bookController.Index(null) as ViewResult;
            var booksModels = result.Model as List<BookModel>;

            // Assert
            Assert.IsTrue(booksModels != null && booksModels.Count == 5);
        }

        [TestMethod]
        public void IndexAction_ReturnAvailableBooks()
        {
            // Arrange
            var bookBALMoq = new Mock<IBookBAL>();
            bookBALMoq.Setup(x => x.GetAllBooks(2, fakeUser.Id)).Returns(() => fakeBooksList.Where(b => b.IsAvailable).ToList());

            BooksController bookController = CreateControllerWithFakeUser(bookBALMoq.Object);

            // Act
            var result = bookController.Index(null, false, 2) as ViewResult;
            var booksModels = result.Model as List<BookModel>;

            // Assert
            Assert.IsTrue(booksModels != null && booksModels.Count == 3);
        }

        [TestMethod]
        public void IndexAction_ReturnBooksTakenByUser()
        {
            // Arrange
            var bookBALMoq = new Mock<IBookBAL>();
            bookBALMoq.Setup(x => x.GetAllBooks(3, fakeUser.Id)).Returns(() => fakeBooksList.Where(b => b.CurrentReaderId == fakeUser.Id).ToList());

            BooksController bookController = CreateControllerWithFakeUser(bookBALMoq.Object);

            // Act
            var result = bookController.Index(null, false, 3) as ViewResult;
            var booksModels = result.Model as List<BookModel>;

            // Assert
            Assert.IsTrue(booksModels != null && 
                booksModels.Count == 2 && 
                booksModels.All(b => b.TakenByCurrentUser));
        }

        [TestMethod]
        public void IndexAction_ReturnAllBook_DefaultSort()
        {
            // Arrange
            var bookBALMoq = new Mock<IBookBAL>();
            bookBALMoq.Setup(x => x.GetAllBooks(1, fakeUser.Id)).Returns(() => fakeBooksList);

            BooksController bookController = CreateControllerWithFakeUser(bookBALMoq.Object);

            // Act
            var result = bookController.Index(null) as ViewResult;
            var booksModels = result.Model as List<BookModel>;

            // Assert
            Assert.IsTrue(booksModels != null && 
                booksModels.SequenceEqual(booksModels.OrderBy(b => b.BookName)));
        }

        [TestMethod]
        public void SortBooks_BookTitle_Asc()
        {
            // Arrange
            BooksController bookController = CreateControllerWithFakeUser(null);
            var booksModels = bookController.MapBooksToViewModels(fakeBooksList, fakeUser);

            // Act            
            var sortedModels = bookController.SortBooks(booksModels, BooksController.SortByBookTitle, true);

            // Assert
            Assert.IsTrue(sortedModels != null &&
                sortedModels.SequenceEqual(booksModels.OrderBy(b => b.BookName)));
        }

        [TestMethod]
        public void SortBooks_BookTitle_Desc()
        {
            // Arrange
            BooksController bookController = CreateControllerWithFakeUser(null);
            var booksModels = bookController.MapBooksToViewModels(fakeBooksList, fakeUser);

            // Act - to sort desc i have to sort with the same column twice
            var sortedModels = bookController.SortBooks(booksModels, BooksController.SortByBookTitle, true);
            sortedModels = bookController.SortBooks(booksModels, BooksController.SortByBookTitle, true);

            // Assert
            Assert.IsTrue(sortedModels != null &&
                sortedModels.SequenceEqual(booksModels.OrderByDescending(b => b.BookName)));
        }

        [TestMethod]
        public void SortBooks_Auther_Asc()
        {
            // Arrange
            BooksController bookController = CreateControllerWithFakeUser(null);
            var booksModels = bookController.MapBooksToViewModels(fakeBooksList, fakeUser);

            // Act
            var sortedModels = bookController.SortBooks(booksModels, BooksController.SortByAuther, true);

            // Assert
            Assert.IsTrue(sortedModels != null &&
                sortedModels.SequenceEqual(booksModels.OrderBy(b => b.Authers)));
        }

        [TestMethod]
        public void SortBooks_Auther_Desc()
        {
            // Arrange
            BooksController bookController = CreateControllerWithFakeUser(null);
            var booksModels = bookController.MapBooksToViewModels(fakeBooksList, fakeUser);

            // Act - to sort desc i have to sort with the same column twice
            var sortedModels = bookController.SortBooks(booksModels, BooksController.SortByAuther, true);
            sortedModels = bookController.SortBooks(booksModels, BooksController.SortByAuther, true);

            // Assert
            Assert.IsTrue(sortedModels != null &&
                sortedModels.SequenceEqual(booksModels.OrderByDescending(b => b.Authers)));
        }
    }
}