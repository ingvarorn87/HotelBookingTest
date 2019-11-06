﻿using System;
using HotelBooking.Core;
using HotelBooking.UnitTests.Fakes;
using TechTalk.SpecFlow;
using Xunit;

namespace HotelBooking.UnitTests
{
    [Binding]
    [Scope(Feature = "Create booking", Tag = "SDinB_EDinA")]
    public class CreateBookingStepsSDinB_EDinA
    {
        private Booking booking = new Booking();
        private bool result;
        static DateTime start = new DateTime(2019, 12, 9);
        static DateTime end = new DateTime(2019, 12, 15);
        static IRepository<Booking> bookingRepository = new FakeBookingRepository(start, end);
        static IRepository<Room> roomRepository = new FakeRoomRepository();
        private BookingManager bookingManager = new BookingManager(bookingRepository, roomRepository);

        [Given(@"I have entered (.*),(.*),(.*) as start date")]
        public void GivenIHaveEnteredAsStartDate(string p0, string p1, string p2)
        {
            Int32.TryParse(p0, out int year);
            Int32.TryParse(p1, out int month);
            Int32.TryParse(p2, out int day);
            booking.StartDate = new DateTime(year, month, day);
        }
        
        [Given(@"I have entered (.*),(.*),(.*) as end date")]
        public void GivenIHaveEnteredAsEndDate(string p0, string p1, string p2)
        {
            Int32.TryParse(p0, out int year);
            Int32.TryParse(p1, out int month);
            Int32.TryParse(p2, out int day);
            booking.EndDate = new DateTime(year, month, day);
        }
        
        [When(@"I press book")]
        public void WhenIPressBook()
        {
            result = bookingManager.CreateBooking(booking);
        }

        [Then(@"the result should be false")]
        public void ThenTheResultShouldBeFalse()
        {
            Assert.False(result);
        }
    }
}
