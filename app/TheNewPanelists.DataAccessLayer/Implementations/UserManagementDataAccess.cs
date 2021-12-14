using System.Linq;
using MySql.Data.MySqlClient;


namespace TheNewPanelists.DataAccessLayer
{
    class UserManagementDataAccess : IDataAccess
    {   
        private Dictionary<string, string> userInfo {get; set;}
        private string operation { get; set; }

        private UserManagementDataAccess (string operation, Dictionary<string, string> userInfo) {
            this.userInfo = userInfo;
            this.operation = operation;
        }

        private void BuildTempUser() {
            using(MySqlConnection conn = new MySqlConnection("server=localhost;user=MotoMotoA;password=password;"))
            using (MySqlCommand cmd = new MySqlCommand("CREATE USER 'tempuser'@'localhost' IDENTIFIED BY 'password';", conn))
            {
                try {
                    conn.Open();
                    Console.WriteLine("Connection Open...");
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Temp User Created...");
                } catch (Exception e) {
                    Console.WriteLine("Exited Program with Exit "+e);
                }
                conn.Close();
            }
        }

        public void DeleteTempUser() {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;password=password;"))
            using (MySqlCommand cmd = new MySqlCommand("DROP USER 'tempuser'@'localhost'", conn)) {
                try {
                    conn.Open();
                    Console.WriteLine("Connection Open...");
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Temp User Dropped...");
                } catch (Exception e) {
                    Console.WriteLine("Exited Program with Exit "+e);
                }
                conn.Close();
            }
        }
        public bool EstablishMariaDBConnection()
        {
            MySqlConnection mySqlConnection;
            // NOTE: hardcoded, will be different based on your naming
            string connectionString = "server=localhost;user=MotoMotoA;database=motomotousermanagement;port=3306;password=password";
            // @"Data Source=localhost;User ID=admin_MM_test;Password=l23";

            mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();

            // SqlGenerator
            // run query and compare against query
            Console.WriteLine("Connection open");

            mySqlConnection.Close();

            return false;
        }

        public string SqlGenerator()
        {   
            return "";
        }
    }
}




        // private string FindUser() {
        //     String query = "";
            
        //     if (this.userInfo.ContainsKey("username")) {
        //             query = "SELECT u FROM User u WHERE u.username ="+this.userInfo["username"];
        //             return query;  
        //         }
        //     Console.WriteLine("Query not created due to invalid user account");
        //     return(query);
        // }

        // private string CreateUser() {
        //     String query = "";
        //     String username="";
        //     String password="";
        //     String email="";
                
        //     if (this.userInfo.ContainsKey("username")){
        //             username = this.userInfo["username"];
        //     } else if (this.userInfo.ContainsKey("password")) {
        //             password = this.userInfo["password"];
        //     } else if (this.userInfo.ContainsKey("email")) {
        //             email = this.userInfo["email"];
        //     }
        //     if (!username.Equals("") && !password.Equals("") && !email.Equals("")){
        //         query = "INSERT INTO USER VALUES (NULL, NULL, "+username+", "+password+", "+email+", false, false;";
        //         Console.WriteLine(query);
        //         return query;
        //     }
        //     return query;
        // }