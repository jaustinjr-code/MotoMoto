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

        [HttpGet("CarType")]
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

        [HttpPost("CreateCar")]
        public IActionResult CreateCar(CarTypeModel car)
        {
            CarBuildService service = new CarBuildService(_carBuildDataAccess);
            CarBuildManager manager = new CarBuildManager(service);
            bool result = manager.SaveCarTypeManager(car); //What should i put here if i want to save it to DataStoreCarBuilds
            if (result)
            {
                Dictionary<string, string> response = new Dictionary<string, string>
            {
                { "message", "Flag Successfully Created" },
            };
                return Ok(response);
            }
            else
            {
                Dictionary<string, string> response = new Dictionary<string, string>
            {
                { "message", "Error Flag Could Not Be Created" },
            };
                return BadRequest(response);
            }
        }

        [HttpGet("CarBuild")]
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

        [HttpPut("UpdateCar")]
        public IActionResult UpdateCar(ModifyCarBuildModel modifyCar) // What should I take in????
        {
            CarBuildService service = new CarBuildService(_carBuildDataAccess);
            CarBuildManager manager = new CarBuildManager(service);
            bool result = manager.SaveCarModificationsManager(modifyCar); //What should i put here if i want to save it to DataStoreCarBuilds
            if (result)
            {
                Dictionary<string, string> response = new Dictionary<string, string>
            {
                { "message", "Flag Successfully Created" },
            };
                return Ok(response);
            }
            else
            {
                Dictionary<string, string> response = new Dictionary<string, string>
            {
                { "message", "Error Flag Could Not Be Created" },
            };
                return BadRequest(response);
            }
        }
    }
}