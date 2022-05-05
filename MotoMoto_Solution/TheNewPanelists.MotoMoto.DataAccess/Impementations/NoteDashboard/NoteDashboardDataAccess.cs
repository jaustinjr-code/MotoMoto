using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.Models.NoteDashboardModels;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class NoteDashboardDataAccess
    {
        MySqlConnection? mySqlConnection { get; set; }
        private string _connectionString = "server=moto-moto.crd4iyvrocsl.us-west-1.rds.amazonaws.com;user=dev_moto;database=pro_moto;port=3306;password=motomoto;";

        public NoteDashboardDataAccess() { }

        public NoteDashboardDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
        private bool CLoseConnection(MySqlCommand command)
        {
            try
            {
                mySqlConnection!.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection did not close");
                Console.WriteLine(ex.Message);  
                return false;
            }
        }
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
        public int getUserId(string username)
        {
            if (!EstablishMariaDBConnection())
            {
                return -1;
            }
            using (var command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = "SELECT p.userId FROM Profile p WHERE p.username = @v1;";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", username);
                command.Parameters.AddRange(parameters);

                MySqlDataReader reader = command.ExecuteReader();
                try
                {
                    int userId = -1;
                    while (reader.Read())
                    {
                        userId = reader.GetInt32(0);
                    }
                    return userId;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
                finally
                {
                    reader.Close();
                    CLoseConnection(command);
                }
            }
        }
        public List<NoteModel> GetNotes(string username, string order)
        {
            List<NoteModel> notes = new List<NoteModel>();
            int userId = getUserId(username);
            if (!EstablishMariaDBConnection())
            {
                return new List<NoteModel>();
            }
            using (var command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = "SELECT title,notes,timestamp FROM Notes WHERE userId = @v1 ORDER BY "+ order + " ;";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", userId);
                command.Parameters.AddRange(parameters);

                MySqlDataReader reader = command.ExecuteReader();
                int test = 0;
                while(reader.Read())
                {
                    Console.WriteLine("Order: " + order + " " + test + ") " + reader.GetString(0));
                    test++;
                    string note = "";
                    string title = reader.GetString(0);
                    if (!reader.IsDBNull("notes"))
                    {
                        note = reader.GetString(1);
                    }
                    DateTime date = reader.GetDateTime(2);
                    NoteModel noteModel = new NoteModel();
                    noteModel.SetNotes(note);
                    noteModel.SetTitle(title);
                    noteModel.SetUsername(username);
                    notes.Add(noteModel);
                }
                return notes;
            }
        }

        public bool AddNotes(NoteModel model)
        {
            string username = model.GetUsername();
            string title = model.GetTitle();
            int userId = getUserId(username);
            using var con = new MySqlConnection(_connectionString);
            if (!EstablishMariaDBConnection())
            {
                return false;
            }

            List<NoteModel> temp = GetNotes(username, "timeStamp ASC");
            if(temp.Count >= 100)
            {
                return false;
            }

            try
            {
                
                con.Open();

                var sql = "INSERT INTO Notes(userID, title) VALUES (@v1, @v2);";
                using var cmd = new MySqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@v1", userId);
                cmd.Parameters.AddWithValue("@v2", title);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;

            }
            finally
            {
                con.Close();
            }
           
        }
        public bool DeleteNotes(NoteModel model)
        {
            string username = model.GetUsername();
            string title = model.GetTitle();
            int userId = getUserId(username);

            List<NoteModel> list = new List<NoteModel>();
            list = GetNotes(username, "timeStamp ASC");
            bool validNote = false; 
            foreach(NoteModel m in list)
            {
                if(model.GetTitle().Equals(m.GetTitle()) && model.GetUsername().Equals(m.GetUsername()))
                {
                   validNote = true;
                }
            }
            if(!validNote)
            {
                return false;
            }
            using var con = new MySqlConnection(_connectionString);
            if (!EstablishMariaDBConnection())
            {
                return false;
            }
            try
            {              
                con.Open();

                var sql = "DELETE FROM Notes WHERE userID = @v1 AND title = @v2;";
                using var cmd = new MySqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@v1", userId);
                cmd.Parameters.AddWithValue("@v2", title);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;

            }
            finally
            {
                con.Close();
            }

        }

        public bool UpdateNotes(NoteModel model)
        {
            string username = model.GetUsername();
            string title = model.GetTitle();
            string notes = model.GetNotes();

            List<NoteModel> list = new List<NoteModel>();
            list = GetNotes(username, "timeStamp ASC");
            bool validNote = false;
            foreach (NoteModel m in list)
            {
                if (model.GetTitle().Equals(m.GetTitle()) && model.GetUsername().Equals(m.GetUsername()))
                {
                    validNote = true;
                }
            }
            if (!validNote)
            {
                return false;
            }

            DateTime currentDate = DateTime.Now;
            int userId = getUserId(username);
            using var con = new MySqlConnection(_connectionString);
            if (!EstablishMariaDBConnection())
            {
                return false;
            }
            try
            {
                
                con.Open();

                var sql = "UPDATE Notes n Set n.notes = @v2, n.timestamp = @v3 WHERE n.userID = @v4 AND n.title = @v1;";
                using var cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@v1", title);
                cmd.Parameters.AddWithValue("@v2", notes);
                cmd.Parameters.AddWithValue("@v3", currentDate);
                cmd.Parameters.AddWithValue("@v4", userId);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;

            }
            finally
            {
                con.Close();
            }

        }
    }
}
