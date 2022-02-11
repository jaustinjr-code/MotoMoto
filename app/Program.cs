using TheNewPanelists.ApplicationLayer.Authentication;
using TheNewPanelists.ApplicationLayer;
using System.Collections;

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            IEntry entry;
            string input = menu();
            while (input != "EXIT")
            {
                if (input != "")
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
            Console.WriteLine("6) Bulk Operation");
            Console.WriteLine("7) Exit");

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
                    return "BULK";
                case "7":
                    return "EXIT";
                default:
                    Console.WriteLine("Invalid Input - Try Again");
                    return menu();
            }
        }
    }
}