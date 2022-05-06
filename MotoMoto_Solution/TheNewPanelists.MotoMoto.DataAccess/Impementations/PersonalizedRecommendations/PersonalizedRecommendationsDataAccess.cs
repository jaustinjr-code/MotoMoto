using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.DataStoreEntities.PersonalizedRecommendations;

namespace TheNewPanelists.MotoMoto.DataAccess.Impementations.PersonalizedRecommendations
{
    public class PersonalizedRecommendationsDataAccess
    {
        private MySqlConnection? _mySqlConnection { get; set; }
        private string _connectionString = "server=moto-moto.crd4iyvrocsl.us-west-1.rds.amazonaws.comp;user=dev_moto;database=pro_moto;port=3306;password=motomoto;";
        public bool EstablishMariaDBConnection()
        {
            try
            {
                _mySqlConnection = new MySqlConnection(_connectionString);
                _mySqlConnection.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public void ReturnCountriesFollowed(int userId, ref DataStoreRequestPreferences dataStoreRequest)
        {
            try
            {
                if (!EstablishMariaDBConnection())
                    dataStoreRequest.messages!.Add("Error: Could not establish database connection.");
                else
                {
                    MySqlCommand command = _mySqlConnection!.CreateCommand();
                    command.Connection = _mySqlConnection;
                    command.Transaction = _mySqlConnection.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.CommandText = $"SELECT (country) FROM FOLLOWEDCOUNTRY FC WHERE FC.userId = @v1";
                    command.Parameters.Add(new MySqlParameter("@v1", userId));

                    MySqlDataReader myReader = command.ExecuteReader();
                    while (myReader.Read())
                    {
                        dataStoreRequest.followedCountries!.Add(new Country { country = myReader.GetString("country") });
                    }
                }
            }
            catch (Exception e)
            {
                dataStoreRequest.messages!.Add("ErrorType: " + e.GetType() + "\nErrorMessage: " + e.Message);
            }
            finally
            {
                _mySqlConnection!.Close();
            }
        }

        public void ReturnMakesFollowed(int userId, ref DataStoreRequestPreferences dataStoreRequest)
        {
            try
            {
                if (!EstablishMariaDBConnection())
                    dataStoreRequest.messages!.Add("Error: Could not establish database connection.");
                else
                {
                    MySqlCommand command = _mySqlConnection!.CreateCommand();
                    command.Connection = _mySqlConnection;
                    command.Transaction = _mySqlConnection.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.CommandText = $"SELECT (Make) FROM FOLLOWEDMAKE FM WHERE FM.userId = @v1";
                    command.Parameters.Add(new MySqlParameter("@v1", userId));

                    MySqlDataReader myReader = command.ExecuteReader();
                    while (myReader.Read())
                    {
                        dataStoreRequest.followedMakes!.Add(new Make { make = myReader.GetString("make") });
                    }
                }
            }
            catch (Exception e)
            {
                dataStoreRequest.messages!.Add("ErrorType: " + e.GetType() + "\nErrorMessage: " + e.Message);
            }
            finally
            {
                _mySqlConnection!.Close();

            }
        }

        public void ReturnModelsFollowed(int userId, ref DataStoreRequestPreferences dataStoreRequest)
        {
            try
            {
                if (!EstablishMariaDBConnection())
                    dataStoreRequest.messages!.Add("Error: Could not establish database connection.");
                else
                {
                    MySqlCommand command = _mySqlConnection!.CreateCommand();
                    command.Connection = _mySqlConnection;
                    command.Transaction = _mySqlConnection.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.CommandText = $"SELECT (model) FROM FOLLOWEDMODEL FM WHERE FM.userId = @v1";
                    command.Parameters.Add(new MySqlParameter("@v1", userId));

                    MySqlDataReader myReader = command.ExecuteReader();
                    while (myReader.Read())
                    {
                        dataStoreRequest.followedModels!.Add(new Model { model = myReader.GetString("model") });
                    }
                }
            }
            catch (Exception e)
            {
                dataStoreRequest.messages!.Add("ErrorType: " + e.GetType() + "\nErrorMessage: " + e.Message);
            }
            finally
            {
                _mySqlConnection!.Close();

            }
        }

        public bool AddCountryPreferences(int userId, ref List<Country> countries, ref List<string> messages)
        {
            MySqlTransaction sqlTrans;
            try
            {
                if (!EstablishMariaDBConnection())
                {
                    messages!.Add("Error: AddCountryPreferences() - Could not establish database connection.");
                    return false;
                }
                else
                {
                    MySqlCommand command = _mySqlConnection!.CreateCommand();
                    sqlTrans = _mySqlConnection.BeginTransaction();
                    command.Connection = _mySqlConnection;
                    command.Transaction = sqlTrans;
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.Parameters.Add(new MySqlParameter("@v1", userId));
                    command.CommandText = $"INSERT INTO FOLLOWEDCOUNTRY VALUES ";

                    for (int i = 0; i < countries.Count; i++)
                    {
                        command.CommandText += $"(@v1, {countries[i]})";
                        if (i < countries.Count - 1)
                            command.CommandText += ", ";
                        else
                            command.CommandText += ";";
                    }

                    try
                    {
                        command.ExecuteNonQuery();
                        messages.Add("Success: AddCountryPreferences() - Executed Query - no exceptions.");
                        sqlTrans.Commit();
                        return true;
                    }
                    catch (Exception e2)
                    {
                        messages!.Add("ErrorType: " + e2.GetType() + "\nErrorMessage: " + e2.Message);
                        sqlTrans.Rollback();
                        return false;
                    }
                }
            }
            catch (Exception e1)
            {
                messages!.Add("ErrorType: " + e1.GetType() + "\nErrorMessage: " + e1.Message);
                return false;
            }
            finally
            {
                _mySqlConnection!.Close();
            }
        }

        public bool AddMakePreferences(int userId, ref List<Make> makes, ref List<string> messages)
        {
            MySqlTransaction sqlTrans;
            try
            {
                if (!EstablishMariaDBConnection())
                {
                    messages!.Add("Error: AddMakePreferences() - Could not establish database connection.");
                    return false;
                }
                else
                {
                    MySqlCommand command = _mySqlConnection!.CreateCommand();
                    sqlTrans = _mySqlConnection.BeginTransaction();
                    command.Connection = _mySqlConnection;
                    command.Transaction = sqlTrans;
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.Parameters.Add(new MySqlParameter("@v1", userId));
                    command.CommandText = $"INSERT INTO FOLLOWEDMAKE VALUES ";

                    for (int i = 0; i < makes.Count; i++)
                    {
                        command.CommandText += $"(@v1, {makes[i]})";
                        if (i < makes.Count - 1)
                            command.CommandText += ", ";
                        else
                            command.CommandText += ";";
                    }

                    try
                    {
                        command.ExecuteNonQuery();
                        messages.Add("Success: AddMakePreferences() - Executed Query - no exceptions.");
                        sqlTrans.Commit();
                        return true;
                    }
                    catch (Exception e2)
                    {
                        messages!.Add("ErrorType: " + e2.GetType() + "\nErrorMessage: " + e2.Message);
                        sqlTrans.Rollback();
                        return false;
                    }
                }
            }
            catch (Exception e1)
            {
                messages!.Add("ErrorType: " + e1.GetType() + "\nErrorMessage: " + e1.Message);
                return false;
            }
            finally
            {
                _mySqlConnection!.Close();
            }
        }
        public bool AddModelPreferences(int userId, ref List<Model> models, ref List<string> messages)
        {
            MySqlTransaction sqlTrans;
            try
            {
                if (!EstablishMariaDBConnection())
                {
                    messages!.Add("Error: AddModelPreferences() - Could not establish database connection.");
                    return false;
                }
                else
                {
                    MySqlCommand command = _mySqlConnection!.CreateCommand();
                    sqlTrans = _mySqlConnection.BeginTransaction();
                    command.Connection = _mySqlConnection;
                    command.Transaction = sqlTrans;
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.Parameters.Add(new MySqlParameter("@v1", userId));
                    command.CommandText = $"INSERT INTO FOLLOWEDMODEL VALUES ";

                    for (int i = 0; i < models.Count; i++)
                    {
                        command.CommandText += $"(@v1, {models[i].make}, {models[i].model})";
                        if (i < models.Count - 1)
                            command.CommandText += ", ";
                        else
                            command.CommandText += ";";
                    }

                    try
                    {
                        command.ExecuteNonQuery();
                        messages.Add("Success: AddModelPreferences() - Executed Query - no exceptions.");
                        sqlTrans.Commit();
                        return true;
                    }
                    catch (Exception e2)
                    {
                        messages!.Add("ErrorType: " + e2.GetType() + "\nErrorMessage: " + e2.Message);
                        sqlTrans.Rollback();
                        return false;
                    }
                }
            }
            catch (Exception e1)
            {
                messages!.Add("ErrorType: " + e1.GetType() + "\nErrorMessage: " + e1.Message);
                return false;
            }
            finally
            {
                _mySqlConnection!.Close();
            }
        }
        public bool RemoveCountryPreferences(int userId, ref List<string> messages)
        {
            MySqlTransaction sqlTrans;
            try
            {
                if (!EstablishMariaDBConnection())
                {
                    messages.Add("Error: RemovedCountryPreferences() - Failed to connected to the database");
                    return false;
                }
                else
                {
                    MySqlCommand command = _mySqlConnection!.CreateCommand();
                    sqlTrans = _mySqlConnection.BeginTransaction();
                    command.Connection = _mySqlConnection;
                    command.Transaction = sqlTrans;
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.CommandText = $"DELETE FROM FOLLOWEDCOUNTRY FC WHERE FC.userId = @v1";
                    command.Parameters.Add(new MySqlParameter("@v1", userId));

                    try
                    {
                        command.ExecuteNonQuery();
                        messages.Add("Success: RemovedCountryPreferences() - Executed Query - no exceptions.");
                        sqlTrans.Commit();
                        return true;
                    }
                    catch (Exception e2)
                    {
                        messages.Add("ErrorType: " + e2.GetType() + "\nErrorMessage: " + e2.Message);
                        sqlTrans.Rollback();
                        return false;
                    }
                }
            }
            catch (Exception e1)
            {
                messages!.Add("ErrorType: " + e1.GetType() + "\nErrorMessage: " + e1.Message);
                return false;
            }
            finally
            {
                _mySqlConnection!.Close();

            }
        }

        public bool RemoveMakePreferences(int userId, ref List<string> messages)
        {
            MySqlTransaction sqlTrans;
            try
            {
                if (!EstablishMariaDBConnection())
                {
                    messages.Add("Error: RemovedMakePreferences() - Failed to connected to the database");
                    return false;
                }
                else
                {
                    MySqlCommand command = _mySqlConnection!.CreateCommand();
                    sqlTrans = _mySqlConnection.BeginTransaction();
                    command.Connection = _mySqlConnection;
                    command.Transaction = sqlTrans;
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.CommandText = $"DELETE FROM FOLLOWEDMAKES FM WHERE FM.userId = @v1";
                    command.Parameters.Add(new MySqlParameter("@v1", userId));

                    try
                    {
                        command.ExecuteNonQuery();
                        messages.Add("Success: RemovedMakePreferences() - Executed Query - no exceptions.");
                        sqlTrans.Commit();
                        return true;
                    }
                    catch (Exception e2)
                    {
                        messages.Add("ErrorType: " + e2.GetType() + "\nErrorMessage: " + e2.Message);
                        sqlTrans.Rollback();
                        return false;
                    }
                }
            }
            catch (Exception e1)
            {
                messages!.Add("ErrorType: " + e1.GetType() + "\nErrorMessage: " + e1.Message);
                return false;
            }
            finally
            {
                _mySqlConnection!.Close();

            }
        }
        public bool RemoveModelPreferences(int userId, ref List<string> messages)
        {
            MySqlTransaction sqlTrans;
            try
            {
                if (!EstablishMariaDBConnection())
                {
                    messages.Add("Error: RemovedModelPreferences() - Failed to connected to the database");
                    return false;
                }
                else
                {
                    MySqlCommand command = _mySqlConnection!.CreateCommand();
                    sqlTrans = _mySqlConnection.BeginTransaction();
                    command.Connection = _mySqlConnection;
                    command.Transaction = sqlTrans;
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.CommandText = $"DELETE FROM FOLLOWEDMODELS FM WHERE FM.userId = @v1";
                    command.Parameters.Add(new MySqlParameter("@v1", userId));

                    try
                    {
                        command.ExecuteNonQuery();
                        messages.Add("Success: RemovedModelPreferences() - Executed Query - no exceptions.");
                        sqlTrans.Commit();
                        return true;
                    }
                    catch (Exception e2)
                    {
                        messages.Add("ErrorType: " + e2.GetType() + "\nErrorMessage: " + e2.Message);
                        sqlTrans.Rollback();
                        return false;
                    }
                }
            }
            catch (Exception e1)
            {
                messages!.Add("ErrorType: " + e1.GetType() + "\nErrorMessage: " + e1.Message);
                return false;
            }
            finally
            {
                _mySqlConnection!.Close();

            }
        }
    }
}
