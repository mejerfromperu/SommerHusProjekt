using Microsoft.VisualStudio.TestTools.UnitTesting;
using SommerHusProjekt.Model07;
using SommerHusProjekt.Repository07;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
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
            // ARange
            int numberOfUserBefore = _userRepository.GetAll().Count;
            User newUser = new User("alex", "alex", "88625364", "Alex-UnitTest@gmail.com", "44988232", "streetname", "2", 4000, false, false);


            // Act
            _userRepository.Add(newUser); // tilføjer bruger til database


            // Assert 
            // Der er blevet tilføjet en user så derfor skulle start antallet + 1 gerne været = med antallet are user i databasen
            Assert.AreEqual(numberOfUserBefore + 1, _userRepository.GetAll().Count);
        }

        public void AddTestWithId()
        {
            // ARange
            int numberOfUserBefore = _userRepository.GetAll().Count;
            User newUser = new User("alex", "alex", "88625364", "Alex-UnitdedeTest@gmail.com", "44988232", "streetname", "2", 4000, false, false);


            // Act
            _userRepository.Add(newUser); // tilføjer bruger til database


            // Assert 
            // Der er blevet tilføjet en user så derfor skulle start antallet + 1 gerne været = med antallet are user i databasen
            Assert.AreEqual(numberOfUserBefore + 1, _userRepository.GetAll().Count);
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void AddUser_DuplicateEmailTest()
        {
            // Arange
            User newuser = new User("alex", "alex", "88625364", "UNIT-TEST@gmail.com", "password123", "streetname", "2", "1", 4000, false, false);


            // Act
            _userRepository.Add(newuser); // Første skulle gerne tilføjes i database 
            _userRepository.Add(newuser); // Nummer 2 skal fejle begrund af duplicate email i database.


            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void AddUser_DuplicateEmailTest2()
        {
            // Arange
            User newuser = new User("alex", "alex", "88625364", "UNIT-TEST3@gmail.com", "password123", "streetname", "2", "1", 4000, false, false);

            User newuser2 = new User("John", "John", "231423532", "ShouldBeAdded@gmail.com", "password", "streetname", "2", "1", 4000, false, false);


            // Act
            _userRepository.Add(newuser); // Første skulle gerne tilføjes i database 
            _userRepository.Add(newuser2); // Newuser skal gerne addeds 
            _userRepository.Add(newuser); // Nummer 2 skal fejle begrund af duplicate email i database.


            //Assert
            Assert.Fail(); // meningen er at vi stadig skal smide en exception efter de 2 første objecter af typen user bliver tilføjet til repo.
        }


        [TestMethod()]
        public void DeleteTest()
        {
            int idToDelete = _userRepository.GetAll().Last().Id;
            int numberOfUserBefore = _userRepository.GetAll().Count;

            _userRepository.Delete(idToDelete);


            Assert.AreEqual(numberOfUserBefore -1, _userRepository.GetAll().Count);
        }

        [TestMethod()]
        public void GetAllTest()
        {

            int countAll = _userRepository.GetAll().Count;

            Assert.AreEqual(countAll, _userRepository.GetAll().Count);
        }

        [TestMethod()]
        public void GetSomethingTest()
        {
            User newuser = _userRepository.GetById(21);
            int id = newuser.Id;

            Assert.AreEqual(id, 21);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            User newuser = _userRepository.GetById(21);
            int id = newuser.Id;

            Assert.AreEqual(id, 21);
        }


    }
}