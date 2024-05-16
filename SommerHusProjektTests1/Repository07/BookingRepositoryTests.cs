using Microsoft.VisualStudio.TestTools.UnitTesting;
using SommerHusProjekt.Repository07;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SommerHusProjekt.Repository07.Tests
{

    [TestClass()]
    public class BookingRepositoryTests
    {

        private IBookingRepository _bookingrepo;

        [TestInitialize]
        public void Init()
        {
            _bookingrepo = new BookingRepository();
        }


        [TestMethod()]
        public void AddTest()
        {
            Assert.Fail();
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
        public void GetBookingByUserIdTest()
        {
            Assert.Fail();
        }
    }
}