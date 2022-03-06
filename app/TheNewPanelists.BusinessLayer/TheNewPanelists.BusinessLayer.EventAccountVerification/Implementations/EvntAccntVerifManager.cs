using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using TheNewPanelists.ServiceLayer.EventAccountVerification;

namespace TheNewPanelists.BusinessLayer.EventAccountVerification
{
    public class EvntAccntVerifManager
    {
        private string requestPath;
        public EvntAccntVerifManager()
        {
            requestPath = "";
        }

        public EvntAccntVerifManager(string filepath)
        {
            this.requestPath = filepath;
        }

        public bool IsValidRequest(Dictionary<string, string> request)
        {
            bool containsOperation = request.ContainsKey("operation");
            if (containsOperation)
            {
                return HasValidAttributes(request["operation"].ToUpper(), request);
            }
            return false;
        }

        public bool HasValidAttributes(string operation, Dictionary<string, string> attributes)
        {
            bool hasValidAttributes = false;
            switch (operation.ToUpper())
            {
                case "FIND_RATING":
                    hasValidAttributes = attributes.ContainsKey("userId");
                    break;

                //case "CREATE":
                //    hasValidAttributes = attributes.ContainsKey("username") && attributes.ContainsKey("password")
                //                            && attributes.ContainsKey("email");
                //    break;

                //case "DROP":
                //    hasValidAttributes = attributes.ContainsKey("username");
                //    break;

                //case "UPDATE":
                //    hasValidAttributes = (attributes.ContainsKey("newusername") || attributes.ContainsKey("newpassword")
                //                            || attributes.ContainsKey("newemail")) && attributes.ContainsKey("username");
                //    break;
                //case "ACCOUNT RECOVERY":
                //    hasValidAttributes = attributes.ContainsKey("username") || attributes.ContainsKey("email");
                //    break;
            }
            return hasValidAttributes;
        }

        public void ParseAndCall()
        {
            string requestPath = this.requestPath;
            foreach (string line in System.IO.File.ReadLines(@requestPath))
            {
                Dictionary<string, string> requestDictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(line) ?? throw new ArgumentException();
                string operation = requestDictionary["operation"];
                if (IsValidRequest(requestDictionary))
                {
                    CallOperation(operation, requestDictionary);
                }
            }
        }

        public bool CallOperation(string operation, Dictionary<string, string> accountInfo)
        {
            bool returnVal = false;
            if (HasValidAttributes(operation, accountInfo))
            {
                EvntAccntVerifService evntAccntVerifServiceObject = new EvntAccntVerifService(operation, accountInfo);
                if (evntAccntVerifServiceObject.SqlGenerator())
                {
                    returnVal = true;
                }
            }

            return returnVal;
        }

    }
}
