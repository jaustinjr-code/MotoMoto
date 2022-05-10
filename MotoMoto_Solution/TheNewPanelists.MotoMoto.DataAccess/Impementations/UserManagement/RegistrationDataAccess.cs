using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models;
using System.Data;

namespace TheNewPanelists.MotoMoto.DataAccess.Registration
{
    public class RegistrationDataAccess : IDataAccess
    {
        ///<value>Property <c>_mySqlConnection</c> represents the connection that will be used to access the database.</value>
        private MySqlConnection? _mySqlConnection;
        public MySqlConnection getConnection () {return _mySqlConnection!;}

        ///<value>Property <c>_connectionString</c> represents the connection string that _mySqlConnection will use to open a connection</value>
        private string _connectionString = "server=localhost;user=root;database=dev_um;port=3306;password=12345;";

        ///<summary>try/catch to open a connection to the database using <c>_mySqlConnection</c> and <c>_connectionString</c></summary/
        ///<remarks>The exception is caught but not thrown. Error type and error message are passed to the console for debugging reference.</remarks>
        ///<returns>True if opening a connection executes successfully. False if an exception is caught.</returns>
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

        ///<summary>Queries the user table during registration to ensure the new user does not have an existing account.</summary>
        ///<param name="email">the new user's email address</param>
        ///<remarks>The exception is caught but not thrown. Error type and error message are passed to the console for debugging reference.</remarks>
        ///<returns>True if the email is found(i.e. the new user already has an account). 
        ///False if the email is NOT found or an exception is caught.</returns>
        public bool QueryUserTable(string email)
        {
            try
            {
                if (!EstablishMariaDBConnection())
                    return false;
                
                MySqlCommand command = _mySqlConnection!.CreateCommand();
                command.Connection = _mySqlConnection;
                command.Transaction = _mySqlConnection.BeginTransaction();                
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.CommandText = $"SELECT * FROM User U WHERE email = @v1;";
                command.Parameters.Add(new MySqlParameter("@v1", email));

                MySqlDataReader reader = command.ExecuteReader();
                return (reader.HasRows);                
            }
            catch (Exception e)
            {
                Console.WriteLine("ErrorType: " + e.GetType() + "\nErrorMessage: " + e.Message);
                return false;
            }
            finally
            {
                _mySqlConnection!.Close();
            }
        }

        ///<summary>Queries the registration table during registration to ensure the new user does not already
        ///have an active and pending registration.</summary>
        ///<param name="email">the new user's email address</param>
        ///<remarks>The exception is caught but not thrown. Error type and error message are passed to the console for debugging reference.</remarks>
        ///<returns>True if the email is found (i.e. the new user already has an active registration that has not been confirmed). 
        ///False if the email is NOT found or an exception is caught.</returns>
        public bool HasActiveRegistration(string email)
        {
            try
            {
                if (!EstablishMariaDBConnection())
                    return false;

                MySqlCommand command = _mySqlConnection!.CreateCommand();
                command.Connection = _mySqlConnection;
                command.Transaction = _mySqlConnection.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.CommandText = $"SELECT * FROM Registration WHERE email = @v1 AND validated = FALSE AND @v2 < expiration;";
                command.Parameters.AddRange(new MySqlParameter[2] {
                    new MySqlParameter("@v1", email),
                    new MySqlParameter("@v2", DateTime.Now)
                });
                
                MySqlDataReader reader = command.ExecuteReader();

                return (reader.HasRows);
            }
            catch (Exception e)
            {
                Console.WriteLine("ErrorType: " + e.GetType() + "\nErrorMessage: " + e.Message);
                return false;
            }
            finally
            {
                _mySqlConnection!.Close();
            }
        }

        ///<summary>Updates the pending registration for the new user to validated.</summary>
        ///<param name="registrationId">the registration ID for the specified registration in the database.</param>
        ///<remarks>The exception is caught but not thrown. Error type and error message are passed to the console for debugging reference.</remarks>
        ///<returns>True if the only 1 row is affected and the commit transaction executes successfully. 
        ///False if no rows are affected or an exception is caught.</returns>
        public bool UpdateRegistrationToValid(int registrationId, bool isTest = false)
        {
            try
            {
                if (!EstablishMariaDBConnection())
                    return false;

                MySqlTransaction sqlTrans;
                MySqlCommand command = _mySqlConnection!.CreateCommand();
                sqlTrans = _mySqlConnection.BeginTransaction();
                command.Connection = _mySqlConnection;
                command.Transaction = sqlTrans;
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.CommandText = $"UPDATE Registration R SET validated = TRUE WHERE R.registrationId = @v1";
                command.Parameters.Add(new MySqlParameter("@v1", registrationId));

                int response = command.ExecuteNonQuery();

                if(response == 1)
                {
                    if(!isTest)
                        sqlTrans.Commit();
                    return true;
                }
                
                return false;
            }
            catch(Exception e1)
            {
                Console.WriteLine("ErrorType: " + e1.GetType() + "\nErrorMessage: " + e1.Message);
                return false;
            }
            finally
            {
                _mySqlConnection!.Close();             
            }
        }

        ///<summary>Returns the registration ID associated with the provided email address that is active and has not been validated.</summary>
        ///<param name="email">the new user's email address</param>
        ///<remarks>The exception is caught but not thrown. Error type and error message are passed to the console for debugging reference.</remarks>
        ///<returns>An int representing the active an pending registration ID for the user.
        ///-1 if no entry is found . or an exception is caught.</returns>
        public int ReturnRegistrationId(string email)
        {
            int id = -1;
            try
            {
                if (!EstablishMariaDBConnection())
                    return id;

                MySqlCommand command = _mySqlConnection!.CreateCommand();
                command.Connection = _mySqlConnection;
                command.Transaction = _mySqlConnection.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.CommandText = $"SELECT registrationId FROM Registration WHERE email = @v1 AND @v3 < expiration;";
                command.Parameters.AddRange(new MySqlParameter[2] {
                    new MySqlParameter("@v1", email),
                    new MySqlParameter("@v3", DateTime.Now)
                });
                    
                MySqlDataReader myReader = command.ExecuteReader();

                if (myReader.Read())
                    id = myReader.GetInt32("registrationId");

                myReader.Close();
                return id;
            }
            catch (Exception e) 
            {
                Console.WriteLine("ErrorType: " + e.GetType() + "\nErrorMessage: " + e.Message);    
                return id;
            }     
            finally
            {
                _mySqlConnection!.Close();
            }
        }

        ///<summary>Queries the registration table for the given user data for an active registration that has not
        ///been validated. If reader returns true, assigns the password to the referenced paramater.</summary>
        ///<param name="confirmationRequest">the model for processing registration requests</param>
        ///<remarks>1)The exception is caught but not thrown. Error type and error message are passed to the console for 
        ///debugging reference. 2)The password is passed to the referenced parameter before returning true.</remarks>
        ///<returns>True if the entry is found and the reader execute successfully.
        ///False if the reader returns false or an exception is caught.</returns>
        public bool ConfirmRegistration(ref RegistrationRequestModel confirmationRequest)
        {
            MySqlTransaction sqlTrans;

            try
            {
                if (!EstablishMariaDBConnection())
                    return false;

                MySqlCommand command = _mySqlConnection!.CreateCommand();
                sqlTrans = _mySqlConnection.BeginTransaction();
                command.Connection = _mySqlConnection;
                command.Transaction = sqlTrans;
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.CommandText = $"SELECT password FROM Registration R WHERE R.registrationId = @v1 AND R.email = @v2 AND R.validated = FALSE AND @v3 < expiration;";
                command.Parameters.AddRange(new MySqlParameter[3] {
                    new MySqlParameter("@v1", confirmationRequest.RegistrationId!),
                    new MySqlParameter("@v2", confirmationRequest.Email!),
                    new MySqlParameter("@v3", DateTime.Now)
                });                

                MySqlDataReader myReader = command.ExecuteReader();
                confirmationRequest.Password = "";

                if (myReader.Read())
                {
                    confirmationRequest.Password = myReader.GetString("password");
                    myReader.Close();
                    return true;
                }
                return false;
            }
            catch (Exception e1)
            {
                Console.WriteLine("ErrorType: " + e1.GetType() + "\nErrorMessage: " + e1.Message);
                return false;
            }
            finally
            {
                _mySqlConnection!.Close();
            }
        }

        ///<summary>Inserts the new user's information into the user table.</summary>
        ///<param name="registrationRequest">the model for processing registration requests</param>
        ///<remarks>1)The exception is caught but not thrown. Error type and error message are passed to the console for 
        ///debugging reference.</remarks>
        ///<returns>True if only 1 row is affected and the commit executes successfully.
        ///False if the number of rows affected is not equal to 1 or an exception is caught.</returns>

        public bool InsertRegistrationEntry(ref RegistrationRequestModel registrationRequest, bool isTest = false)
        {
            MySqlTransaction sqlTrans;

            try
            {
                if (!EstablishMariaDBConnection())
                    return false;

                MySqlCommand command = _mySqlConnection!.CreateCommand();
                sqlTrans = _mySqlConnection.BeginTransaction();
                command.Connection = _mySqlConnection;
                command.Transaction = sqlTrans;
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.CommandText = $"INSERT INTO Registration (email, password, expiration) VALUES (@v1, @v2, @v3);";
                command.Parameters.AddRange(new MySqlParameter[3] {
                    new MySqlParameter("@v1", registrationRequest!.Email),
                    new MySqlParameter("@v2", registrationRequest!.Password),
                    new MySqlParameter("@v3", (DateTime.Now).AddDays(1))
                });

                int response = command.ExecuteNonQuery();

                if (response == 1)
                {
                    if(!isTest)
                        sqlTrans.Commit();
                    return true;
                }
                return false;
            }
            catch (Exception e1)
            {
                Console.WriteLine("ErrorType: " + e1.GetType() + "\nErrorMessage: " + e1.Message);
                return false;
            }
            finally
            {
                _mySqlConnection!.Close();
            }
        }

        ///<summary>Delete's the registration entry associated with the provided registration ID</summary>
        ///<param name="registrationId">an int representing the user's registration ID</param>
        ///<remarks>1)The exception is caught but not thrown. Error type and error message are passed to the console for 
        ///debugging reference.</remarks>
        ///<returns>True if only 1 row is affected and the commit executes successfully.
        ///False if the number of rows affected is not equal to 1 or an exception is caught.</returns>
        public bool DeleteActiveRegistration(int registrationId, bool isTest = false)
        {
            if (!EstablishMariaDBConnection())
                return false;

            MySqlCommand command = _mySqlConnection!.CreateCommand();
            MySqlTransaction sqlTrans;

            sqlTrans = _mySqlConnection.BeginTransaction();
            command.Connection = _mySqlConnection;
            command.Transaction = sqlTrans;

            try      
            {
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.CommandText = $"DELETE FROM Registration WHERE registrationId = @v1;";
                command.Parameters.Add(new MySqlParameter("@v1", registrationId));

                if(command.ExecuteNonQuery() == 1) 
                {
                    if (!isTest)
                        sqlTrans.Commit();
                    return true;
                }
                return false;      
            }
            catch (Exception e)
            {
                Console.WriteLine("ErrorType: " + e.GetType() + "\nErrorMessage: " + e.Message);
                return false;
            }
            finally
            {
                _mySqlConnection!.Close();
            }
        }  
    }
}