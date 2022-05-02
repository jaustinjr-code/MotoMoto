using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class UpvoteAnalyticDataAccess : IDataAccess
    {
        private MySqlConnection? _mySqlConnection;
        private string? _connectionString;

        /// <summary>
        /// Empty Default Constructor
        /// </summary>
        public UpvoteAnalyticDataAccess() { }

        public bool EstablishMariaDBConnection()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["MotoMotoDevDBConnection"];
            if (settings != null)
            {
                _connectionString = settings.ConnectionString;
            }
            //else
            //{
            //Console.WriteLine("False");
            //return false;
            //}

            // NOTE: this is not ideal and should be assigned using .config file attributes
            //       but the ConfigurationManager always returns null and I don't know why
            _connectionString = "server=localhost;user=dev_moto;database=dev_UM;port=3306;password=motomoto;";

            // Creates a SqlConnection and opens it
            try
            {
                // Close the connection if one is already opened
                if (_mySqlConnection != null && _mySqlConnection.State == System.Data.ConnectionState.Open)
                    _mySqlConnection.Close();

                _mySqlConnection = new MySqlConnection(_connectionString);
                _mySqlConnection.Open();

                return true;
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return false;
        }

        public int FetchCommentUpvoteTotal(IInteractionModel content)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }

            // Coalesce will help default any null records to zero
            // The post ID will be visible on the client side so using this info is okay
            string commandText = "SELECT COALESCE(SUM(isUpvote), 0) as total FROM UpvoteCommentAnalytics WHERE commentID = @commentID;";
            using (MySqlCommand command = new MySqlCommand(commandText, _mySqlConnection))
            {
                command.Parameters.AddWithValue("@commentID", (int)((UpvoteCommentModel)content).contentId);
                command.Transaction = _mySqlConnection!.BeginTransaction();
                try
                {
                    int result = 0;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        result = reader.GetInt32("total");
                        reader.Close();
                    }
                    command.Transaction.Commit();
                    _mySqlConnection.Close();
                    return result;
                }
                catch (Exception e)
                {
                    command.Transaction.Rollback();
                    throw new Exception("No upvote total: Error Message: " + e.Message);
                }
            }
        }

        public int FetchPostUpvoteTotal(IRequestModel content)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }

            // Coalesce will help default any null records to zero
            string commandText = "SELECT COALESCE(SUM(isUpvote), 0) as total FROM UpvotePostAnalytics WHERE postID = @postID;";
            using (MySqlCommand command = new MySqlCommand(commandText, _mySqlConnection))
            {
                command.Parameters.AddWithValue("@postID", (int)((FetchPostDetailsRequestModel)content).input);
                command.Transaction = _mySqlConnection!.BeginTransaction();
                try
                {
                    int result = 0;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        result = reader.GetInt32("total");
                        reader.Close();
                    }
                    command.Transaction.Commit();
                    _mySqlConnection.Close();
                    return result;
                }
                catch (Exception e)
                {
                    command.Transaction.Rollback();
                    throw new Exception("No upvote total: Error Message: " + e.Message);
                }
            }
        }
    }
}