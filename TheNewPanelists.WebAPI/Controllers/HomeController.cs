using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using TheNewPanelists.ServiceLayer.Logging;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace TheNewPanelists.WebAPI.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        string sessionId = "";
        [HttpOptions]
        public IActionResult Index()
        {
            return NoContent();
        }
        [HttpPost]
        public IActionResult login(string username, string password, Dictionary<string, string>result)
        {
            DataAccessLayer.UserManagementDataAccess manager = new DataAccessLayer.UserManagementDataAccess();
            result.Add("username", username); 
            result.Add("password", password);
            string connectionString = $"server=localhost;user=root;database=motomoto_um;port=3306;password=password;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection open");
                string query = "SELECT * FROM USER u WHERE u.username = '" + username+ "' AND u.password = '" + password + "'";
                MySqlCommand cmd = new MySqlCommand(query,connection);
                MySqlDataReader reader = cmd.ExecuteReader(); ;
                if(reader.HasRows)
                {
                    sessionId = HttpContext.Session.Id;

                    Dictionary<string, string> log = new Dictionary<string, string>();
                    string operation = "SERVER";
                    log.Add("username", username);
                    log.Add("level", "INFO");
                    log.Add("userId", "0");
                    log.Add("DSCRIPTION", "Login Success");
                    LogService logservice = new LogService(operation, log, true);
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
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            finally
            {
                connection.Close();
                   
            }
            
        }



    }
}
