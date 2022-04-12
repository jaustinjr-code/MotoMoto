using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class EventAccountVerificationManager
    {
        
        private string requestPath;
        public EventAccountVerificationManager()
        {
            requestPath = "";
        }

        public EventAccountVerificationManager(string filepath)
        {
            this.requestPath = filepath;
        }

/*        public bool IsValidRequest(Dictionary<string, string> request)
        {
            bool containsOperation = request.ContainsKey("operation");
            if (containsOperation)
            {
                return HasValidAttributes(request["operation"].ToUpper(), request);
            }
            return false;
        }*/

/*        public bool HasValidAttributes(string operation, Dictionary<string, string> attributes)
        {
            bool hasValidAttributes = false;
            switch (operation.ToUpper())
            {
                case "FIND_RATING":
                    hasValidAttributes = attributes.ContainsKey("username");
                    break;

                case "FIND_REVIEW":
                    hasValidAttributes = attributes.ContainsKey("username");
                    break;
                case "POST_RATING_AND_REVIEW":
                    hasValidAttributes = attributes.ContainsKey("username");
                    break;
            }
            return hasValidAttributes;
        }*/

/*        public void ParseAndCall()
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
        }*/

/*        public bool CallOperation(string operation, Dictionary<string, string> accountInfo)
        {
            bool returnVal = false;
            if (HasValidAttributes(operation, accountInfo))
            {
                EventAccountVerificationService evntAccntVerifServiceObject = new EvntAccntVerifService(operation, accountInfo);
                if (evntAccntVerifServiceObject.SqlGenerator())
                {
                    returnVal = true;
                }
            }

            return returnVal;
        }*/

    }
        
}
