using Microsoft.VisualStudio.TestTools.UnitTesting;
using SommerHusProjekt.Model07;
using SommerHusProjekt.Repository07;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SommerHusProjekt.Repository07.Tests
{
    [TestClass()]
    public class UserRepositoryTests
    {

        private IUserRepository _userRepository;

        [TestInitialize]
        public void Init(IUserRepository repo)
        {
            _userRepository = repo;

        }

        [TestMethod()]
        public void AddTest()
        {

            int numberOfUserBefore = _userRepository.GetAll().Count;
            User newuser = new User("alex", "alex", "88625364", "Alex@gmail.com", "44988232", "streetname", "2", 4000, false, false);

            _userRepository.Add(newuser);

            Assert.AreEqual(numberOfUserBefore + 1, _userRepository.GetAll().Count);
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