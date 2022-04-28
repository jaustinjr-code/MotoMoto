using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.PartFlagging.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PartFlaggingController : ControllerBase
{

    [HttpPost("CreateFlag")]
    public IActionResult CreateFlag(string partNumber, string carMake, string carModel, string carYear) 
    {
        PartFlaggingBusinessLayer partFlaggingBusinessLayer = new PartFlaggingBusinessLayer();
        bool result = partFlaggingBusinessLayer.handleFlagCreation(partNumber, carMake, carModel, carYear);
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

    [HttpPost("DecrementFlagCount")]
    public IActionResult DecrementFlagCount(string partNumber, string carMake, string carModel, string carYear) 
    {
        PartFlaggingBusinessLayer partFlaggingBusinessLayer = new PartFlaggingBusinessLayer();
        bool result = partFlaggingBusinessLayer.HandleFlagCountDecrement(partNumber, carMake, carModel, carYear);
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
    
    [HttpGet("IsPossibleIncompatibility")]
    public IActionResult IsPossibleIncompatibility(string partNumber, string carMake, string carModel, string carYear)
    {
        PartFlaggingBusinessLayer partFlaggingBusinessLayer = new PartFlaggingBusinessLayer();
        bool? result = partFlaggingBusinessLayer.HandleGetFlagCompatibility(partNumber, carMake, carModel, carYear);
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
