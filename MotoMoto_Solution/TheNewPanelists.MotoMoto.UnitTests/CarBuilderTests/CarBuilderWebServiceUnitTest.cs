using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Xunit;
using TheNewPanelists.MotoMoto.WebServices.CarBuilder.Controllers;

namespace TheNewPanelists.MotoMoto.UnitTests.CarBuilderTests
{
    public class CarBuilderWebServiceUnitTest
    {

        //[Fact]
        //public void CreateValidCar()
        //{
        //    string make = "Honda";
        //    string model = "Accord";
        //    string year = "2005";

        //    CarBuilderController controller = new CarBuilderController();

        //    IActionResult result = controller.CreateCar() //DUN DUN DUN

        //    Dictionary<string, string> response = new Dictionary<string, string>
        //    {
        //        { "message", "Car Successfully Created" },
        //    };

        //    var compareResult = new OkObjectResult(response);
        //    bool comparison = false;

        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    var okResultDict = okResult.Value as Dictionary<string, string>;

        //    if (okResultDict is not null)
        //    {
        //        comparison = okResultDict["message"] == "Flag Successfully Created";
        //    }

        //    Assert.True(comparison);
        //}
    }
}
