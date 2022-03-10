using System;
using System.Collections;
using TheNewPanelists.ApplicationLayer;
using TheNewPanelists.ServiceLayer;
using TheNewPanelists.BusinessLayer;
using TheNewPanelists.ServiceLayer.UserManagement;

namespace TheNewPanelists.ApplicationLayer
{
    public class RegistrationEntry : IEntry
    {
        private string operation { get; set; }
        private Dictionary<string, string> request { get; set; }

        public RegistrationEntry() 
        {
            this.operation = "";
            this.request = new Dictionary<string, string>();
        }

        public RegistrationEntry(string operation, Dictionary<string, string> request)
        {
            this.operation = operation;
            this.request = request;
        }

        public string SingleOperationRequest()
        {
            string result = "";
            bool sendOp = false;
            bool receiveOp = false;

            sendOp = this.operation.Contains("DROPREG")
                || this.operation.Contains("ISVALID")
                || this.operation.Contains("ACCOUNT REGISTRATION")
                || this.operation.Contains("REGDOESNOTEXIST");
            receiveOp = this.operation.Contains("CONFIRMREG")
                || this.operation.Contains("RETURNREG");

            RegistrationManager registrationManager = new RegistrationManager(this.operation, this.request);

            if (sendOp)
            {
                if (registrationManager.SendOperation())
                    result = "Operation successful.";
                else
                    result = "Operation failed.";
            }
            else if (receiveOp)
            {
                Dictionary<string, string> queryResult = registrationManager.ReceiveOperation();

                if (queryResult.ContainsKey("Error"))
                    result = "Operation failed";
                else
                {
                    foreach (var item in queryResult)
                        result += $@"{item.Key.ToString()}: {item.Value.ToString()}\n";
                }
            }
            return result;
        }

        private string RegistrationRequest()
        {
            string result = "";
            RegistrationManager registrationManager = new RegistrationManager(this.operation, this.request);
            if (registrationManager.SendOperation("REGDOESNOTEXIST", this.request))
            {
                if (registrationManager.SendOperation())
                {
                    Dictionary<string, string> regAcct = registrationManager.ReceiveOperation("FINDREG", request);
                    // if (registrationManager.SendEmail(request["email"]))
                    if (registrationManager.SendEmail("daniel.bribiesca@student.csulb.edu"))
                    {
                        result = "Registration successful. Email confirmation pending.";
                    }
                    else
                    {
                        registrationManager.SendOperation("DROPREG", regAcct);
                        result = "Error: Email did not sent correctly.";
                    }
                }
                else
                    result = "Registration error. Please try again."; 
            }
            else
                result = "You are already registered for an account. Please confirm email to confirm registration.";

            return result;
        }

        public string EmailConfirmationRequest()
        {
            RegistrationManager registrationManager = new RegistrationManager(this.operation, this.request);
            Dictionary<string, string> regInfo = registrationManager.ReceiveOperation("CONFIRMREG", request);

            if (regInfo.ContainsKey("Error"))
            {
                Console.WriteLine(regInfo["Error"]);
                return "Registration not found.";
            }
            else
            {
                UserManagementManager userManagementManager = new UserManagementManager();
                string uniqueName = "";
                uniqueName = registrationManager.GenerateUniqueName();
                regInfo["username"] = uniqueName;

                if (userManagementManager.CallOperation("CREATE", regInfo))
                {
                    if (registrationManager.SendOperation("ISVALID", regInfo))
                        return "Email Confirmed. Registration Successful.\n\n Your username is: " + uniqueName;
                    else
                        userManagementManager.CallOperation("DROP", regInfo);
                }
            }
            return "Confirmation error.";
        }
    }
}