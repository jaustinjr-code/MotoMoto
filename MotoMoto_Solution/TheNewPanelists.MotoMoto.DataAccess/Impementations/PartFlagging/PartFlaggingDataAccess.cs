using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    /// <summary>
    /// Contains functionality that retrieves or modifies infomation stored in external databases
    /// pertaining to part flags.
    /// </summary>
    public class PartFlaggingDataAccess : IPartFlaggingDataAccess
    {
        /// <summary>
        /// String used for connecting the MotoMoto application to its external database
        /// </summary>
        private readonly string _connectionString = "server=moto-moto.crd4iyvrocsl.us-west-1.rds.amazonaws.com;user=dev_moto;database=pro_moto;port=3306;password=motomoto;";

        /// <summary>
        /// Updates the PartFlag table to reflect the incoming Flag.
        /// If the flag exists, increment the flag count, else create a new row for the flag.
        /// </summary>
        ///
        /// <param name="flag">Flag model containing the necessary data to construct or update a row in the PartFlag database table</param>
        ///
        /// <returns>Boolean value representing if the flag database was successfully updated to reflect the new flag</returns>
        public async Task<bool> CreateOrIncrementFlag(FlagModel flag)
        {
            const int ZERO = 0;
            const int ONE = 1;

            bool returnVal = false;
            string dml = "";
            
            //Use flag count to determine if incoming flag needs to create a new row or update an existing row
            int currentCount = await GetFlagCount(flag);
            if (currentCount == ZERO)
            {
                dml = @$"
                INSERT INTO PartFlags (part_number, car_make, car_model, car_year, count)
                VALUES (@PartNumber, @CarMake, @CarModel, @CarYear, {ONE})
                "; 
            }
            else if (currentCount > ZERO)
            {
                dml = @$"
                UPDATE PartFlags
                SET count = {currentCount + ONE}
                WHERE part_number = @PartNumber 
                AND car_make = @CarMake
                AND car_model = @CarModel
                AND car_year = @CarYear
                "; 
            }


            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                //Add parameters to the dml statement with built in functions to avoid sql injection
                MySqlCommand cmd = new MySqlCommand(dml, connection);
                cmd.Parameters.Add("@PartNumber", MySqlDbType.VarChar);
                cmd.Parameters.Add("@CarMake", MySqlDbType.VarChar);
                cmd.Parameters.Add("@CarModel", MySqlDbType.VarChar);
                cmd.Parameters.Add("@CarYear", MySqlDbType.VarChar);

                cmd.Parameters["@PartNumber"].Value = flag.PartNumber;
                cmd.Parameters["@CarMake"].Value = flag.CarMake;
                cmd.Parameters["@CarModel"].Value = flag.CarModel;
                cmd.Parameters["@CarYear"].Value = flag.CarYear;


                
                int numEffected = await cmd.ExecuteNonQueryAsync();
                
                //Table has only been updated if the number of rows effected is a postive integer
                if (numEffected > ZERO)
                {
                    returnVal = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return returnVal;
        }


        /// <summary>
        /// Updates the part flag table to reflect the decrementing of the flag.
        /// If the flag count is greater than 1, decrement the flag count, if the flag count is 1, remove the flag,
        /// if the flag count is 0 do nothing.
        /// </summary>
        ///
        /// <param name="flag">Flag model containing the necessary data to find the flag to decrement</param>
        ///
        /// <returns>Boolean value representing if the flag database was successfully updated to reflect the flag decrement</returns>
        public async Task<bool> DecrementOrRemove(FlagModel flag)
        {
            const int ZERO = 0;
            const int ONE = 1;

            bool returnVal = false;
            string dml = "";
            
            //Use flag count to determine if flag count should be decremented, removed, or if flag count is zero and does
            int currentCount = await GetFlagCount(flag);
            if (currentCount == ZERO)
            {
                return false;
            }
            else if (currentCount == 1) 
            {
                return await DeleteFlag(flag);
            }
            else if (currentCount > ONE)
            {
                dml = @$"
                UPDATE PartFlags
                SET count = {currentCount - ONE}
                WHERE part_number = @PartNumber 
                AND car_make = @CarMake
                AND car_model = @CarModel
                AND car_year = @CarYear
                "; 
            }


            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                //Add parameters to the dml statement with built in functions to avoid sql injection
                MySqlCommand cmd = new MySqlCommand(dml, connection);
                cmd.Parameters.Add("@PartNumber", MySqlDbType.VarChar);
                cmd.Parameters.Add("@CarMake", MySqlDbType.VarChar);
                cmd.Parameters.Add("@CarModel", MySqlDbType.VarChar);
                cmd.Parameters.Add("@CarYear", MySqlDbType.VarChar);

                cmd.Parameters["@PartNumber"].Value = flag.PartNumber;
                cmd.Parameters["@CarMake"].Value = flag.CarMake;
                cmd.Parameters["@CarModel"].Value = flag.CarModel;
                cmd.Parameters["@CarYear"].Value = flag.CarYear;


                int numEffected = await cmd.ExecuteNonQueryAsync();
                
                //Table has only been updated if the number of rows effected is a postive integer
                if (numEffected > ZERO)
                {
                    returnVal = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return returnVal;
        }


        /// <summary>
        /// Removes all counts of a flag from the part flagging database if it exists
        /// </summary>
        ///
        /// <param name="flag">Flag model containing the necessary data to find the flag to decrement</param>
        ///
        /// <returns>True if the flag was found and removed, false otherwise</returns>
        public async Task<bool> DeleteFlag(FlagModel flag)
        {
            const int ZERO = 0;
            
            bool returnVal = false;
            string dml = @$"
            DELETE FROM PartFlags
            WHERE part_number = @PartNumber 
            AND car_make = @CarMake
            AND car_model = @CarModel
            AND car_year = @CarYear
            "; 

            //Check that flag exists before trying to delete it
            int currentCount = await GetFlagCount(flag);
            if (currentCount > ZERO)
            {
                 MySqlConnection connection = new MySqlConnection(_connectionString);
                 try
                 {
                    connection.Open();
                    //Add parameters to the dml statement with built in functions to avoid sql injection
                    MySqlCommand cmd = new MySqlCommand(dml, connection);
                    cmd.Parameters.Add("@PartNumber", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@CarMake", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@CarModel", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@CarYear", MySqlDbType.VarChar);

                    cmd.Parameters["@PartNumber"].Value = flag.PartNumber;
                    cmd.Parameters["@CarMake"].Value = flag.CarMake;
                    cmd.Parameters["@CarModel"].Value = flag.CarModel;
                    cmd.Parameters["@CarYear"].Value = flag.CarYear;

                    //Table has only been updated if the number of rows effected from dml is a postive integer
                    int numEffected = await cmd.ExecuteNonQueryAsync();
                    if (numEffected > ZERO)
                    {
                        returnVal = true;
                    }
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine(ex.ToString());
                 }
                 finally
                 {
                     connection.Close();
                 }
            }
            return returnVal;
            
        }

        /// <summary>
        /// Retrieves the count of number of the number of times a particular flag has been cited.
        /// If the flag does not exist in the database, the number of times cited is zero.
        /// </summary>
        ///
        /// <param name="flag">Flag model containing the necessary data to query a row from the database table PartFlag</param>
        ///
        /// <returns>Count of times a particular flag has been cited.</returns>
        public async Task<int> GetFlagCount(FlagModel flag) 
        {
            int count = 0;
            string query = @$"
                SELECT count 
                FROM PartFlags 
                WHERE part_number = @PartNumber 
                AND car_make = @CarMake
                AND car_model = @CarModel
                AND car_year = @CarYear
                ";
            
            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                //Add parameters to the dml statement with built in functions to avoid sql injection
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.Add("@PartNumber", MySqlDbType.VarChar);
                cmd.Parameters.Add("@CarMake", MySqlDbType.VarChar);
                cmd.Parameters.Add("@CarModel", MySqlDbType.VarChar);
                cmd.Parameters.Add("@CarYear", MySqlDbType.VarChar);

                cmd.Parameters["@PartNumber"].Value = flag.PartNumber;
                cmd.Parameters["@CarMake"].Value = flag.CarMake;
                cmd.Parameters["@CarModel"].Value = flag.CarModel;
                cmd.Parameters["@CarYear"].Value = flag.CarYear;

                var reader = await cmd.ExecuteReaderAsync();
                
                //Only update count to a value other than zero if a row has been retrieved in the query.
                if (reader.HasRows)
                {
                    reader.Read();
                    count = reader.GetInt32(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return count;
        }
    }
}
