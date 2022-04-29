using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.PartFlagging.Controllers;

/// <summary>
/// Controller encapsulating all WebAPI methods pertaining to Part Flagging.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PartFlaggingController : ControllerBase
{

    /// <summary>
    /// Creates new flag or updates flag count for the existing matching flag in the part flags database.
    /// </summary>
    ///
    /// <param name="partNum">The number associated with the incompatible part</param>
    /// <param name="carMake">The name of the maker of the car that the part is incompatible with</param>
    /// <param name="carModel">The name of the model of the car that the part is incompatible with</param>
    /// <param name="carYear">The year of the model of the car that the part is incompatible with</param>
    ///
    /// <returns>Message stating whether or not flag creation was successful</returns>
    [HttpPost("CreateFlag")]
    public IActionResult CreateFlag(string partNum, string carMake, string carModel, string carYear) 
    {
        PartFlaggingBusinessLayer partFlaggingBusinessLayer = new PartFlaggingBusinessLayer();
        bool result = partFlaggingBusinessLayer.HandleFlagCreation(partNum, carMake, carModel, carYear);
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

    /// <summary>
    /// Decrements the flag count of the existing matching flag, or removes flag entirely if flag count reaches 0 after decrement.
    /// Returns to the caller an object representing the result of the operation.
    /// </summary>
    ///
    /// <param name="partNum">The number associated with the incompatible part</param>
    /// <param name="carMake">The name of the maker of the car that the part is incompatible with</param>
    /// <param name="carModel">The name of the model of the car that the part is incompatible with</param>
    /// <param name="carYear">The year of the model of the car that the part is incompatible with</param>
    ///
    /// <returns>Message stating whether or not flag decrement was successful</returns>
    [HttpPost("DecrementFlagCount")]
    public IActionResult DecrementFlagCount(string partNum, string carMake, string carModel, string carYear) 
    {
        PartFlaggingBusinessLayer partFlaggingBusinessLayer = new PartFlaggingBusinessLayer();
        bool result = partFlaggingBusinessLayer.HandleFlagCountDecrement(partNum, carMake, carModel, carYear);
        if (result)
        {
            Dictionary<string, string> response = new Dictionary<string, string>
            {
                { "message", "Flag Successfully Decremented" }, 
            };
            return Ok(response);
        }
        else
        {
            Dictionary<string, string> response = new Dictionary<string, string>
            {
                { "message", "Error Flag Could Not Be Decremented" }, 
            };
            return BadRequest(response);
        }
    }
    
    /// <summary>
    /// Returns a boolean value designating whether or not a part is incompatible 
    /// based on times that part has been flagged for that car by users.
    /// </summary>
    ///
    /// <param name="partNum">The number associated with the incompatible part</param>
    /// <param name="carMake">The name of the maker of the car that the part is incompatible with</param>
    /// <param name="carModel">The name of the model of the car that the part is incompatible with</param>
    /// <param name="carYear">The year of the model of the car that the part is incompatible with</param>
    ///
    /// <returns>Object containing incompatibility status or message describing operation failure</returns>
    [HttpGet("IsPossibleIncompatibility")]
    public IActionResult IsPossibleIncompatibility(string partNum, string carMake, string carModel, string carYear)
    {
        PartFlaggingBusinessLayer partFlaggingBusinessLayer = new PartFlaggingBusinessLayer();
        bool? result = partFlaggingBusinessLayer.HandleGetFlagCompatibility(partNum, carMake, carModel, carYear);
        if (result is not null)
        {
            if (result == true)
            {
                Dictionary<string, bool> response = new Dictionary<string, bool>
                {
                    { "isPossibleIncompatiblility",  true}, 
                };
                return Ok(response);
            }
            else
            {
                Dictionary<string, bool> response = new Dictionary<string, bool>
                {
                    { "isPossibleIncompatiblility",  false}, 
                };
                return Ok(response);
            }
        }
        else
        {
            Dictionary<string, string> response = new Dictionary<string, string>
            {
                { "message",  "Error Part Flag Invalid"}, 
            };
            return BadRequest(response);
        }
    }
}
