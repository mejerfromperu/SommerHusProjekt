using Microsoft.VisualStudio.TestTools.UnitTesting;
using SommerHusProjekt.Model07;
using SommerHusProjekt.Repository07;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SommerHusProjekt.Repository07.Tests
{
    [TestClass()]
    public class UserRepositoryTests
    {

        private IUserRepository _userRepository;

        [TestInitialize]
        public void Init()
        {
            _userRepository = new UserRepository();
        }

        [TestMethod()]
        public void AddTest()
        {

            int numberOfUserBefore = _userRepository.GetAll().Count;
            User newuser = new User("chris", "mejer", "292929292", "Chris@gmail.com", "99999999", "street", "housenumber", 4000, false, false);

            _userRepository.Add(newuser);


            Assert.AreEqual(_userRepository.GetAll().Count, numberOfUserBefore + 1);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetSomethingTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetByEmailAndPasswordTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SearchTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SortIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SortLastNameTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SortFirstNameTest()
        {
            Assert.Fail();
        }
    }
}