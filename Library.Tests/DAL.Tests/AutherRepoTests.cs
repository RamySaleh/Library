using Library.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.DAL.Tests
{
    [TestClass]
    public class AutherRepoTests
    {
        [TestMethod]
        public void GetAuthersByBookIdReturnsData()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["LibraryDBConnection"].ToString();
            var autherRepo = new AutherRepo(connectionString);

            var authers = autherRepo.GetAuthersByBookId(2);

            Assert.IsTrue(authers != null && authers.Count() > 0);
        }
    }
}
