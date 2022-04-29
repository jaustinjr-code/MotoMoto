using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models;
using System.Data.SqlClient;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class RegistrationDataAccess : IDataAccess
    {
        private MySqlConnection? _mySqlConnection { get; set; }

        private string _connectionString = "server=localhost;user=dev_moto;database=dev_UM;port=3306;password=motomoto;";
        
        public RegistrationDataAccess() { } 
        
        public RegistrationDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        private bool ExecuteQuery(MySqlCommand command)
        {
            if (command.ExecuteNonQuery() == 1)
            {
                _mySqlConnection!.Close();
                return true;
            }
            _mySqlConnection!.Close();
            return false;
        }

        public bool EstablishMariaDBConnection()
        {
            try
            {
                _mySqlConnection = new MySqlConnection(_connectionString);
                _mySqlConnection.Open();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool QueryUserTable(string email)
        {
            if (!EstablishMariaDBConnection())
                throw new NullReferenceException();

            using (MySqlCommand command = new MySqlCommand())
            {
                command.CommandText = $"SELECT U FROM User WHERE U.EMAIL = @v1;";
                var parameter = new SqlParameter("@v1", email);
                command.Parameters.Add(parameter);

                return (ExecuteQuery(command));
            }
        }

        public bool HasActiveRegistration(string email)
        {
            if (!EstablishMariaDBConnection())
                throw new NullReferenceException();

            using (MySqlCommand command = new MySqlCommand())
            {
                command.CommandText = $"SELECT R FROM REGISTRATION WHERE R.EMAIL = @v1 AND R.VALIDATED = FALSE AND DATE.NOW() < EXPIRATION;";
                var parameter = new SqlParameter("@v1", email);
                command.Parameters.Add(parameter);

                return (ExecuteQuery(command));
            }
        }

        public bool ConfirmRegistration(EmailConfirmationRequestModel confirmationRequest)
        {
            if (!EstablishMariaDBConnection())
                throw new NullReferenceException();

            using (MySqlCommand command = new MySqlCommand())
            {
                command.CommandText = $"SELECT R FROM REGISTRATION WHERE R.REGISTRATIONID = @v1 AND R.EMAIL = @v2 AND R.VALIDATED = FALSE AND DATE.NOW() < EXPIRATION;";

                var parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@v1", confirmationRequest.RegistrationID!);
                parameters[1] = new SqlParameter("@v2", confirmationRequest.Email!);
                command.Parameters.AddRange(parameters);

                return (ExecuteQuery(command));
            }
        }

        public bool InsertRegistrationEntry(RegistrationRequestModel registrationRequest)
        {
            if (!EstablishMariaDBConnection())
                throw new NullReferenceException();

            using (MySqlCommand command = new MySqlCommand())
            {
                command.CommandText = $"INSERT INTO REGISTRATION (email, password, expiration)" +
                                      $"VALUES (@v1, @v2, DATE_ADD(NOW(), INTERVAL 24 HOUR));";
                
                var parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@v1", registrationRequest!.Email);
                parameters[1] = new SqlParameter("@v2", registrationRequest!.Password);
                command.Parameters.AddRange(parameters);

                return (ExecuteQuery(command));
            }
        }

        public bool DeleteActiveRegistration(string email)
        {
            if (!EstablishMariaDBConnection())
                throw new NullReferenceException();

            using (MySqlCommand command = new MySqlCommand())
            {
                command.CommandText = $"DELETE FROM REGISTRATION R WHERE R.EMAIL = @v1 AND DATE.NOW() < EXPIRATION;";
                var parameter = new SqlParameter("@v1", email);
                command.Parameters.Add(parameter);

                return (ExecuteQuery(command));
            }
        }

        public int ReturnRegistrationId(string email)
        {
            if (!EstablishMariaDBConnection())
                throw new NullReferenceException();

            using (var command = new MySqlCommand())
            {
                command.CommandText = $"SELECT REGISTRATIONID FROM REGISTRATION R WHERE R.EMAIL = @v1 AND DATE.NOW() < EXPIRATION;";
                var parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@v1", email);
                command.Parameters.AddRange(parameters);

                command.Transaction = _mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;

                MySqlDataReader myReader = command.ExecuteReader();
                int regId = -1;

                if (myReader.Read())
                    regId = myReader.GetInt32("registrationId");
                
                myReader.Close();
                _mySqlConnection!.Close();

                return regId;
            }
        }

        public DataStoreConfirmedAccount ReturnConfirmedAccount(EmailConfirmationRequestModel confirmationRequest)
        {
            if (!EstablishMariaDBConnection())
                throw new NullReferenceException();

            using (var command = new MySqlCommand())
            {
                command.CommandText = $"SELECT REGISTRATIONID, EMAIL, PASSWORD FROM REGISTRATION R WHERE R.REGISTRATIONID = @v1 AND R.EMAIL = @v2 AND DATE.NOW() < EXPIRATION AND VALIDATED = FALSE;";
                var parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@v1", confirmationRequest.RegistrationID);
                parameters[0] = new SqlParameter("@v2", confirmationRequest.Email);
                command.Parameters.AddRange(parameters);

                command.Transaction = _mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;

                MySqlDataReader myReader = command.ExecuteReader();
                DataStoreConfirmedAccount confirmedAccount = new DataStoreConfirmedAccount();

                while (myReader.Read())
                {
                    confirmedAccount.RegistrationId = myReader.GetInt32("registraionId");
                    confirmedAccount.Email = myReader.GetString("email");
                    confirmedAccount.Password = myReader.GetString("password");
                }
                myReader.Close();
                _mySqlConnection!.Close();

                return confirmedAccount;
            }
        }
    }
}