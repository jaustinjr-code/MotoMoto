using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models;
using System.Data;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class RegistrationDataAccess : IDataAccess
    {
        private MySqlConnection? _mySqlConnection { get; set; }

        private string _connectionString = "server=localhost;user=root;database=dev_um;port=3306;password=12345;";

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
                throw e;
            }   
        }

        public bool QueryUserTable(string email)
        {
            try
            {
                if (!EstablishMariaDBConnection())
                    throw new NullReferenceException();
                
                MySqlCommand command = _mySqlConnection!.CreateCommand();
                command.Connection = _mySqlConnection;
                command.Transaction = _mySqlConnection.BeginTransaction();                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.CommandText = $"SELECT * FROM USER U WHERE U.email = @v1;";
                command.Parameters.Add(new MySqlParameter("@v1", email));

                int response = command.ExecuteNonQuery();
                return response == 1;                
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

        public bool HasActiveRegistration(string email)
        {
            try
            {
                if (!EstablishMariaDBConnection())
                    throw new NullReferenceException();

                MySqlCommand command = _mySqlConnection!.CreateCommand();
                command.Connection = _mySqlConnection;
                command.Transaction = _mySqlConnection.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.CommandText = $"SELECT * FROM REGISTRATION R WHERE R.email = @v1 AND R.validated = FALSE AND @v2 < expiration;";
                command.Parameters.AddRange(new MySqlParameter[2] {
                    new MySqlParameter("@v1", email),
                    new MySqlParameter("@v2", DateTime.Now)
                });

                int response = command.ExecuteNonQuery();
                return response == 1;     
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

        public bool UpdateRegistrationToValid(string email)
        {
            try
            {
                if (!EstablishMariaDBConnection())
                    throw new NullReferenceException();

                MySqlTransaction sqlTrans;
                MySqlCommand command = _mySqlConnection!.CreateCommand();
                sqlTrans = _mySqlConnection.BeginTransaction();
                command.Connection = _mySqlConnection;
                command.Transaction = sqlTrans;
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.CommandText = $"UPDATE REGISTRATION R SET validated = TRUE WHERE R.registrationId = @v1";

                int response = command.ExecuteNonQuery();

                if(response == 1)
                {
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

        public int ReturnRegistrationId(string email)
        {
            int id = -1;
            try
            {
                if (!EstablishMariaDBConnection())
                    throw new NullReferenceException();

                MySqlCommand command = _mySqlConnection!.CreateCommand();
                command.Connection = _mySqlConnection;
                command.Transaction = _mySqlConnection.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.CommandText = $"SELECT registrationId FROM REGISTRATION R WHERE R.email = @v1 AND @v3 < expiration;";
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

        public bool ConfirmRegistration(ref RegistrationRequestModel confirmationRequest)
        {
            MySqlTransaction sqlTrans;

            try
            {
                if (!EstablishMariaDBConnection())
                    throw new NullReferenceException();

                MySqlCommand command = _mySqlConnection!.CreateCommand();
                sqlTrans = _mySqlConnection.BeginTransaction();
                command.Connection = _mySqlConnection;
                command.Transaction = sqlTrans;
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.CommandText = $"SELECT password FROM REGISTRATION R WHERE R.registrationId = @v1 AND R.email = @v2 AND R.validated = FALSE AND @v3 < expiration;";
                command.Parameters.AddRange(new MySqlParameter[3] {
                    new MySqlParameter("@v1", confirmationRequest.RegistrationId!),
                    new MySqlParameter("@v2", confirmationRequest.Email!),
                    new MySqlParameter("@v3", DateTime.Now)
                });                

                MySqlDataReader myReader = command.ExecuteReader();

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

        public bool InsertRegistrationEntry(ref RegistrationRequestModel registrationRequest)
        {
            MySqlTransaction sqlTrans;

            try
            {
                if (!EstablishMariaDBConnection())
                    throw new NullReferenceException();

                MySqlCommand command = _mySqlConnection!.CreateCommand();
                sqlTrans = _mySqlConnection.BeginTransaction();
                command.Connection = _mySqlConnection;
                command.Transaction = sqlTrans;
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.CommandText = $"INSERT INTO REGISTRATION (email, password, expiration) VALUES (@v1, @v2, @v3);";
                command.Parameters.AddRange(new MySqlParameter[3] {
                    new MySqlParameter("@v1", registrationRequest!.Email),
                    new MySqlParameter("@v2", registrationRequest!.Password),
                    new MySqlParameter("@v3", (DateTime.Now).AddDays(1))
                });

                int response = command.ExecuteNonQuery();

                if (response == 1)
                {
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

        public bool DeleteActiveRegistration(string email)
        {
            if (!EstablishMariaDBConnection())
                throw new NullReferenceException();

            MySqlCommand command = _mySqlConnection!.CreateCommand();
            MySqlTransaction sqlTrans;

            sqlTrans = _mySqlConnection.BeginTransaction();
            command.Connection = _mySqlConnection;
            command.Transaction = sqlTrans;

            try      
            {
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.CommandText = $"DELETE FROM REGISTRATION WHERE email = @v1 AND @v2 < expiration;";
                command.Parameters.AddRange(new MySqlParameter[2] {
                    new MySqlParameter("@v1", email),
                    new MySqlParameter("@v2", DateTime.Now)
                });

                int response = command.ExecuteNonQuery();
                sqlTrans.Commit();
                return response == 1;         
            }
            catch (Exception e)
            {
                Console.WriteLine("ErrorType: " + e.GetType() + "\nErrorMessage: " + e.Message);
                try
                {
                    sqlTrans.Rollback();
                }
                catch(MySqlException ex)
                {
                    Console.WriteLine("ErrorType: " + ex.GetType() + "\nErrorMessage: " + ex.Message);
                    throw ex;
                }
                throw e;
            }
            finally
            {
                _mySqlConnection!.Close();
            }
        }

        // public int ReturnRegistrationId(string email)
        // {
        //     if (!EstablishMariaDBConnection())
        //         throw new NullReferenceException();

        //     MySqlCommand command = _mySqlConnection!.CreateCommand();
        //     MySqlTransaction sqlTrans;

        //     sqlTrans = _mySqlConnection.BeginTransaction();
        //     command.Connection = _mySqlConnection;
        //     command.Transaction = sqlTrans;

        //     try      
        //     {
        //         command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
        //         command.CommandText = $"SELECT registrationId FROM REGISTRATION R WHERE R.email = @v1 AND @v2 < expiration;";
        //         command.Parameters.AddRange(new MySqlParameter[2] {
        //             new MySqlParameter("@v1", email),
        //             new MySqlParameter("@v2", DateTime.Now)
        //         });

        //         MySqlDataReader myReader = command.ExecuteReader();
        //         int regId = -1;

        //         if (myReader.Read())
        //             regId = myReader.GetInt32("registrationId");
                
        //         return regId;
        //     }
        //     catch(Exception e)
        //     {
        //         Console.WriteLine("ErrorType: " + e.GetType() + "\nErrorMessage: " + e.Message);
        //         throw e;
        //     }
        //     finally
        //     {
        //         _mySqlConnection.Close();
        //     }
        // }

        // public DataStoreConfirmedAccount ReturnConfirmedAccount(ref RegistrationRequestModel confirmationRequest)
        // {
        //     if (!EstablishMariaDBConnection())
        //         throw new NullReferenceException();

        //     MySqlTransaction sqlTrans;
        //     MySqlCommand command = _mySqlConnection!.CreateCommand();

        //     sqlTrans = _mySqlConnection.BeginTransaction();
        //     command.Connection = _mySqlConnection;
        //     command.Transaction = sqlTrans;

        //     try      
        //     {
        //         command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
        //         command.CommandText = $"SELECT registrationId FROM REGISTRATION R WHERE R.registrationId = @v1 AND R.email = @v2 AND @v3 < expiration AND validated = FALSE;";
        //         command.Parameters.AddRange( new MySqlParameter[3] {
        //             new MySqlParameter("@v1", confirmationRequest.RegistrationId),
        //             new MySqlParameter("@v2", confirmationRequest.Email),
        //             new MySqlParameter("@v3", DateTime.Now)
        //         });                

        //         MySqlDataReader myReader = command.ExecuteReader();
        //         DataStoreConfirmedAccount confirmedAccount = new DataStoreConfirmedAccount();

        //         if (myReader.Read())
        //             confirmedAccount.RegistrationId = myReader.GetInt32("registrationId");

        //         myReader.Close();
        //         return confirmedAccount;
        //     }
        //     catch(Exception e)
        //     {
        //         Console.WriteLine("ErrorType: " + e.GetType() + "\nErrorMessage: " + e.Message);
        //         throw e;
        //     }
        //     finally
        //     {
        //         _mySqlConnection.Close();
        //     }
        // }
    }
}