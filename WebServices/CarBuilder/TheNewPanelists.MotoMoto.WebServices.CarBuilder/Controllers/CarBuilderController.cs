using Microsoft.AspNetCore.Mvc;

namespace TheNewPanelists.MotoMoto.WebServices.CarBuilder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarBuilderController : ControllerBase
    {

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<CarBuilderController> _logger;

        public CarBuilderController(ILogger<CarBuilderController> logger)
        {
            _logger = logger;
        }



        //[HttpGet]
        //[Route("GetCarType")]
        //public IActionResult GetCarType()
        //{
        //    try
        //    {CarBuilderBLayer directMessageBusinessLayer = new DirectMessageBusinessLayer();
        //        return Ok(directMessageBusinessLayer.GetMessages(sender, receiver));
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}