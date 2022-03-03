using TheNewPanelists.ApplicationLayer.Authentication;
using TheNewPanelists.ApplicationLayer;
using TheNewPanelists.ServiceLayer.Authentication;
using System.Collections;

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            IEntry entry;
            string input = menu();
            int attempts = 0;

            while (input != "EXIT")
            {
                if (input == "AUTHENTICATE")
                {
                    while (attempts < 5)
                    {
                        string otp = createOTP();

                        Dictionary<string, string> request = InputRequest(input);
                        AuthenticationService authService = new AuthenticationService(input, request);
                        
                        bool IsValidRequest = authService.validateRequest();
                        if (!IsValidRequest)
                        {
                            if (attempts < 4) 
                            {
                                Console.WriteLine("Invalid username, password, and/or OTP." +
                                " Retry again or contact system administrator.");
                            }   
                            attempts++;  
                        }
                        else
                        {
                            authService.SqlGenerator();
                        }
                    }
                    if (attempts == 5) 
                    {
                        Console.WriteLine("You've reached the maximum authentication attempts."
                                        + "\nYour account has been disabled for security reasons.");   
                    }
                   
                }
                else if (input != "")
                {
                    Dictionary<string, string> request = InputRequest(input);
                    if (request != null)
                    {
                        entry = new UserManagementEntry(input, request);
                        Console.WriteLine(entry.SingleOperationRequest());
                    }
                    else Console.WriteLine("No request...");
                }
                else if (input == "BULK")
                {
                    entry = new UserManagementEntry();
                    Console.Write("Enter the request file path: ");
                    string filepath = Console.ReadLine();
                    Console.WriteLine(((UserManagementEntry)entry).BulkOperationRequest(filepath));
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
                string type = Console.ReadLine();
                request.Add("typeId", type);
                Console.Write("Username: ");
                string username = Console.ReadLine();
                request.Add("username", username);
                // Password entry is not secured!
                Console.Write("Password: ");
                string password = Console.ReadLine();
                request.Add("password", password);
                Console.Write("Email: ");
                string email = Console.ReadLine();
                request.Add("email", email);
                Console.Write("Status: (TRUE/FALSE) ");
                string status = Console.ReadLine();
                request.Add("status", status);
                Console.Write("Event Account Enabled: (TRUE/FALSE) ");
                string eventAccount = Console.ReadLine();
                request.Add("eventAccount", eventAccount);
            }
            else if (operation == "DELETE")
            {
                Console.WriteLine("Enter the account username to delete:");

                Console.Write("Username: ");
                string username = Console.ReadLine();
                request.Add("username", username);
            }
            else if (operation == "UPDATE")
            {
                Console.WriteLine("Enter the fields to update: (Leave blank if no change)");

                Console.Write("Username: ");
                string username = Console.ReadLine();
                request.Add("username", username);

                Console.Write("New Username: ");
                string newusername = Console.ReadLine();
                if (username != "")
                    request.Add("newusername", username);

                // This is not secured!
                Console.Write("New Password: ");
                string password = Console.ReadLine();
                if (password != "")
                    request.Add("newpassword", password);

                Console.Write("New Email: ");
                string email = Console.ReadLine();
                if (email != "")
                    request.Add("newemail", email);
            }
            else if (operation == "DISABLE")
            {
                Console.WriteLine("Are you an ADMIN?");
                if (Console.ReadLine() == "y")
                {
                    Console.WriteLine("Enter the account username to delete:");

                    Console.Write("Username: ");
                    string username = Console.ReadLine();
                    request.Add("username", username);
                }
                else
                {
                    Console.WriteLine("Not an ADMIN!");
                    return null;
                }
            }
            else if (operation == "ENABLE")
            {
                Console.WriteLine("Are you an ADMIN?");
                if (Console.ReadLine() == "y")
                {
                    Console.WriteLine("Enter the account username to delete:");

                    Console.Write("Username: ");
                    string username = Console.ReadLine();
                    request.Add("username", username);
                }
                else
                {
                    Console.WriteLine("Not an ADMIN!");
                    return null;
                }
            }
            else if (operation == "AUTHENTICATE")
            {
                Console.WriteLine("Enter the account information to authenticate");

                Console.Write("Username: ");
                string username = Console.ReadLine();
                request.Add("username", username);

                Console.Write("Password: ");
                string password = Console.ReadLine();
                request.Add("password", password);

                Console.Write("OTP: ");
                string otp = Console.ReadLine();
                request.Add("otp", otp);
            }

            foreach(KeyValuePair<string, string> entry in request){
                Console.WriteLine("The key is:{0}", entry.Key);
                Console.WriteLine("The value is:{0}", entry.Value);
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
            Console.WriteLine("6) Authenticate User");
            Console.WriteLine("7) Bulk Operation");
            Console.WriteLine("8) Exit");

            switch (Console.ReadLine())
            {
                case "1":
                    return "CREATE";
                case "2":
                    return "DELETE";
                case "3":
                    return "UPDATE";
                case "4":
                    return "DISABLE";
                case "5":
                    return "ENABLE";
                case "6":
                    return "AUTHENTICATE";
                case "7":
                    return "BULK";
                case "8":
                    return "EXIT";
                default:
                    Console.WriteLine("Invalid Input - Try Again");
                    return menu();
            }
        }

        public static string createOTP()
        {
            // A - Z: ASCII 65 - 90 rand.Next(65, 91)
            // a - z: ASCII 97 - 122 rand.Next(97, 123)
            // 0 - 9: ASCII 48 - 57 rand.Next(48, 58)
            Random rand = new Random();
            char[] chArr = new char[9];
            string otp = "";

            for (int i = 0; i < chArr.Length; i++)
            {
                int num =  i < 3? num = i : num = rand.Next(0, 3);

                if (num == 0) 
                {
                    chArr[i] = (char) rand.Next(65, 91);     // upper case
                }

                else if (num == 1)
                {
                    chArr[i] = (char) rand.Next(97, 123);   // lower case
                }

                else if (num == 2)
                {
                    chArr[i] = (char) rand.Next(48, 58);    // number 0 - 9
                }
            }

            Console.Write("before shuffle: ");
            foreach (char ch in chArr)
            {
                Console.Write(ch);
            }

            for (int i = 0; i < 100; i++)
            {
                int randNum1 = rand.Next(chArr.Length);
                int randNum2 = rand.Next(chArr.Length);
                char temp = chArr[randNum1];
                chArr[randNum1] = chArr[randNum2];
                chArr[randNum2] = temp;
            }
            
            foreach (char ch in chArr)
            {
                otp += ch;
            }
            Console.WriteLine("\notp: " + otp);
            return otp;
        }
    }
}