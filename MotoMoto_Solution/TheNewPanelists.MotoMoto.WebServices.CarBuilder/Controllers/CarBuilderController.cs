using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.DataAccess.Implementations.CarBuilder;
using TheNewPanelists.MotoMoto.Models.CarbuilderModels;

namespace TheNewPanelists.MotoMoto.WebServices.CarBuilder.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarBuilderController : ControllerBase
    {
        private readonly CarBuildDataAccess _carBuildDataAccess = new CarBuildDataAccess();


        // [HttpGet("GetCarTypes")]
        [Route("GetCarTypes")]
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

        // [HttpPost("CreateCarType")]
        [Route("CreateCarType")]
        public IActionResult CreateCarTypes(CarTypeModel car)
        {
            CarBuildService service = new CarBuildService(_carBuildDataAccess);
            CarBuildManager manager = new CarBuildManager(service);
            bool result = manager.SaveCarTypeManager(car);
            if (result)
            {
                Dictionary<string, string> response = new Dictionary<string, string>
            {
                { "message", "Car Successfully Created" },
            };
                return Ok(response);
            }
            else
            {
                Dictionary<string, string> response = new Dictionary<string, string>
            {
                { "message", "Error Car Could Not Be Created" },
            };
                return BadRequest(response);
            }
        }

        // [HttpGet("CarBuild")]
        [Route("CarBuild")]
        public IActionResult GetModifiedCarBuilds(string username)
        {
            CarBuildService service = new CarBuildService(_carBuildDataAccess);
            CarBuildManager manager = new CarBuildManager(service);

            try
            {
                IList<UserCarBuildModel> retrieveAllModifiedCars = manager.RetrieveAllModifiedCarBuilds(username);
                //UserCarBuildModel retrieveAllModifiedCars = manager.RetrieveAllModifiedCarBuilds(username);
                return Ok(retrieveAllModifiedCars);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        //[HttpPost("CreateCarPart")]
        //public IActionResult CreateCarPart(ModifyCarBuildModel modifyCar) 
        //{
        //    CarBuildService service = new CarBuildService(_carBuildDataAccess);
        //    CarBuildManager manager = new CarBuildManager(service);
        //    bool result = manager.SaveCarModificationsManager(modifyCar); 
        //    if (result)
        //    {
        //        Dictionary<string, string> response = new Dictionary<string, string>
        //        {
        //            { "message", "Car Successfully Modified" },
        //        };
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        Dictionary<string, string> response = new Dictionary<string, string>
        //        {
        //            { "message", "Error Could Not Modify Car" },
        //        };
        //        return BadRequest(response);
        //    }
        //}

        // [HttpGet("GetCarPart")]
        [Route("GetCarPart")]
        public IActionResult GetCarPart()
        {
            CarBuildService service = new CarBuildService(_carBuildDataAccess);
            CarBuildManager manager = new CarBuildManager(service);

            try
            {
                IList<ModifyCarBuildModel> retrieveAllCarParts = manager.RetrieveAllCarParts();
                return Ok(retrieveAllCarParts);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // [HttpPut("UpdateCar")]
        [Route("UpdateCar")]
        public IActionResult UpdateCar(UpdateCarModel updateCarBuildModel) // What should I take in????
        {
            CarBuildService service = new CarBuildService(_carBuildDataAccess);
            CarBuildManager manager = new CarBuildManager(service);
            bool result = manager.UpdateCarManager(updateCarBuildModel); //What should i put here if i want to save it to DataStoreCarBuilds
            if (result)
            {
                Dictionary<string, string> response = new Dictionary<string, string>
                {
                    { "message", "Car Successfully Modified" },
                };
                return Ok(response);
            }
            else
            {
                Dictionary<string, string> response = new Dictionary<string, string>
        {
            { "message", "Error Could Not Modify Car" },
        };
                return BadRequest(response);
            }
        }
    }
}
