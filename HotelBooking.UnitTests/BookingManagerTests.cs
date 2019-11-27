using HotelBooking.Core;
using HotelBooking.UnitTests.Fakes;
using System;
using Xunit;

namespace HotelBooking.UnitTests
{
    public class BookingManagerTests
    {
        private IBookingManager bookingManager;

        public BookingManagerTests()
        {
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);
            IRepository<Booking> bookingRepository = new FakeBookingRepository(start, end);
            IRepository<Room> roomRepository = new FakeRoomRepository();
            bookingManager = new BookingManager(bookingRepository, roomRepository);
        }

        [Fact]
        public void CreateBookingTest1Case1()
        {
            var b = new Booking()
            {
                StartDate = DateTime.Today.AddDays(-1)
            };

            Assert.Throws<ArgumentException>(() => bookingManager.CreateBooking(b));
        }

        [Fact]
        public void CreateBookingTest1Case2()
        {
            var b = new Booking()
            {
                StartDate = DateTime.Today.AddDays(3),
                EndDate = DateTime.Today.AddDays(2)
            };

            Assert.Throws<ArgumentException>(() => bookingManager.CreateBooking(b));
        }

        [Fact]
        public void CreateBookingTest1Case3()
        {
            var b = new Booking()
            {
                StartDate = DateTime.Today.AddDays(21),
                EndDate = DateTime.Today.AddDays(23)
            };

            Assert.True(bookingManager.CreateBooking(b));
        }

        [Fact]
        public void CreateBookingTest2Case2()
        {
            var b = new Booking()
            {
                StartDate = DateTime.Today.AddDays(2),
                EndDate = DateTime.Today.AddDays(3)
            };

            Assert.True(bookingManager.CreateBooking(b));
        }

        [Fact]
        public void CreateBookingTest2Case3()
        {
            var b = new Booking()
            {
                StartDate = DateTime.Today.AddDays(22),
                EndDate = DateTime.Today.AddDays(25)
            };

            Assert.True(bookingManager.CreateBooking(b));
        }

        [Fact]
        public void CreateBookingTest2Case4()
        {
            var b = new Booking()
            {
                StartDate = DateTime.Today.AddDays(11),
                EndDate = DateTime.Today.AddDays(12) //it can be anything bigger than startdate
            };

            Assert.False(bookingManager.CreateBooking(b));
        }

        [Fact]
        public void CreateBookingTest2Case5()
        {
            var b = new Booking()
            {
                StartDate = DateTime.Today.AddDays(9),
                EndDate = DateTime.Today.AddDays(21)
            };
            Assert.False(bookingManager.CreateBooking(b));
        }
    }
}
