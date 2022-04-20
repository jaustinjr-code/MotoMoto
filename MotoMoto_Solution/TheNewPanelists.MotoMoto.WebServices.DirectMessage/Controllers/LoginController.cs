using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace TheNewPanelists.MotoMoto.WebServices.DirectMessage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        [HttpOptions]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("Login")]
        public IActionResult Login(string username, string password)
        {

            string connectionString = "server=moto-moto.crd4iyvrocsl.us-west-1.rds.amazonaws.com;user=dev_moto;database=pro_moto;port=3306;password=motomoto;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection open");
                string query = "SELECT * FROM User u WHERE u.username = '" + username + "' AND u.password = '" + password + "'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            finally
            {
                connection.Close();

            }

        }
    }
}
