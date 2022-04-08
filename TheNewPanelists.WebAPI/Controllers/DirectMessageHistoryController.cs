using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using VueJsToNetCore.ViewModel;

namespace TheNewPanelists.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectMessageHistoryController : Controller
    {
        [HttpOptions]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public List<String> GetMessageHistory(int currentUser)
        {
            List<String> users = new List<String>();
            string connectionString = $"server=localhost;user=root;database=motomoto_um;port=3306;password=password;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection open");
                string query = "SELECT username from USER u INNER JOIN MESSAGEHISTORY m ON u.userId = m.recieverID WHERE m.senderID = '" + currentUser + "'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(reader.GetString(0));
                }
                reader.Close();
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        [HttpPost]
        public ActionResult setMessageHistory(int currentUserID, int recieverID)
        {
            //validate that recieverID is a valid user
            //validate that the current user doesn't have a message open already with the reciever
            string connectionString = $"server=localhost;user=root;database=motomoto_um;port=3306;password=password;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection open");
                string query = "INSERT INTO MESSAGEHISTORY (senderID, recieverID) VALUES (" + currentUserID + "," + recieverID + ");";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                return Ok(true);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
