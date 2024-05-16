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
            
            int numberOfBookingsBefore = _bookingrepo.GetAll().Count;
            Booking newBooking = new Booking
            {
                UserId = 22, 
                SummerHouseId = 1, 
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7)
            };

            // Act
            Booking addedBooking = _bookingrepo.Add(newBooking);

            // Assert
            Assert.IsNotNull(addedBooking);
            Assert.AreEqual(numberOfBookingsBefore + 1, _bookingrepo.GetAll().Count);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Arrange
            int numberOfBookingsBefore = _bookingrepo.GetAll().Count;
            Booking newbooking = _bookingrepo.GetAll().Last() ; 
            int bookingIdToDelete = newbooking.Id;
            // Act
            Booking deletedBooking = _bookingrepo.Delete(bookingIdToDelete);

            // Assert

            Assert.AreEqual(numberOfBookingsBefore - 1, _bookingrepo.GetAll().Count);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            // Act
            List<Booking> bookings = _bookingrepo.GetAll();

            // Assert
            Assert.IsNotNull(bookings);
            Assert.IsTrue(bookings.Count > 0);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            // Arrange
            int bookingIdToRetrieve = 1; 

            // Act
            Booking booking = _bookingrepo.GetById(bookingIdToRetrieve);

            // Assert
            Assert.IsNotNull(booking);
            Assert.AreEqual(bookingIdToRetrieve, booking.Id);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            // Arrange
            int bookingIdToUpdate = 48; 
            Booking updatedBookingData = new Booking
            {
                UserId = 46, 
                SummerHouseId = 1, 
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(8)
            };

            // Act
            Booking updatedBooking = _bookingrepo.Update(bookingIdToUpdate, updatedBookingData);

            // Assert
            Assert.AreEqual(updatedBookingData.StartDate, updatedBooking.StartDate);
            Assert.AreEqual(updatedBookingData.EndDate, updatedBooking.EndDate);
        }

        [TestMethod()]
        public void GetBookingByUserIdTest()
        {
            // Arrange
            int userId = 46; 

            // Act
            List<Booking> bookings = _bookingrepo.GetBookingByUserId(userId);

            // Assert
            Assert.IsNotNull(bookings);
            Assert.IsTrue(bookings.Count > 0);
        }
    }
}