using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class DirectMessageDataAccess
    {
        private string _connectionString = "server=localhost;user=root;database=motomoto_um;port=3306;password=password;";
        
        public bool IsValidUser(string username)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection Open");
                string query = "SELECT * FROM USER u WHERE u.username = '" + username + "'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows) 
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
            finally 
            {
                connection.Close();
            }

        }

        private List<int> getSenderRecieverID(string sender, string reciever)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            List<int> senderRecieverId = new List<int>();
            try
            {
                connection.Open();
                Console.WriteLine("Connection Open");
                string getSenderUserIdQuery = "SELECT userId FROM USER u WHERE u.username = '" + sender + "'";
                MySqlCommand cmd = new MySqlCommand(getSenderUserIdQuery, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    senderRecieverId.Add(reader.GetInt32(0));
                    reader.Close();
                }

                string getRecieverUserIdQuery = "SELECT userId FROM USER u WHERE u.username = '" + reciever + "'";
                cmd = new MySqlCommand(getRecieverUserIdQuery, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    senderRecieverId.Add(reader.GetInt32(0));
                    reader.Close();
                }
                reader.Close();
   
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return senderRecieverId;
        }

        private bool NoDuplicateMessage(string sender, string receiver)
        {
            List<int> ids = getSenderRecieverID(sender, receiver);
            if (ids.Count != 2)
            {
                return false;
            }
            int senderId = ids.ElementAt(0);
            int receiverId = ids.ElementAt(1);
            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection Open");
                string query = "SELECT * FROM MESSAGEHISTORY mh WHERE (mh.senderId = '" + senderId + "' AND mh.recieverId = '" + receiverId + "') OR (mh.senderId = '" + receiverId + "' AND mh.recieverId = '" + senderId + "');";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    return false;
                }
                reader.Close();
                return true;
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
            finally
            {
                
                connection.Close();
            }
        }

        public bool CreateNewDirectMessage(string sender, string receiver)
        {
            List<int> ids = getSenderRecieverID(sender, receiver);
            if(ids.Count != 2)
            {
                return false;
            }
            int senderId = ids.ElementAt(0);
            int receiverId = ids.ElementAt(1);
            if (!NoDuplicateMessage(sender, receiver))
            {
                return false;
            }

            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection Open");
                string query = "INSERT INTO MESSAGEHISTORY (senderID, recieverID) VALUES (" + senderId + "," + receiverId + ");";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery(); 
                return true;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
            

        }
            
        public bool SendMessage(string sender, string receiver, string message)
        {
            int messageHistoryId = -1;
            List<int> ids = getSenderRecieverID(sender, receiver);
            if (ids.Count != 2)
            {
                return false;
            }
            int senderId = ids.ElementAt(0);
            int receiverId = ids.ElementAt(1);

            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection Open");
                string query = "SELECT messageHistoryId FROM MESSAGEHISTORY mh WHERE senderID = '" + senderId + "' AND recieverID = '" + receiverId + "';";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    messageHistoryId = reader.GetInt32(0);
                    reader.Close();
                }
                else
                {
                    //missing a history imput
                    return false;
                }

                query = "INSERT INTO MESSAGES (messageHistoryId, messages) VALUES ('" + messageHistoryId + "' , '" + message + "');";
                cmd = new MySqlCommand(query, connection);
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
                connection.Close();
            }
        }
    
        public List<string> GetMessageHistory(string sender)
        {
            List<string> userMessaged = new List<string>();
            List<int> ids = getSenderRecieverID(sender, "");
            int senderId = ids.ElementAt(0);

            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection Open");
                string query = "SELECT username FROM USER u INNER JOIN MESSAGEHISTORY mh ON u.userID = mh.recieverId WHERE mh.senderID = '" + senderId + "';";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    userMessaged.Add(reader.GetString(0));
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                connection.Close();
            }
            return userMessaged;
        }

        public List<List<string>> GetMessages(string sender, string receiver)
        {  
            List<List<string>>allMessages = new List<List<string>>();
            List<string>messages = new List<string>();

            List<int> ids = getSenderRecieverID(sender, receiver);
            int senderId = ids.ElementAt(0);
            int receiverId = ids.ElementAt(1);
            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                string query = "SELECT U.USERNAME, M.MESSAGES,M.TIMESTAMP FROM MESSAGES M " +
                    "INNER JOIN MESSAGEHISTORY MH ON M.MESSAGEHISTORYID = MH.MESSAGEHISTORYID " +
                    "INNER JOIN USER U ON MH.SENDERID = U.USERID " +
                    "WHERE MH.SENDERID = '" + senderId + "'AND MH.RECIEVERID = '" + receiverId +"'" +
                    "UNION  " +
                    "SELECT U.USERNAME, M.MESSAGES,M.TIMESTAMP FROM MESSAGES M " +
                    "INNER JOIN MESSAGEHISTORY MH ON M.MESSAGEHISTORYID = MH.MESSAGEHISTORYID " +
                    "INNER JOIN USER U ON MH.SENDERID = U.USERID " +
                    "WHERE MH.SENDERID = '" + receiverId + "'" + " AND MH.RECIEVERID = '" + senderId + "' " +
                    "ORDER BY TIMESTAMP;";
                MySqlCommand cmd = new MySqlCommand( query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    messages.Add(reader.GetString(0));
                    messages.Add(reader.GetString(1));
                    messages.Add(reader.GetString(2));
                    allMessages.Add(messages);
                    messages = new List<string>();
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
            return allMessages;

        }
    }
}
