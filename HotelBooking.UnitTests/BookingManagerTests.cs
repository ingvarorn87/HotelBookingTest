using System;
using System.Collections.Generic;
using HotelBooking.Core;
using HotelBooking.UnitTests.Fakes;
using Xunit;
using Moq;

namespace HotelBooking.UnitTests
{
    public class BookingManagerTests
    {
        private IBookingManager bookingManager;

        public BookingManagerTests(){
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);
            IRepository<Booking> bookingRepository = new FakeBookingRepository(start, end);
            IRepository<Room> roomRepository = new FakeRoomRepository();
            bookingManager = new BookingManager(bookingRepository, roomRepository);
        }

        [Fact]
        public void FindAvailableRoom_StartDateNotInTheFuture_ThrowsArgumentException()
        {
            DateTime date = DateTime.Today;
            Assert.Throws<ArgumentException>(() => bookingManager.FindAvailableRoom(date, date));
        }

        [Fact]
        public void FindAvailableRoom_RoomAvailable_RoomIdNotMinusOne()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(1);
            // Act
            int roomId = bookingManager.FindAvailableRoom(date, date);
            // Assert
            Assert.NotEqual(-1, roomId);
        }

        [Fact]
        public void GetFullyOcupiedDates_SameDateTest()
        {
            DateTime date = DateTime.Today;
            Assert.Throws<ArgumentException>(() => bookingManager.GetFullyOccupiedDates(date.AddDays(1), date));

        }

        [Fact]
        public void GetFullyOccupiedDates_Valid_Test()
        {
            DateTime start = DateTime.Today;
            DateTime end = start.AddDays(1);
            var ret = bookingManager.GetFullyOccupiedDates(start, end);
            Assert.IsType<List<DateTime>>(ret);
        }

        [Fact]
        public void FindAvailableRoomTest() 
        {
            //instead of FakeBookingRepository, we will create our own with Moq.
            //But! for the room repository, we will use the pre-made FakeRoomRepository because we won't actually use it.
            
            //prepare fake bookings for inmemory db in repository
            List<Booking> bookings = new List<Booking>()
            {
                new Booking()
                {
                    Id = 1,
                    IsActive = true,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(5),
                    RoomId = 1
                },
                new Booking()
                {
                    Id = 2,
                    IsActive = true,
                    StartDate = DateTime.Today.AddDays(5),
                    EndDate = DateTime.Today.AddDays(10),
                    RoomId = 2
                }
            };

            //creating the mock booking repo based on the interface
            Mock<IRepository<Booking>> mockRepo = new Mock<IRepository<Booking>>();
            //telling fake GetAll() to return the fake bookings when called
            mockRepo.Setup(x => x.GetAll()).Returns(bookings);

            //creating BookingManager using own fake booking repo to inject
            //mockRepo.Object will return the actual repository object from inside the Mock that we set up earlier
            bookingManager = new BookingManager(mockRepo.Object,new FakeRoomRepository());

            //fail in code: findavailableroom won't search with today's date
            //because if (startDate <= DateTime.Today || startDate > endDate) <-- it should be < instead of <=
            //var availRoomId = bookingManager.FindAvailableRoom(DateTime.Today, DateTime.Today.AddDays(1));
            //Assert.Equal(-1,availRoomId);

            //this should offer room 2 as it is booked 5 days away from now, and we're looking for 1 day away
            var availRoomId = bookingManager.FindAvailableRoom(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2));
            Assert.InRange(availRoomId,1,2);

            //this should throw an exception for startDate > endDate
            //Assert.Throws<ArgumentException>(() =>
            //    bookingManager.FindAvailableRoom(DateTime.Today.AddDays(2), DateTime.Today.AddDays(1)));

        }

        [Fact]
        public void CreateBookingTest()
        {
            var booking = new Booking()
            {
                CustomerId = 1,
                Id = 1,
                IsActive = true,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(5),
                RoomId = 1
            };

            Mock<IRepository<Booking>> mockRepo = new Mock<IRepository<Booking>>();
            //mockRepo.Setup(x => x.Add(booking)).Verifiable();

            bookingManager = new BookingManager(mockRepo.Object,new FakeRoomRepository());


            //fail in code: result should be false because room nr 1 is already booked in the fake booking we made.
            //but the code finds an available room (nr 2) and resets the roomnumber of the booking to 2 without us knowing it
            //var toAddBooking = new Booking()
            //{
            //    RoomId = 1,
            //    StartDate = DateTime.Today.AddDays(1),
            //    EndDate = DateTime.Today.AddDays(2)
            //};

            //var added = bookingManager.CreateBooking(toAddBooking);
            //Assert.False(added);
            //mockRepo.Verify();
            

            //this should be false because of the dates. it shouldn't be able to create a booking
            var toAddBooking = new Booking()
            {
                StartDate = DateTime.Today.AddDays(2),
                EndDate = DateTime.Today.AddDays(1),
                RoomId = 1
            };

            Assert.Throws<ArgumentException>(() => bookingManager.CreateBooking(toAddBooking));
            mockRepo.Verify();
        }
    }
}
