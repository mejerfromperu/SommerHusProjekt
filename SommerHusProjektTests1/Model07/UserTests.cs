using Microsoft.VisualStudio.TestTools.UnitTesting;
using SommerHusProjekt.Model07;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SommerHusProjekt.Model07.Tests
{
    [TestClass()]
    public class UserTests
    {
        private User _user;

        [TestInitialize]
        public void BeforeEachTest()
        {
            _user = new User(5, "Chris", "Chris", "25624716", "Chris@gmail.com", "88888888", "ChrisStreet", "3", "3", "Roskilde", 23, false, false);
        }

        [TestMethod()]
        public void UserConstructerTest()
        {
            Assert.AreEqual(false, _user.IsAdmin);
        }
        [TestMethod()]
        public void UserConstructerTest2()
        {
            Assert.AreEqual(false, _user.IsLandlord);
        }

        [TestMethod()]
        public void UserIdTest()
        {

            int expectedUserID = 0;

            Assert.AreEqual(expectedUserID, _user.Id);
            Console.WriteLine(_user.Id);
        }

        [TestMethod()]
        public void UserIdTest2()
        {
            User userWithNegativeId = new User(-332, "Alex", "funny", "99328123", "crissy@gmail.com", "password123456", "street", "2", "1", "Roskilde", 4000, false, false);
            int expectedUserID = 0;

            Assert.AreEqual(expectedUserID, _user.Id);
            Console.WriteLine(_user.Id);
        }


    }
}