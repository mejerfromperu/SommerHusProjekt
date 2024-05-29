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

      

         
    }
}