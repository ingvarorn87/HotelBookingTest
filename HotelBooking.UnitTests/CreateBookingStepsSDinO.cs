using System;
using TechTalk.SpecFlow;

namespace HotelBooking.UnitTests
{
    [Binding]
    public class CreateBookingStepsSDinO
    {
        [Given(@"I have entered ([0-9]*),([0-9]*),([0-9]*) as start date")]
        public void GivenIHaveEnteredAsStartDate(int year, int month, int day)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I have entered ([0-9]*),([0-9]*),([0-9]*) as end date")]
        public void GivenIHaveEnteredAsEndDate(int year, int month, int day)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I press book")]
        public void WhenIPressBook()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the result should be false")]
        public void ThenTheResultShouldBeFalse()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
