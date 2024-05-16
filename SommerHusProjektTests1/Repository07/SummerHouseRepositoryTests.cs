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
    public class SummerHouseRepositoryTests
    {
        private ISummerHouseRepository _summerepo;

        [TestInitialize]
        public void Init()
        {
            _summerepo = new SummerHouseRepository();

        }


        [TestMethod()]
        public void AddTest()
        {
            // Arrange
            int numberOfHousesBefore = _summerepo.GetAll().Count;
            SummerHouse newHouse = new SummerHouse
            {
                StreetName = "Test Street",
                HouseNumber = "123",
                Floor = "1",
                PostalCode = 2630,
                Description = "Test Description",
                Price = 100,
                Picture = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTZTWiRCFFbHYyzqGAiHju2zzi_0jn7bGcdNfSairRqoQ&s",
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now.AddDays(7),
                AmountSleepingSpace = 5
            };

            // Act
            SummerHouse addedHouse = _summerepo.Add(newHouse);

            // Assert
            Assert.IsNotNull(addedHouse);
            Assert.AreEqual(numberOfHousesBefore + 1, _summerepo.GetAll().Count);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Arrange
            int numberOfHousesBefore = _summerepo.GetAll().Count;
            SummerHouse goingToBeDeleted = _summerepo.GetAll().Last();
            // Act
            SummerHouse deletedHouse = _summerepo.Delete(goingToBeDeleted.Id); 

            // Assert

            Assert.AreEqual(numberOfHousesBefore - 1, _summerepo.GetAll().Count);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            // Act
            List<SummerHouse> summerHouses = _summerepo.GetAll();

            // Assert
            Assert.IsNotNull(summerHouses);
            Assert.IsTrue(summerHouses.Count > 0);
        }



        [TestMethod()]
        public void GetByIdTest()
        {
            // Act
            SummerHouse house = _summerepo.GetById(1); 

            // Assert
            Assert.IsNotNull(house);
            Assert.AreEqual(1, house.Id); 
        }

        [TestMethod()]
        public void UpdateTest()
        {
            // Arrange
            int houseIdToUpdate = 2; // Replace 1 with the ID of the house to update
            SummerHouse updatedHouseData = new SummerHouse
            {
                StreetName = "Updated Street",
                HouseNumber = "456",
                Floor = "2",
                PostalCode = 2630,
                Description = "Updated Description",
                Price = 150,
                Picture = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTZTWiRCFFbHYyzqGAiHju2zzi_0jn7bGcdNfSairRqoQ&s",
                DateFrom = DateTime.Now.AddDays(1),
                DateTo = DateTime.Now.AddDays(14),
                AmountSleepingSpace = 8
            };

            // Act
            SummerHouse updatedHouse = _summerepo.Update(houseIdToUpdate, updatedHouseData);

            // Assert
            Assert.AreEqual(updatedHouseData.StreetName, updatedHouse.StreetName);
            Assert.AreEqual(updatedHouseData.HouseNumber, updatedHouse.HouseNumber);
            Assert.AreEqual(updatedHouseData.Floor, updatedHouse.Floor);
            Assert.AreEqual(updatedHouseData.PostalCode, updatedHouse.PostalCode);
            Assert.AreEqual(updatedHouseData.Description, updatedHouse.Description);
            Assert.AreEqual(updatedHouseData.Price, updatedHouse.Price);
            Assert.AreEqual(updatedHouseData.Picture, updatedHouse.Picture);
            //Assert.AreEqual(updatedHouseData.DateFrom, updatedHouse.DateFrom);
            //Assert.AreEqual(updatedHouseData.DateTo, updatedHouse.DateTo);
            Assert.AreEqual(updatedHouseData.AmountSleepingSpace, updatedHouse.AmountSleepingSpace);
        }

        //[TestMethod()]
        //public void SearchTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void SortIdTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void SortStreetNameTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void SortPostalCodeTest()
        //{
        //    Assert.Fail();
        //}
    }
}