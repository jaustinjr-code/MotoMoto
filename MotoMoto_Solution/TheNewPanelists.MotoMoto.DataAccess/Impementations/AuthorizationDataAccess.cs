using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.Models;
using System.Diagnostics;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    /// <summary>
    /// Contains functionality that retrieves or modifies infomation stored in external databases
    /// pertaining to part flags.
    /// </summary>
    public class AuthorizationDataAccess
    {
        /// <summary>
        /// String used for connecting the MotoMoto application to its external database
        /// </summary>
        private readonly string _connectionString = "server=moto-moto.crd4iyvrocsl.us-west-1.rds.amazonaws.com;user=dev_moto;database=pro_moto;port=3306;password=motomoto;";

        /// <summary>
        /// Retrieves the count of number of the number of times a particular flag has been cited.
        /// If the flag does not exist in the database, the number of times cited is zero.
        /// </summary>
        ///
        /// <param name="flag">Flag model containing the necessary data to query a row from the database table PartFlag</param>
        ///
        /// <returns>Count of times a particular flag has been cited.</returns>
        public AuthorizationLevel GetAuthorizationLevel(string featureName, string typeName) 
        {
            int count = 0;
            bool featureFound = false;

            AuthorizationLevel authorizationLevel = new AuthorizationLevel();
            string query = @$"
                SELECT Count(typeName) as count
                FROM Authorization 
                WHERE featureName = @FeatureName AND typeName = @TypeName
                ";
            
            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                //Add parameters to the dml statement with built in functions to avoid sql injection
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.Add("@FeatureName", MySqlDbType.VarChar);
                cmd.Parameters["@FeatureName"].Value = featureName;

                cmd.Parameters.Add("@TypeName", MySqlDbType.VarChar);
                cmd.Parameters["@TypeName"].Value = typeName;

                var reader = cmd.ExecuteReader();
                
                //Only update count to a value other than zero if a row has been retrieved in the query.
                if (reader.HasRows)
                {
                    reader.Read();
                    count = reader.GetInt32("count");
                    if (count > 0) 
                    {
                        featureFound = true;
                        authorizationLevel = new AuthorizationLevel(typeName, featureName, featureFound);
                    }
                    else
                    {
                        authorizationLevel = new AuthorizationLevel();
                    }
                    
                }
            }
            catch (Exception)
            {
                authorizationLevel = new AuthorizationLevel();
            }
            finally
            {
                connection.Close();
            }
            return authorizationLevel;
        }
    }
}
