using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using TheNewPanelists.MotoMoto.ServiceLayer;
using System;



namespace TheNewPanelists.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpOptions]
        public IActionResult Index()
        {
            return NoContent();
        }






    }
}