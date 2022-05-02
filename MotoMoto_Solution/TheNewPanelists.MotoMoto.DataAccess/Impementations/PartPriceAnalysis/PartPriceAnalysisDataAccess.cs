using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System.Configuration;
using TheNewPanelists.MotoMoto.Models;
using System.Data;
using System;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class PartPriceAnalysisDataAccess : IPartPriceAnalysisDataAccess
    {
        MySqlConnection? mySqlConnection { get; set; }
        private string _connectionString = "server=moto-moto.crd4iyvrocsl.us-west-1.rds.amazonaws.com;user=dev_moto;database=pro_moto;port=3306;password=motomoto;";

        public PartPriceAnalysisDataAccess(){}
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
                return listModel;
            }
            try
            {
                using (var command = new MySqlCommand())
                {
                    command.Transaction = mySqlConnection!.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.Connection = mySqlConnection!;
                    command.CommandType = CommandType.Text;

                    command.CommandText = "SELECT * FROM VehicleParts WHERE productName LIKE @v1";
                    var parameters = new MySqlParameter[1];
                    listModel.categorySelect = $"%{listModel.categorySelect}%";
                    parameters[0] = new MySqlParameter("@v1", listModel.categorySelect);

                    command.Parameters.AddRange(parameters);

                    IEnumerable<IPartEntity> _partList = new List<IPartEntity>();

                    using (MySqlDataReader myReader = command.ExecuteReader())
                    {
                        while (myReader.Read())
                        {
                            int partID = myReader.GetInt32("productId");
                            string partName = myReader.GetString("productName");
                            string rating = myReader.GetString("rating");
                            int ratingCount = myReader.GetInt32("ratingCount");
                            double productPrice = myReader.GetDouble("productPrice");
                            string productURL = myReader.GetString("productURL");

                            IPartEntity partEntity = new DataStoreVehicleParts(partID, partName, rating, ratingCount, productURL, productPrice);
                            partEntity.ShrinkPartName();
                            ((List<IPartEntity>)_partList).Add(partEntity);
                        }
                        myReader.Close();
                    };
                    listModel.partList = _partList;
                    return listModel;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("VehicleParts the ListID do not exist", nameof(listModel), ex);
            }
            finally
            {
                mySqlConnection!.Close();
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
                return part;
            }
            try
            {
                using (var command = new MySqlCommand())
                {
                    command.Transaction = mySqlConnection!.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.Connection = mySqlConnection!;
                    command.CommandType = CommandType.Text;

                    command.CommandText = "SELECT * FROM VehicleParts WHERE productId = @v1;";
                    var parameters = new MySqlParameter[1];
                    parameters[0] = new MySqlParameter("@v1", part.partID);

                    command.Parameters.AddRange(parameters);
                    PartModel _partModel = new PartModel();
                    using (MySqlDataReader myReader = command.ExecuteReader())
                    {
                        while (myReader.Read())
                        {
                            _partModel.partID = myReader.GetInt32("productId");
                            _partModel.partName = myReader.GetString("productName");
                            _partModel.rating = myReader.GetString("rating");
                            _partModel.ratingCount = myReader.GetInt32("ratingCount");
                            _partModel.productURL = myReader.GetString("productURL");
                            _partModel.currentPrice = myReader.GetDouble("productPrice");
                        }
                        myReader.Close();
                        return _partModel;
                    };
                }
            }
            catch(Exception ex)
            {
                throw new ArgumentException("VehicleParts the ID do not exist", nameof(part), ex);
            }
            finally
            {
                mySqlConnection!.Close();
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
                return partModel;
            }
            try
            {
                using (var command = new MySqlCommand())
                {
                    command.Transaction = mySqlConnection!.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.Connection = mySqlConnection!;
                    command.CommandType = CommandType.Text;

                    command.CommandText = "SELECT * FROM FormerPartPrices WHERE productId = @v1";
                    var parameters = new MySqlParameter[1];
                    parameters[0] = new MySqlParameter("@v1", partModel.partID);
                    command.Parameters.AddRange(parameters);

                    MySqlDataReader myReader = command.ExecuteReader();
                    IEnumerable<IPartPriceHistory> _partHistory = new List<IPartPriceHistory>();
                    IEnumerable<double> _partPrices = new List<double>();
                    IEnumerable<DateTime> _partDates = new List<DateTime>();
                    while (myReader.Read())
                    {
                        int productId = myReader.GetInt32("productId");
                        double productPrice = myReader.GetDouble("productPrice");
                        DateTime priceSetDate = myReader.GetDateTime("lastRecordedDate");

                        IPartPriceHistory partHistory = new DataStorePartHistory(productId, priceSetDate, productPrice);
                        ((List<double>)_partPrices).Add(productPrice);
                        ((List<DateTime>)_partDates).Add(priceSetDate);
                        ((List<IPartPriceHistory>)_partHistory).Add(partHistory);
                        partModel.historicalDate = _partDates;
                        partModel.histroicalListingPrice = _partPrices;
                        partModel.historicalPrices = _partHistory;
                    }
                    myReader.Close();
                    return partModel;
                }
            }
            catch(Exception ex)
            {
                throw new ArgumentException("VehiclePart the ID do is non-existent", nameof(partModel), ex);
            }
            finally
            {
                mySqlConnection!.Close();
            }
        }
        /// <summary>
        /// Update Part Price updates the current price of a product and 
        /// sends that data to the data store to keep forever. The former price
        /// will be logged onto the FormerPartPrices table where that data is retrieved
        /// for display
        /// </summary>
        /// <param name="partModel"></param>
        /// <returns></returns>
        public bool UpdatePartPrice(PartModel partModel)
        {
            AddPriceToPriceHistory(partModel);
            if (!EstablishMariaDBConnection())
            {
                return false;
            }
            try
            {
                using (var command = new MySqlCommand())
                {
                    command.Transaction = mySqlConnection!.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.Connection = mySqlConnection!;
                    command.CommandType = CommandType.Text;

                    command.CommandText = "UPDATE VehicleParts SET productPrice = @v1 WHERE productId = @v2";
                    var parameters = new MySqlParameter[2];
                    parameters[0] = new MySqlParameter("@v1", partModel.newPrice);
                    parameters[1] = new MySqlParameter("@v2", partModel.partID);

                    command.Parameters.AddRange(parameters);
                    return ExecuteQuery(command);
                }
            }
            catch(Exception ex)
            {
                throw new ArgumentException("PartUpdate error with model", nameof(partModel), ex);
            }
            finally
            {
                mySqlConnection?.Close();
            }
            
        }
        /// <summary>
        /// This function siply adds prices that are stored in the datastore and
        /// returns to be displayed on the graphs VIA the motomoto webpage. Simply, 
        /// the part history traces a part's price data from the past 6 months
        /// </summary>
        /// <param name="partModel"></param>
        /// <returns></returns>
        private bool AddPriceToPriceHistory(PartModel partModel)
        {
            DateTime currentDate = DateTime.Now;
            if (!EstablishMariaDBConnection())
            {
                return false;
            }
            try
            {
                using (var command = new MySqlCommand())
                {
                    command.Transaction = mySqlConnection!.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.Connection = mySqlConnection!;
                    command.CommandType = CommandType.Text;

                    command.CommandText = "INSTERT INTO FormerPartPrices (productId, lastRecordedDate, productPrice) VALUES (@v1, @v2, @v3);";
                    var parameters = new MySqlParameter[3];
                    parameters[0] = new MySqlParameter("@v1", partModel.partID);
                    parameters[1] = new MySqlParameter("@v2", currentDate);
                    parameters[2] = new MySqlParameter("@v3", partModel.currentPrice);

                    command.Parameters.AddRange(parameters);
                    return ExecuteQuery(command);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("PartModel insertion error with model", nameof(partModel), ex);
            }
            finally
            {
                mySqlConnection!.Close();
            }
        }
    }
}
