﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class EventAccountVerificationDataAccess : IDataAccess
    {
        private string? query { get; set; }
        private MySqlConnection? mySqlConnection = null;

        public EventAccountVerificationDataAccess()
        {
            query = null;
            mySqlConnection = null;
        }

        public EventAccountVerificationDataAccess(string query)
        {
            this.query = query;
        }

        private void BuildTempUser()
        {
            // Hides password
            Console.WriteLine("Please Enter Your MariaDB Username:");
            string? username = Console.ReadLine();
            Console.WriteLine($"Please Enter the password for {username}:");
            StringBuilder input = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
                else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
            }
            string pass = input.ToString();
            // Console.WriteLine(pass);
            // Console.WriteLine(System.Environment.UserName);

            MySqlConnection tempMySqlConnection = new MySqlConnection($"server=localhost;user={username};password={pass}");
            // MySqlConnection tempMySqlConnection = new MySqlConnection($"server=localhost;user={user};password={pass}");
            try
            {
                tempMySqlConnection.Open();
                // MySqlCommand cmd1 = new MySqlCommand("DROP USER IF EXISTS 'tempuser'@'localhost';", tempMySqlConnection);
                MySqlCommand cmd2 = new MySqlCommand("CREATE USER IF NOT EXISTS 'tempuser'@'localhost' IDENTIFIED BY '123';", tempMySqlConnection);
                MySqlCommand cmd3 = new MySqlCommand("GRANT ALL PRIVILEGES ON *.* TO 'tempuser'@'localhost' WITH GRANT OPTION;", tempMySqlConnection);
                MySqlCommand cmd4 = new MySqlCommand("FLUSH PRIVILEGES;", tempMySqlConnection);
                // MySqlCommand cmd4 = new MySqlCommand("SHOW DATABASE LIKE logs_MM_test;", tempMySqlConnection);
                // MySqlCommand cmd5 = new MySqlCommand("CREATE DATABASE IF NOT EXISTS logs_MM_test;", tempMySqlConnection);

                Console.WriteLine("Connection Open...");
                // cmd1.ExecuteNonQuery();
                Console.WriteLine("DROP");
                cmd2.ExecuteNonQuery();
                Console.WriteLine("GRANT");
                cmd3.ExecuteNonQuery();
                Console.WriteLine("FLUSH");
                cmd4.ExecuteNonQuery();
                Console.WriteLine("CREATE");
                // cmd5.ExecuteNonQuery();
                Console.WriteLine("Temp User Created...");
                tempMySqlConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exited Program with Exit " + e.Message);
            }
            EstablishMariaDBConnection();
        }

        public bool EstablishMariaDBConnection()
        {
            Dictionary<string, string> informationLog = new Dictionary<string, string>();

            Console.WriteLine("Please Enter a Valid Database/Schema: ");
            string? databaseName = Console.ReadLine();

            Console.WriteLine("Please Enter Database/Schema password: ");
            StringBuilder input = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
                else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
            }
            string databasePass = input.ToString();
            // MySqlConnection mySqlConnection;
            // This is a hardcoded string, it will be different based on your naming
            // Need to generalize the database name or create a new database and run the restore sql file on it

            /** ROOT CONNECTION PASSWORD IS DIFFERENT FOR EVERYONE!!! PLEASE CHANGE*/
            string connectionString = $"server=localhost;user=root;database={databaseName};port=3306;password={databasePass};";
            //connectionString 
            try
            {
                mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();

                Console.WriteLine("Connection open");

                // Console.WriteLine("Close");
                //mySqlConnection.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("ERROR - Creating new user...");
                BuildTempUser();
            }

            return false;
        }

        public bool FindRatingOrAccount()
        {
            if (!EstablishMariaDBConnection()) Console.WriteLine("Connection failed to open...");
            else Console.WriteLine("Connection opened...");

            MySqlCommand command = new MySqlCommand(this.query, mySqlConnection);
            if (command.ExecuteNonQuery() == -1)
            {
                if (mySqlConnection != null)
                {
                    mySqlConnection.Close();
                    Console.WriteLine("Connection closed...");

                    return true;
                }
                else { return false; }

            }
            if (mySqlConnection != null)
            {
                mySqlConnection.Close();
                Console.WriteLine("Connection closed...");
                return false;
            }
            else { return false; }
        }

        public bool PostRatingAndReview()
        {
            if (!EstablishMariaDBConnection()) Console.WriteLine("Connection failed to open...");
            else Console.WriteLine("Connection opened...");

            MySqlCommand command = new MySqlCommand(this.query, mySqlConnection);
            if (command.ExecuteNonQuery() == 1)
            {
                if (mySqlConnection != null)
                {
                    mySqlConnection.Close();
                    Console.WriteLine("Connection closed...");

                    return true;
                }

            }
            if (mySqlConnection != null)
            {
                mySqlConnection.Close();
                Console.WriteLine("Connection closed...");
                return false;
            }
            else { return false; }
        }
    }
}
