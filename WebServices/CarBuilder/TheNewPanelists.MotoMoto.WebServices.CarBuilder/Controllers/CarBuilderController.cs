using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.DataAccess.Implementations.CarBuilder;

namespace TheNewPanelists.MotoMoto.WebServices.CarBuilder.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class CarBuilderController : ControllerBase
    {
        private readonly CarBuildDataAccess _carBuildDataAccess = new CarBuildDataAccess();

        //private readonly ILogger<CarBuilderController> _logger;

        //public CarBuilderController(ILogger<CarBuilderController> logger)
        //{
        //    _logger = logger;
        //}

        [HttpGet("cartype")]
        public IActionResult GetCarTypes()
        {
            CarBuildService service = new CarBuildService(_carBuildDataAccess);
            CarBuildManager manager = new CarBuildManager(service);

            try
            {
                IList<CarTypeModel> retrieveAllCarTypes = manager.RetrieveAllCarTypes();
                return Ok(retrieveAllCarTypes);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("carbuild")]
        public IActionResult GetModifiedCarBuilds()
        {
            CarBuildService service = new CarBuildService(_carBuildDataAccess);
            CarBuildManager manager = new CarBuildManager(service);

            try
            {
                IList<ModifyCarBuildModel> retrieveAllModifiedCars = manager.RetrieveAllModifiedCarBuilds();
                return Ok(retrieveAllModifiedCars);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}