﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Booking newBooking43 = _bookingrepo.GetAll().Last();
            Booking newBooking = new Booking
            {
                UserId = newBooking43.UserId, 
                SummerHouseId = newBooking43.SummerHouseId, 
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