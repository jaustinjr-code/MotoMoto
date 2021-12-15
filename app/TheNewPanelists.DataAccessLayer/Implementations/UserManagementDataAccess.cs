using System.Linq;
using MySql.Data.MySqlClient;


namespace TheNewPanelists.DataAccessLayer
{
    class UserManagementDataAccess : IDataAccess
    {   
        private Dictionary<string, string> userInfo {get; set;}
        private string operation { get; set; }
        private MySqlConnection mySqlConnection = null;
        public UserManagementDataAccess ()
        {
        }
        public UserManagementDataAccess (string operation, Dictionary<string, string> userInfo) 
        {
            this.userInfo = userInfo;
            this.operation = operation;
        }
        private void BuildTempUser() 
        {
            using(mySqlConnection = new MySqlConnection("server=localhost;user=MotoMotoA;password=password;"))
            using (MySqlCommand cmd = new MySqlCommand("CREATE USER 'tempuser'@'localhost' IDENTIFIED BY 'password';", mySqlConnection))
            {
                try 
                {
                    mySqlConnection.Open();
                    Console.WriteLine("Connection Open...");
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Temp User Created...");
                } 
                catch (Exception e) 
                {
                    Console.WriteLine("Exited Program with Exit "+e);
                }
                mySqlConnection.Close();
            }
            EstablishMariaDBConnection();
        }
        private void DeleteTempUser() 
        {
            using (mySqlConnection = new MySqlConnection("server=localhost;user=MotoMotoA;password=password;"))
            using (MySqlCommand cmd = new MySqlCommand("DROP USER 'tempuser'@'localhost'", mySqlConnection)) 
            {
                try 
                {
                    mySqlConnection.Open();
                    Console.WriteLine("Connection Open...");
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Temp User Dropped...");
                }
                catch (Exception e) 
                {
                    Console.WriteLine("Exited Program with Exit "+e);
                }
                mySqlConnection.Close();
            }
        }
        public bool EstablishMariaDBConnection() 
        {    
            // NOTE: hardcoded, will be different based on your naming
            string connectionString = "server=localhost;user=MotoMotoA;database=motomotousermanagement;port=3306;password=password";
            // @"Data Source=localhost;User ID=admin_MM_test;Password=l23";

            try 
            {
                mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                Console.WriteLine("Connection open");
            } 
            catch (Exception e) 
            {
                Console.WriteLine("Invalid Connection "+e+ " creating temp user now");
                this.BuildTempUser();
            }
            // SqlGenerator
            // run query and compare against query
            mySqlConnection.Close();
            return false;
        }
        public string SqlGenerator()
        {   
            if (this.operation == "FIND") 
            {
                return this.FindUser();
            }
            else if (this.operation == "CREATE") 
            {
                return this.CreateUser();
            } 
            else if (this.operation == "DROP") 
            {
                return this.DropUser();
            }
            else if (this.operation == "UPDATE") 
            {
                return this.Update();
            }
            return "";
        }
        private string FindUser() 
        {
            return "SELECT u FROM User u WHERE u.username ="+this.userInfo["username"]+";";
        }
        private string CreateUser() 
        {
            return "INSERT INTO USER VALUES (NULL, NULL, "+this.userInfo["username"]+", "
                   +this.userInfo["password"]+", "+this.userInfo["email"]+", false, false);";
        }
        private string DropUser() 
        {
            return "DELETE u FROM USER u WHERE u.username ="+this.userInfo["username"]+";";
        }
        private string Update() 
        {
            return "UPDATE USER u SET u.username = "+this.userInfo["newusername"]+
                    " WHERE u.username="+this.userInfo["username"];
        }
    }
}