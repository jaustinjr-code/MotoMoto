using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.PartFlagging.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PartFlaggingController : ControllerBase
{

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
