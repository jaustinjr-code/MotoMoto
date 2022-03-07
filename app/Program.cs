using TheNewPanelists.ApplicationLayer.Authorization;
using TheNewPanelists.ApplicationLayer;
using TheNewPanelists.ServiceLayer.Authentication;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Collections;
using MySqlConnector;
using System.Text;

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            // UserManagementAuthorization ua = new UserManagementAuthorization();


            IEntry entry;
            string input = menu();
            int attempts = 0;
            while (input != "EXIT")
            {
                if (input == "AUTHENTICATE")
                {
                    AuthenticationService authService = new AuthenticationService(input);
                    authService.RequestInput();
                    if (authService.attempts == 5)
                    {
                        input = "DISABLE";
                        Dictionary<string, string> request = InputRequest(input);
                        if (request != null)
                        {
                            entry = new UserManagementEntry(input, request);
                            Console.WriteLine(entry.SingleOperationRequest());
                        }
                    }
                }
                else if (input == "ACCOUNT REGISTRATION")
                {
                    bool SessionIsAuthenticated = false;

                    if (!SessionIsAuthenticated)
                    {
                        Dictionary<string, string> request = InputRequest(input);
                        entry = new RegistrationEntry(input, request);
                        string result = ((RegistrationEntry)entry).RegistrationRequest();

                        Console.WriteLine(result);
                    }
                    else
                        Console.WriteLine("Invalid request. User in active session.");
                }
                else if (input == "EMAIL VALIDATION")
                {
                    Dictionary<string, string> request = InputRequest(input);
                    entry = new RegistrationEntry(input, request);
                    Console.WriteLine(((RegistrationEntry)entry).EmailConfirmationRequest());
                }
                else if (input == "BULK")
                {
                    entry = new UserManagementEntry();
                    Console.Write("Enter the request file path: ");
                    string? filepath = Console.ReadLine();
                    Console.WriteLine(((UserManagementEntry)entry).BulkOperationRequest(filepath!));
                }
                else if (input != "")
                {
                    Dictionary<string, string> request = InputRequest(input);
                    if (request != null)
                    {
                        if (input == "FIND_RATING" || input == "FIND_REVIEW" || input == "POST_RATING_AND_REVIEW")
                        {
                            entry = new EvntAccntVerifEntry(input, request);
                            Console.WriteLine(entry.SingleOperationRequest());
                        }
                        else
                        {
                            entry = new UserManagementEntry(input, request);
                            Console.WriteLine(entry.SingleOperationRequest());
                        }

                    }
                    else Console.WriteLine("No request...");
                }
                input = menu();
            }
        }
        public static Dictionary<string, string> InputRequest(string operation)
        {
            Dictionary<string, string> request = new Dictionary<string, string>();
            if (operation == "CREATE")
            {
                Console.WriteLine("Enter the fields to create a new account:");

                Console.Write("Type ID: (ADMIN, REGISTERED, DEFAULT) ");

                // string type = Console.ReadLine();
                // request.Add("typeName", type);

                string? type = Console.ReadLine();
                request.Add("typeId", type!);

                Console.Write("Username: ");
                string? username = Console.ReadLine();
                request.Add("username", username!);
                // Password entry is not secured!
                Console.Write("Password: ");
                string? password = Console.ReadLine();
                request.Add("password", password!);
                Console.Write("Email: ");
                string? email = Console.ReadLine();
                request.Add("email", email!);
                Console.Write("Status: (TRUE/FALSE) ");
                string? status = Console.ReadLine();
                request.Add("status", status!);
                Console.Write("Event Account Enabled: (TRUE/FALSE) ");
                string? eventAccount = Console.ReadLine();
                request.Add("eventAccount", eventAccount!);
            }
            else if (operation == "ACCOUNT REGISTRATION")
            {
                bool emailValid = false;
                bool passwordValid = false;
                string email = "";
                string password = "";
                while (!emailValid)
                {
                    Console.Write("Email: ");
                    email = Console.ReadLine();
                    try
                    {
                        var eAddr = new MailAddress(email);
                        emailValid = eAddr.Address == email;
                    }
                    catch
                    {
                        emailValid = false;
                    }

                }
                request.Add("email", email);

                Regex letter = new Regex(@"[a-zA-Z]");
                Regex num = new Regex(@"[0-9]");
                Regex specialChar = new Regex(@"[. ,@!]");
                Regex length = new Regex(@"[a-zA-Z0-9.,@!]{8,}");
                StringBuilder input = new StringBuilder();

                while (!passwordValid)
                {
                    Console.Write("Password: ");

                    while (true)
                    {
                        var key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Enter) break;
                        if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
                        else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
                    }

                    password = input.ToString();
                    passwordValid = letter.IsMatch(password) && num.IsMatch(password)
                        && specialChar.IsMatch(password) && (password.Length > 8);
                }
                request.Add("password", password);
            }
            else if (operation == "EMAIL VALIDATION")
            {
                Console.Write("Email: ");
                string? email = Console.ReadLine();
                request.Add("email", email!);
                Console.Write("URL: ");
                string? url = Console.ReadLine();
                request.Add("url", url!);
            }
            else if (operation == "DROP")
            {
                Console.WriteLine("Enter the account username to delete:");
                Console.Write("Username: ");
                string? username = Console.ReadLine();
                request.Add("username", username!);
                Console.WriteLine("Enter "+username+"'s Password: ");
                string? password = Console.ReadLine();
                request.Add("password", password!);
            }
            else if (operation == "UPDATE")
            {
                Console.WriteLine("Enter the fields to update: (Leave blank if no change)");

                Console.Write("Username: ");
                string? username = Console.ReadLine();
                request.Add("username", username!);

                Console.Write("New Username: ");
                string? newusername = Console.ReadLine();
                if (username != "")
                    request.Add("newusername", username!);

                // This is not secured!
                Console.Write("New Password: ");
                string? password = Console.ReadLine();
                if (password != "")
                    request.Add("newpassword", password!);

                Console.Write("New Email: ");
                string? email = Console.ReadLine();
                if (email != "")
                    request.Add("newemail", email!);
            }
            else if (operation == "DISABLE")
            {
                Console.WriteLine("Are you an ADMIN?");
                if (Console.ReadLine() == "y")
                {
                    Console.WriteLine("Enter the account username to delete:");

                    Console.Write("Username: ");
                    string? username = Console.ReadLine();
                    request.Add("username", username!);
                }
                else
                {
                    Console.WriteLine("Not an ADMIN!");
                    return request;
                }
            }
            else if (operation == "ENABLE")
            {
                Console.WriteLine("Are you an ADMIN?");
                if (Console.ReadLine() == "y")
                {
                    Console.WriteLine("Enter the account username to delete:");

                    Console.Write("Username: ");
                    string? username = Console.ReadLine();
                    request.Add("username", username!);
                }
                else
                {
                    Console.WriteLine("Not an ADMIN!");
                    return request;
                }
            } 
            else if (operation == "ACCOUNT RECOVERY")
            {
                request = accountRecovery(request);
            }
            else if (operation == "AUTHENTICATE")
            {
                Console.WriteLine("Enter the account information to authenticate");

                Console.Write("Username: ");
                string? username = Console.ReadLine();
                request.Add("username", username!);

                Console.Write("Password: ");
                string? password = Console.ReadLine();
                request.Add("password", password!);

                Console.Write("OTP: ");
                string? otp = Console.ReadLine();
                request.Add("otp", otp!);
            }

            else if (operation == "FIND_RATING")
            {
                Console.WriteLine("Enter the name of the account that you want to rate");

                Console.Write("Username: ");
                string? username = Console.ReadLine();
                request.Add("username", username!);

                //Console.Write("Rating from 1-5: ");
                //string rating = Console.ReadLine();
                //request.Add("FIND_RATING", rating);

            }

            else if (operation == "FIND_REVIEW")
            {
                Console.WriteLine("Enter the name of the account that you want to review");

                Console.Write("Username: ");
                string? username = Console.ReadLine();
                request.Add("username", username!);

                //Console.Write("Rating from 1-5: ");
                //string review = Console.ReadLine();
                //request.Add("review", review);
            }

            else if (operation == "POST_RATING_AND_REVIEW")
            {
                Console.WriteLine("Enter the name of the account that you want to Rate and Review");

                Console.Write("Username: ");
                string? username = Console.ReadLine();
                request.Add("username", username!);

                Console.Write("Rating from 1-5: ");
                string? rating = Console.ReadLine();
                request.Add("rating", rating!);

                Console.Write("Review using 0-1000 UTF-8 Characters: ");
                string? review = Console.ReadLine();
                request.Add("review", review!);
            }

            return request;
        }

        public static string menu()
        {
            Console.WriteLine("1) Create User");
            Console.WriteLine("2) Delete User");
            Console.WriteLine("3) Update User");
            Console.WriteLine("4) Disable User");
            Console.WriteLine("5) Enable User");
            Console.WriteLine("6) Bulk Operation");
            Console.WriteLine("7) Account Recovery");
            Console.WriteLine("8) Authentication");
            Console.WriteLine("9) Exit");

            Console.WriteLine("10) Find Rating");
            Console.WriteLine("11) Find Review");
            Console.WriteLine("12) Post Rating and Review");
            Console.WriteLine("13) Account Registration");
            Console.WriteLine("14) Email Validation");

            switch (Console.ReadLine())
            {
                case "1":
                    return "CREATE";
                case "2":
                    return "DROP";
                case "3":
                    return "UPDATE";
                case "4":
                    return "DISABLE";
                case "5":
                    return "ENABLE";
                case "6":
                    return "BULK";
                case "7":
                    return "ACCOUNT RECOVERY";
                case "8":
                    return "AUTHENTICATE";
                case "9":
                    return "EXIT";
                case "13":
                    return "ACCOUNT REGISTRATION";
                case "14":
                    return "EMAIL VALIDATION";
                case "10":
                    return "FIND_RATING";
                case "11":
                    return "FIND_REVIEW";
                case "12":
                    return "POST_RATING_AND_REVIEW";

                default:
                    Console.WriteLine("Invalid Input - Try Again");
                    return menu();
            }
        }
        public static Dictionary<string, string> accountRecovery(Dictionary<string, string> request)
        {
            Console.WriteLine("1. Forgot Username");
            Console.WriteLine("2. Forgot Password");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Enter account email:");
                    string? email = Console.ReadLine();
                    request.Add("email", email!);
                    break;
                case "2":
                    Console.WriteLine("Enter account username:");
                    string? username = Console.ReadLine();
                    request.Add("username", username!);
                    break;
                //case "3":
                //    return "EXIT";
                //    break;
                default:
                    Console.WriteLine("Invalid Input - Try Again");
                    return accountRecovery(request);
            }
            return request;
        }
        /**
         * Logs the user out of their account and close all open connection and any data that is left over
         * Currently logout needs the user's username and a connection string to work
         * 
         * Logout feature hasn't been tested yet 
         * logging feature, cleaning data, and closing connections has not been implemented
         * 
         **/

        public static void logout(string username, string connectionString)//or string for username
        {
            MySql.Data.MySqlClient.MySqlConnection con = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
            con.Open();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand("SELECT * FROM USER u WHERE u.username = " + username, con);
            MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows) //if there is an element
            {
                if (reader["status"].Equals(true))

                {
                    //tenant is in a session
                    Console.WriteLine("Do you want to logout (Y/N)");
                    if (Console.ReadLine()!.ToUpper() == ("Y"))
                    {
                        //change user status to false;
                        string updateUserStatusQ = "UPDATE USER u WHERE u.username = @username SET u.status = @status";
                        cmd.CommandText = updateUserStatusQ;
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@status", false);
                        cmd.ExecuteNonQuery();

                        //Log Success

                        menu();
                    }

                }
                else //status is incorrect or user is not currently in a session
                {
                    Console.WriteLine("ERROR: Currently not in session");
                    menu();
                }
            }
            else//Error: no user is found
            {
                Console.WriteLine("ERROR: No user found");
                menu();
            }
            con.Close(); 
        }
    }
}