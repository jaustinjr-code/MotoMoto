using System;
using System.Collections;
using TheNewPanelists.ApplicationLayer;
using TheNewPanelists.ServiceLayer;
using TheNewPanelists.BusinessLayer;
using TheNewPanelists.ServiceLayer.UserManagement;

namespace TheNewPanelists.ApplicationLayer
{
    class RegistrationEntry : IEntry
    {
        private string operation { get; set; }
        private Dictionary<string, string> request { get; set; }

        private UserManagementManager userManagementManager;

        private RegistrationService registrationService;

        public RegistrationEntry() {}
        public RegistrationEntry(string operation, Dictionary<string, string> request)
        {
            this.operation = operation;
            this.request = request;
            this.userManagementManager = new UserManagementManager();
            this.registrationService = new RegistrationService();
        }

        public string SingleOperationRequest()
        {
            return null;
        }

        public string RegistrationRequest()
        {
            if (userManagementManager.CallOperation(this.operation, request))
            {
                Dictionary<string, string> regAcct = userManagementManager.ReturnRegOperation("FINDREG", request);
                RegistrationService registrationService = new RegistrationService(regAcct);
                if (registrationService.SendEmail())
                {
                    return ("Registration successful. Email confirmation pending.");
                }
                else
                    userManagementManager.CallOperation("DROPREG", regAcct);
            }
            return ("Registration error. Please try again.");
        }

        public string EmailConfirmationRequest()
        {
            Dictionary<string, string> regInfo;
            regInfo = userManagementManager.ReturnRegOperation("FINDREG", request);

            if (regInfo.Count != 0)
            {
                string uniqueName = "";
                RegistrationService registrationService = new RegistrationService(regInfo);
                uniqueName = registrationService.GenerateUniqueName();
                regInfo["username"] = uniqueName;

                if (userManagementManager.CallOperation("CREATE", regInfo))
                {
                    userManagementManager.CallOperation("ISVALID", regInfo);
                    return "Email Confirmed. Registration Successful.\n\n Your username is: " + uniqueName;
                }
            }

            return "Confirmation error.";
        }
    }
}