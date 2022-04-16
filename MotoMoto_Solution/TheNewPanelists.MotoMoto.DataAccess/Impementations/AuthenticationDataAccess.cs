using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models;


namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class AuthenticationDataAccess : IDataAccess
    {
        private MySqlConnection? mySqlConnection { get; set; }

        private string _connectionString = "server=localhost;user=dev_moto;database=dev_UM;port=3306;password=motomoto;"; //write config so this only appears once

        public AuthenticationDataAccess() {}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public AuthenticationDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        private bool ExecuteQuery(MySqlCommand command)
        {
            switch (command.ExecuteNonQuery())
            {
                case 1:
                    mySqlConnection!.Close();
                    return true;
                default:
                    mySqlConnection!.Close();
                    return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool EstablishMariaDBConnection()
        {
            try
            {
                mySqlConnection = new MySqlConnection(_connectionString);
                mySqlConnection.Open();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationModel"></param>
        /// <exception cref="NullReferenceException"></exception>
        public void InsertAuthenticatedUser(AuthenticationModel authenticationModel)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            DataStoreUser dataStoreUser = RetrieveDataStoreSpecifiedUserEntity(authenticationModel!.Username!);
            if (dataStoreUser == null) 
                return;
            using (var command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = "INSERT INTO AUTHENTICATION (userId, username, attempts)"
                                    + "VALUES ((SELECT userId FROM User WHERE userId = @v1), (SELECT"
                                    + "username FROM User WHERE userId = @v2), "
                                    + "@v3);";
                var parameters = new MySqlParameter[3];
                parameters[0] = new MySqlParameter("@v1", dataStoreUser!.UserId);
                parameters[1] = new MySqlParameter("@v2", dataStoreUser!.UserId);
                parameters[2] = new MySqlParameter("@v3", authenticationModel!.Attempts);
                
                command.Parameters.AddRange(parameters);
                ExecuteQuery(command);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public DataStoreUser RetrieveDataStoreSpecifiedUserEntity(string username)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = "SELECT * FROM USER U WHERE U.USERNAME = @v1";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", username);

                command.Parameters.AddRange(parameters);

                MySqlDataReader myReader = command.ExecuteReader();
                DataStoreUser returnUser = new DataStoreUser();
                while (myReader.Read())
                {
                    returnUser.UserId = myReader.GetInt32("userId");
                    returnUser._userType = myReader.GetString("typeName");
                    returnUser._username = myReader.GetString("username");
                    returnUser._password = myReader.GetString("password");
                    returnUser._email = myReader.GetString("email");
                    returnUser._salt = myReader.GetString("salt");
                }
                myReader.Close();
                mySqlConnection!.Close();
                return returnUser;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationModel"></param>
        /// <exception cref="NullReferenceException"></exception>
        public void UpdateAuthenticationReset(AuthenticationModel authenticationModel)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (var command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = "UPDATE AUTHENTICATION SET attempts = '@v1'"
                                    + ", otp = '@v2', otpExpireTime = '@v3', WHERE"
                                    + "userId = @v4;";
                var parameters = new MySqlParameter[4];
                parameters[0] = new MySqlParameter("@v1", authenticationModel!.Attempts);
                parameters[1] = new MySqlParameter("@v2", authenticationModel!.Otp);
                parameters[2] = new MySqlParameter("@v3", authenticationModel!.OtpExpireTime);
                parameters[3] = new MySqlParameter("@v4", authenticationModel!.UserId);

                command.Parameters.AddRange(parameters);
                ExecuteQuery(command);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationModel"></param>
        /// <exception cref="NullReferenceException"></exception>
        public void LockUserAccountFor24Hours(AuthenticationModel authenticationModel)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (var command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = "UPDATE AUTHENTICATION SET accountStatus = '@v1'"
                                    + "WHERE userId = @v2;";
                var parameters = new MySqlParameter[2];
                parameters[0] = new MySqlParameter("@v1", false);
                parameters[1] = new MySqlParameter("@v2", authenticationModel.UserId);

                command.Parameters.AddRange(parameters);
                ExecuteQuery(command);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationModel"></param>
        /// <exception cref="NullReferenceException"></exception>
        public void RemoveUserFromAuthenticationTable(AuthenticationModel authenticationModel)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (var command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = "DELETE FROM AUTHENTICATION WHERE userId = @v1;";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", authenticationModel.UserId);

                command.Parameters.AddRange(parameters);
                ExecuteQuery(command);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationModel"></param>
        /// <exception cref="NullReferenceException"></exception>
        public void SetAuthenticationSessionTime(AuthenticationModel authenticationModel)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (var command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = "UPDATE AUTHENTICATION SET sessionEndTime = '@v1'"
                                    + "WHERE userId = @v2;";
                var parameters = new MySqlParameter[2];
                parameters[0] = new MySqlParameter("@v1", authenticationModel.SessionEndTime);
                parameters[1] = new MySqlParameter("@v2", authenticationModel.UserId);

                command.Parameters.AddRange(parameters);
                ExecuteQuery(command);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationModel"></param>
        /// <exception cref="NullReferenceException"></exception>
        public void SetAttemptsAndEndSessionTimeAuthentication(AuthenticationModel authenticationModel)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (var command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = "UPDATE AUTHENTICATION SET attempts = @v1,"
                                    + "sessionEndTime = '@v2' WHERE userId = @v3;";
                var parameters = new MySqlParameter[3];
                parameters[0] = new MySqlParameter("@v1", authenticationModel.Attempts);
                parameters[1] = new MySqlParameter("@v2", authenticationModel.SessionEndTime);
                parameters[2] = new MySqlParameter("@v3", authenticationModel.UserId);

                command.Parameters.AddRange(parameters);
                ExecuteQuery(command);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataStoreUser"></param>
        /// <returns>boolean value that updates their information and executes those values</returns>
        /// <exception cref="ArgumentNullException"></exception>
        private string RetrieveSaltFromDataStore(DataStoreUser dataStoreUser)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new ArgumentNullException(nameof(dataStoreUser));
            }
            using (var command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = $"SELECT SALT FROM USER WHERE USERNAME = @v1";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", dataStoreUser!._username);

                command.Parameters.AddRange(parameters);

                MySqlDataReader myReader = command.ExecuteReader();
                DataStoreUser returnUser = new DataStoreUser();
                while (myReader.Read())
                {
                    returnUser.UserId = myReader.GetInt32("userId");
                    returnUser._userType = myReader.GetString("typeName");
                    returnUser._username = myReader.GetString("username");
                    returnUser._password = myReader.GetString("password");
                    returnUser._email = myReader.GetString("email");
                    returnUser._salt = myReader.GetString("salt");
                }
                myReader.Close();
                mySqlConnection!.Close();
                return returnUser!._salt!;
            }
        }
    }
}
/*
public AuthenticationDataAccess() {}

public AuthenticationDataAccess(string query)
{
    this.query = query;
}

private void BuildTempUser()
{
    // Hides password
    Console.WriteLine("Please Enter Your MariaDB Username:");
    string username = Console.ReadLine();
    Console.WriteLine($"Please Enter the password for {username}:");
    StringBuilder input = new StringBuilder();
    while (true)
    {
        var key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.Enter) break;
        if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
        else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
    }
    string pass = input.ToString();
    // Console.WriteLine(pass);
    // Console.WriteLine(System.Environment.UserName);

    MySqlConnection tempMySqlConnection = new MySqlConnection($"server=localhost;user={username};password={pass}");
    // MySqlConnection tempMySqlConnection = new MySqlConnection($"server=localhost;user={user};password={pass}");
    try
    {
        tempMySqlConnection.Open();
        // MySqlCommand cmd1 = new MySqlCommand("DROP USER IF EXISTS 'tempuser'@'localhost';", tempMySqlConnection);
        MySqlCommand cmd2 = new MySqlCommand("CREATE USER IF NOT EXISTS 'tempuser'@'localhost' IDENTIFIED BY '123';", tempMySqlConnection);
        MySqlCommand cmd3 = new MySqlCommand("GRANT ALL PRIVILEGES ON *.* TO 'tempuser'@'localhost' WITH GRANT OPTION;", tempMySqlConnection);
        MySqlCommand cmd4 = new MySqlCommand("FLUSH PRIVILEGES;", tempMySqlConnection);
        // MySqlCommand cmd4 = new MySqlCommand("SHOW DATABASE LIKE logs_MM_test;", tempMySqlConnection);
        // MySqlCommand cmd5 = new MySqlCommand("CREATE DATABASE IF NOT EXISTS logs_MM_test;", tempMySqlConnection);

        Console.WriteLine("Connection Open...");
        // cmd1.ExecuteNonQuery();
        Console.WriteLine("DROP");
        cmd2.ExecuteNonQuery();
        Console.WriteLine("GRANT");
        cmd3.ExecuteNonQuery();
        Console.WriteLine("FLUSH");
        cmd4.ExecuteNonQuery();
        Console.WriteLine("CREATE");
        // cmd5.ExecuteNonQuery();
        Console.WriteLine("Temp User Created...");
        tempMySqlConnection.Close();
    }
    catch (Exception e)
    {
        Console.WriteLine("Exited Program with Exit " + e.Message);
    }
    EstablishMariaDBConnection();
}


public bool EstablishMariaDBConnection()
{
    // Dictionary<string, string> informationLog = new Dictionary<string, string>();

    // Console.WriteLine("Please Enter a Valid Database/Schema: ");
    // string databaseName = Console.ReadLine();

    // Console.WriteLine("Please Enter Database/Schema password: ");
    // StringBuilder input = new StringBuilder();
    // while (true)
    // {
    //     var key = Console.ReadKey(true);
    //     if (key.Key == ConsoleKey.Enter) break;
    //     if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
    //     else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
    // }
    // string databasePass = input.ToString();

    string databaseName = "MotoMotoDB";
    string databasePass = "naeun";
    // MySqlConnection mySqlConnection;
    // This is a hardcoded string, it will be different based on your naming
    // Need to generalize the database name or create a new database and run the restore sql file on it

    /** ROOT CONNECTION PASSWORD IS DIFFERENT FOR EVERYONE!!! PLEASE CHANGE*/
/*
string connectionString = $"server=localhost;user=root;database={databaseName};port=3306;password={databasePass};";
//connectionString 
try
{
    mySqlConnection = new MySqlConnection(connectionString);
    mySqlConnection.Open();

    Console.WriteLine("Connection open");

    // Console.WriteLine("Close");
    //mySqlConnection.Close();
    return true;
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    Console.WriteLine("ERROR - Creating new user...");
    BuildTempUser();
}

return false;
}
public Dictionary<string, string> SelectUser()
{
Dictionary<string, string> userAccount = new Dictionary<string, string>();
if (!EstablishMariaDBConnection()) Console.WriteLine("Connection failed to open...");
else Console.WriteLine("Connection opened...");

MySqlCommand command = new MySqlCommand(this.query, mySqlConnection);
MySqlDataReader reader = command.ExecuteReader();
while (reader.Read())
{  
    for (int i = 0; i < reader.FieldCount; i++)
    {
        userAccount[reader.GetName(i).ToString()] = reader[i].ToString();
    }
}
mySqlConnection.Close();
Console.WriteLine("Connection closed...");

return userAccount;
}
public bool UpdateAuthenticationTable()
{  
if (!EstablishMariaDBConnection()) Console.WriteLine("Connection failed to open...");
else Console.WriteLine("Connection opened...");

MySqlCommand command = new MySqlCommand(this.query, mySqlConnection);
if (command.ExecuteNonQuery() == 1)
{
    mySqlConnection.Close();
    Console.WriteLine("Connection closed...");
    return true;
}
mySqlConnection.Close();
Console.WriteLine("Connection closed...");
return false;
}
*/
