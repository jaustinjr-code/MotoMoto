using Xunit;
using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.WebServices.PartFlagging.Controllers;
using TheNewPanelists.MotoMoto.Models;
using System.Collections.Generic;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class PartFlaggingWebServiceUnitTest
    {
        [Fact]
        public void CreateValidFlag()
        {
            string partNum = "1";
            string carMake = "Honda";
            string carModel = "Accord";
            string carYear = "2022";

            PartFlaggingController controller = new PartFlaggingController();

            IActionResult result = controller.CreateFlag(partNum, carMake, carModel, carYear);

            Dictionary<string, string> response = new Dictionary<string, string>
            {
                { "message", "Flag Successfully Created" }, 
            };

            var compareResult = new OkObjectResult(response);
            bool comparison = false;

            var okResult = Assert.IsType<OkObjectResult>(result);
            var okResultDict = okResult.Value as Dictionary<string, string>;

            if (okResultDict is not null)
            {
                comparison = okResultDict["message"] == "Flag Successfully Created";
            }
            
            Assert.True(comparison);
        }

        [Fact]
        public void CreateInvalidFlag()
        {
            string partNum = "";
            string carMake = "";
            string carModel = "";
            string carYear = "";

            PartFlaggingController controller = new PartFlaggingController();

            IActionResult result = controller.CreateFlag(partNum, carMake, carModel, carYear);

            bool comparison = false;

            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            var badResultDict = badResult.Value as Dictionary<string, string>;

            if (badResultDict is not null)
            {
                comparison = badResultDict["message"] == "Error Flag Could Not Be Created";
            }
            
            Assert.True(comparison);
        }

        [Fact]
        public void DecrementValidFlag()
        {
            const string testName = "DecrementValidFlag";

            string partNum = testName;
            string carMake = testName;
            string carModel = testName;
            string carYear = "2022";

            //Ensure flag exists
            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();
            PartFlaggingBusinessLayer partFlaggingBusiness = new PartFlaggingBusinessLayer();
            partFlaggingDataAccess.createOrIncrementFlag(partFlaggingBusiness.createFlagModel(partNum, carMake, carModel, carYear));
            
            PartFlaggingController controller = new PartFlaggingController();

            IActionResult result = controller.CreateFlag(partNum, carMake, carModel, carYear);

            Dictionary<string, string> response = new Dictionary<string, string>
            {
                { "message", "Flag Successfully Created" }, 
            };

            var compareResult = new OkObjectResult(response);
            bool comparison = false;

            var okResult = Assert.IsType<OkObjectResult>(result);
            var okResultDict = okResult.Value as Dictionary<string, string>;

            if (okResultDict is not null)
            {
                comparison = okResultDict["message"] == "Flag Successfully Created";
            }
            
            Assert.True(comparison);
        }

        [Fact]
        public void DecrementInvalidFlag()
        {
            string partNum = "";
            string carMake = "";
            string carModel = "";
            string carYear = "";

            PartFlaggingController controller = new PartFlaggingController();

            IActionResult result = controller.DecrementFlagCount(partNum, carMake, carModel, carYear);

            bool comparison = false;

            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            var badResultDict = badResult.Value as Dictionary<string, string>;

            if (badResultDict is not null)
            {
                comparison = badResultDict["message"] == "Error Flag Could Not Be Decremented";
            }
            
            Assert.True(comparison);
        }

        [Fact]
        public void GetValidFlag()
        {
            string partNum = "1";
            string carMake = "Honda";
            string carModel = "Accord";
            string carYear = "2022";

            PartFlaggingController controller = new PartFlaggingController();

            IActionResult result = controller.IsPossibleIncompatibility(partNum, carMake, carModel, carYear);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var okResultDict = okResult.Value as Dictionary<string, bool>;

            bool operationSuccess = false; 
            if (okResultDict is not null)
            {
                if (okResultDict.ContainsKey("isPossibleIncompatiblility"))
                {
                    operationSuccess = true;
                }
            }
            
            Assert.True(operationSuccess);
        }

        [Fact]
        public void GetInvalidFlag()
        {
            string partNum = "";
            string carMake = "";
            string carModel = "";
            string carYear = "";

            PartFlaggingController controller = new PartFlaggingController();

            IActionResult result = controller.IsPossibleIncompatibility(partNum, carMake, carModel, carYear);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }

    
}