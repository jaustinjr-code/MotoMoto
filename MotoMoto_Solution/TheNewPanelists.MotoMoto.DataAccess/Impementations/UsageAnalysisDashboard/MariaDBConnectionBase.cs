using MySql.Data.MySqlClient;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public abstract class MariaDBConnectionBase
    {
        public static MySqlConnection EstablishConnection(string connectionString)
        {
            // Does not work
            //ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["MotoMotoDevDBConnection"];
            //if (settings != null)
            //{
            //_connectionString = settings.ConnectionString;
            //}
            //else
            //{
            //Console.WriteLine("False");
            //return false;
            //}

            // NOTE: this is not ideal and should be assigned using .config file attributes
            //       but the ConfigurationManager always returns null and I don't know why

            // Creates a SqlConnection and opens it
            try
            {
                // Close the connection if one is already opened
                // if (_mySqlConnection != null && _mySqlConnection.State == System.Data.ConnectionState.Open)
                //     _mySqlConnection.Close();

                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
                // mySqlConnection.Open();
                return mySqlConnection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new NullReferenceException();
            }
        }
    }
}