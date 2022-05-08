using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.WebServices.MeetingPointDirections;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class MeetingPointDirectionsWebServiceTest
    {
        // Test to determine if API can retrieve event location using valid EventID
        [Fact]
        public void EventLocationRetrieval_ValidEventID()
        {
            // Arrange
            MeetingPointDirectionsController retrievalController = new MeetingPointDirectionsController();
            bool assertValue;

            // Act
            var actionResult = retrievalController.FetchEventLocation(1);

            // Assert
            var okResult = (ObjectResult)actionResult;
            if (okResult.StatusCode == 200)
            {
                assertValue = true;
            }
            else
            {
                assertValue = false;
            }
            Assert.True(assertValue);
        }

        // Test to determine if API can retrieve event location within 10 seconds
        [Fact]
        public void EventLocationRetrieval_ExecutionWithin10Seconds()
        {
            // Arrange
            MeetingPointDirectionsController retrievalController = new MeetingPointDirectionsController();
            bool result;
            Stopwatch stopwatch = new Stopwatch();

            // Act
            stopwatch.Start();
            retrievalController.FetchEventLocation(1);
            stopwatch.Stop();

            // Assert
            if(stopwatch.Elapsed.TotalSeconds <= 10)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            Assert.True(result);
        }
    }
}
