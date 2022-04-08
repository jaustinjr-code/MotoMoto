using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using TheNewPanelists.ServiceLayer.Logging;
using VueJsToNetCore.ViewModel;
using System.Web;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;


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
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            string username = user.getUsername();
            string password = user.getPassword();
            string connectionString = $"server=localhost;user=root;database=motomoto_um;port=3306;password=password;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection open");
                string query = "SELECT * FROM USER u WHERE u.username = '" + username+ "' AND u.password = '" + password + "'";
                MySqlCommand cmd = new MySqlCommand(query,connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    
                    Dictionary<string, string> log = new Dictionary<string, string>();
                    string operation = "SERVER";
                    log.Add("username", username);
                    log.Add("level", "INFO");
                    log.Add("userId", "0");
                    log.Add("DSCRIPTION", "Login Success");
                    LogService logservice = new LogService(operation, log, true);
                    HomeController controller = new HomeController(new HttpContextAccessor());
                    controller.Set("UserId", user.getID().ToString(), 60);

                    Console.WriteLine(controller.Get("UserId"));

                    return Ok(true);
                }
                else
                {
                    Dictionary<string, string> log = new Dictionary<string, string>();
                    string operation = "SERVER";
                    log.Add("username", "Not a user");
                    log.Add("level", "INFO");
                    log.Add("userId", "0");
                    log.Add("DSCRIPTION", "Incorrect login credentials");
                    LogService logservice = new LogService(operation, log, true);
                    return Ok(false);
                }
            }
            catch
            {
                Dictionary<string, string> log = new Dictionary<string, string>();
                string operation = "SERVER";
                log.Add("username", "Not a user");
                log.Add("level", "ERROR");
                log.Add("userId", "0");
                log.Add("DSCRIPTION", "Login ERROR");
                LogService logservice = new LogService(operation, log, true);
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            finally
            {
                connection.Close();
                   
            }
            
        }

 

    }
}
