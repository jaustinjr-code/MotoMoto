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
                command.CommandText = $"SELECT U FROM USER WHERE U.EMAIL = @v1;";
                var parameter = new SqlParameter("@v1", email);

                command.Parameters.Add(parameter);
                return (ExecuteQuery(command));
            }
        }

        //public bool SingleQueryRegistrationTable(string email)
        //{
        //    if (!EstablishMariaDBConnection())
        //    {
        //        throw new NullReferenceException();
        //    }
        //    using (MySqlCommand command = new MySqlCommand())
        //    {
        //        command.CommandText = $"SELECT R FROM REGISTRATION WHERE R.EMAIL = @v1 AND R.VALIDATED = FALSE;";
        //        var parameter = new SqlParameter("@v1", email);

        //        command.Parameters.Add(parameter);
        //        return (ExecuteQuery(command));
        //    }
        //}

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

        public bool DeleteActiveRegistrationEntry(string email)
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

        public RegistrationEntity ReturnActiveRegistrationEntry(string email)
        {
            if (!EstablishMariaDBConnection())
                throw new NullReferenceException();

            using (var command = new MySqlCommand())
            {
                command.CommandText = $"SELECT * FROM REGISTRATION R WHERE R.EMAIL = @v1 AND DATE.NOW() < EXPIRATION;";
                var parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@v1", email);

                command.Parameters.AddRange(parameters);
                command.Transaction = _mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;

                MySqlDataReader myReader = command.ExecuteReader();
                RegistrationEntity registrationEntity = new RegistrationEntity();

                while (myReader.Read())
                {
                    registrationEntity.RegistrationID = myReader.GetInt32("registrationId");
                    registrationEntity.Email = myReader.GetString("email");
                    registrationEntity.Password = myReader.GetString("password");
                    registrationEntity.Expiration = myReader.GetDateTime("expiration");
                    registrationEntity.Validated = myReader.GetBoolean("validated");
                }
                myReader.Close();
                _mySqlConnection!.Close();

                return registrationEntity;
            }
        }
    }
}