using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.DAL;
using System.Configuration;
using System.Linq;

namespace Library.Tests
{
    [TestClass]
    public class BookRepoTests
    {
        [TestMethod]
        public void GetAllBooksReturnsData()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnection"].ToString();
            var bookRepo = new BookRepo(connectionString);

            var books = bookRepo.GetAllBook();

            Assert.IsTrue(books != null && books.Count() > 0);
        }
    }
}
