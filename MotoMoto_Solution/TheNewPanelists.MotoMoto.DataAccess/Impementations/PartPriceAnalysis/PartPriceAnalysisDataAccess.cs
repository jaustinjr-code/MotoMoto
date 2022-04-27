using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System.Configuration;
using TheNewPanelists.MotoMoto.Models;
using System.Data;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class PartPriceAnalysisDataAccess
    {
        MySqlConnection? mySqlConnection { get; set; }
        private string _connectionString = "";
        private enum _categories
        {
            alternator,
            brakePads,
            brakeRotor,
            cylinderHead,
            engineBlock,
            exhaustManifold,
            muffler,
            oilFilter,
            radiator,
            sparkPlug,
            timingBelt,
            timingChain,
            turbo,
            waterPump
        }
        public PartPriceAnalysisDataAccess()
        {
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;

            if (settings != null)
            {
                foreach (ConnectionStringSettings cs in settings)
                    _connectionString = cs.ConnectionString;
            } 
        }
        public PartPriceAnalysisDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
        /// <summary>
        /// Execute query function is soley used to execute non queries
        /// in the sense of sql database. We use this functionality of 
        /// booleans to determine if the query returns valid or not.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private bool ExecuteQuery(MySqlCommand command)
        {
            if (command.ExecuteNonQuery() == 1)
            {
                mySqlConnection!.Close();
                return true;
            }
            mySqlConnection!.Close();
            return false;
        }
        /// <summary>
        /// Establish mariadbconnection ensures the our connection string
        /// is valid. If it does not pass the establishment portion then
        /// our system is notified that connection cannot be passed
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
        /// RetrieveAllCategorialPartInformation will be used to allow users to see specified
        /// lists of items on their view. 
        /// </summary>
        /// <param name="listModel"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public PartListModel RetrieveAllCategorialPartInformationDataAccess(PartListModel listModel)
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

                command.CommandText = "SELECT * FROM VehicleParts v WHERE v.productName LIKE '%@v1%'";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", listModel.categorySelect);

                command.Parameters.AddRange(parameters);

                MySqlDataReader myReader = command.ExecuteReader();
                while (myReader.Read())
                {
                    PartModel partModel = new PartModel()
                    {
                        partID = myReader.GetInt32("productId")
                    };
                    RetrievePartInformation(partModel);
                    listModel.partList.Add(partModel);
                }
                myReader.Close();
                mySqlConnection!.Close();
                return listModel;
            }
        }
        /// <summary>
        /// Retrieve part information will be used to fetch part information from the 
        /// datastore. This function will primarily be called to display our list of items
        /// in the database for quick retrieval. If a user wants to see visually more in
        /// depth information about a product we will provide the Amazon URL for them.
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public PartModel RetrievePartInformation(PartModel part)
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

                command.CommandText = "SELECT * FROM VehicleParts v WHERE v.partId = @v1;";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", part.partID);

                command.Parameters.AddRange(parameters);

                MySqlDataReader myReader = command.ExecuteReader();
                PartPrice _partPrice = new PartPrice();
                while (myReader.Read())
                {
                    part.partID = myReader.GetInt32("productId");
                    part.partName = myReader.GetString("productName");
                    part.rating = myReader.GetString("rating");
                    part.ratingCount = myReader.GetInt32("ratingCount");
                    part.productURL = myReader.GetString("productURL");
                    part.currentPrice = myReader.GetDouble("productPrice");
                }
                myReader.Close();
                mySqlConnection!.Close();
                return part;
            }
        }
        /// <summary>
        /// RetrievSpecifiedPartPriceHistory is used to verify the part price history
        /// of specified items shown on our webpage. This information is soley used
        /// to fetch and respond to part price history that a product has gone through
        /// </summary>
        /// <param name="partModel"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public PartModel RetrieveSpecifiedPartPriceHistory(PartModel partModel)
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

                command.CommandText = "SELECT * FROM FormerPartPrices f WHERE f.productId = @v1";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", partModel.partID);
                command.Parameters.AddRange(parameters);

                MySqlDataReader myReader = command.ExecuteReader();
                PartPrice _partPrice = new PartPrice();
                while (myReader.Read())
                {
                    _partPrice.productId = myReader.GetInt32("productId");
                    _partPrice.productPrice = myReader.GetDouble("productPrice");
                    _partPrice.priceSetDate = myReader.GetDateTime("lastRecordedDate");
                    partModel.partPrices!.Add(_partPrice);
                }
                myReader.Close();
                mySqlConnection!.Close();
                return partModel;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partComparisonModel"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public PartComparisonModel RetrieveMultipleProductsToCompare(PartComparisonModel partComparisonModel)
        {
            foreach (PartModel part in partComparisonModel!.comparisonParts!)
            {
                RetrievePartInformation(part);
                RetrieveSpecifiedPartPriceHistory(part);
            }
            return partComparisonModel;
        }
    }
}
