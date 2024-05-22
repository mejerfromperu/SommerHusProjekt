using Microsoft.VisualStudio.TestTools.UnitTesting;
using SommerHusProjekt.Model07;
using SommerHusProjekt.Repository07;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        [TestMethod()]
        public void UpdateTest()
        {
            // Arrange
            int userIdToUpdate = 22; // Change this to the ID of the user you want to update
            User updatedUserData = new User("Alex", "alex", "299299233", "Alex@gmail.com", "29992999", "streetname", "2st", 4000, false, true);
            

            // Act
            User updatedUser = _userRepository.Update(userIdToUpdate, updatedUserData);

            // Assert
            Assert.AreEqual(updatedUserData.FirstName, updatedUser.FirstName);
            Assert.AreEqual(updatedUserData.LastName, updatedUser.LastName);
            Assert.AreEqual(updatedUserData.Phone, updatedUser.Phone);
            Assert.AreEqual(updatedUserData.Email, updatedUser.Email);
            Assert.AreEqual(updatedUserData.Password, updatedUser.Password);
            Assert.AreEqual(updatedUserData.StreetName, updatedUser.StreetName);
            Assert.AreEqual(updatedUserData.HouseNumber, updatedUser.HouseNumber);
            Assert.AreEqual(updatedUserData.Floor, updatedUser.Floor);
            Assert.AreEqual(updatedUserData.PostalCode, updatedUser.PostalCode);
            Assert.AreEqual(updatedUserData.IsLandlord, updatedUser.IsLandlord);
        }
    }
}