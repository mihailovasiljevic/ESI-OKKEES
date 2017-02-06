using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using System.ServiceModel;
using Connection;
using BufferClient;
using Common;
using NUnit.Framework;

namespace BDD_BufferStatistics
{
    [Binding]
    public class BufferStatisticsFeatureSteps
    {
        private DBRepository repository = null;
        private Dictionary<string, Dictionary<string, double>> dict1;
        private Dictionary<string, Dictionary<string, double>> dict2;

        [Given(@"I have client and service started")]
        public void GivenIHaveClientAndServiceStarted()
        {
            repository = new DBRepository("localhost", "esi-oikkes-test", "root", "root");
            repository.DeleteEverything("dumping_property");
            repository.InsertIntoDumpingPropTable(Codes.ANALOG, 23.2);
        }
        
        [When(@"I have entered (.*) as a start date and (.*) as a end date for no result")]
        public void WhenIHaveEnteredAsAStartDateAndAsAEndDateForNoResult(string p0, string p1)
        {
            string[] splitsStartDate = new string[3];
            string[] splitsEndDate = new string[3];
            splitsStartDate = p0.Split('-');
            splitsEndDate = p1.Split('-');
            string startYearString = splitsStartDate[0];
            string startMonthString = splitsStartDate[1];
            string startDayString = splitsStartDate[2];
            int startYear = Convert.ToInt32(startYearString);
            int startMonth = Convert.ToInt32(startMonthString);
            int startDay = Convert.ToInt32(startDayString);
            string endYearString = splitsEndDate[0];
            string endMonthString = splitsEndDate[1];
            string endDayString = splitsEndDate[2];
            int endYear = Convert.ToInt32(endYearString);
            int endMonth = Convert.ToInt32(endMonthString);
            int endDay = Convert.ToInt32(endDayString);
            DateTime startDate = new DateTime(startYear, startMonth, startDay);
            DateTime endDate = new DateTime(endYear, endMonth, endDay);
            dict1 = repository.GetDataForBufferStatistics(startDate, endDate);
        }
        
        [When(@"I have entered (.*) as a start date and (.*) as a end date for result")]
        public void WhenIHaveEnteredAsAStartDateAndAsAEndDateForResult(string p0, string p1)
        {
            string[] splitsStartDate = new string[3];
            string[] splitsEndDate = new string[3];
            splitsStartDate = p0.Split('-');
            splitsEndDate = p1.Split('-');
            string startYearString = splitsStartDate[0];
            string startMonthString = splitsStartDate[1];
            string startDayString = splitsStartDate[2];
            int startYear = Convert.ToInt32(startYearString);
            int startMonth = Convert.ToInt32(startMonthString);
            int startDay = Convert.ToInt32(startDayString);
            string endYearString = splitsEndDate[0];
            string endMonthString = splitsEndDate[1];
            string endDayString = splitsEndDate[2];
            int endYear = Convert.ToInt32(endYearString);
            int endMonth = Convert.ToInt32(endMonthString);
            int endDay = Convert.ToInt32(endDayString);
            DateTime startDate = new DateTime(startYear, startMonth, startDay);
            DateTime endDate = new DateTime(endYear, endMonth, endDay);
            dict2 = repository.GetDataForBufferStatistics(startDate, endDate);
        }
        
        [Then(@"the result should be (.*), there are no data")]
        public void ThenTheResultShouldBeThereAreNoData(int p0)
        {
            Assert.AreEqual(dict1.Count, p0);
        }
        
        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int p0)
        {
            Assert.AreEqual(dict2.Count, p0);
        }
    }
}
