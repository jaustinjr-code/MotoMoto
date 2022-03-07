using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.ApplicationLayer;
using TheNewPanelists.ServiceLayer;
using TheNewPanelists.BusinessLayer;
using TheNewPanelists.BusinessLayer.EventAccountVerification;

namespace TheNewPanelists.ApplicationLayer
{
    class EvntAccntVerifEntry : IEntry
    {
        private string? operation { get; set; }
        private Dictionary<string, string>? request { get; set; }

        private EvntAccntVerifManager evntAccntVerifManager;

        public EvntAccntVerifEntry()
        {
            this.evntAccntVerifManager = new EvntAccntVerifManager();
        }

        public EvntAccntVerifEntry(string operation, Dictionary<string, string> request)
        {
            this.operation = operation;
            this.request = request;
            this.evntAccntVerifManager = new EvntAccntVerifManager();
        }

        public string SingleOperationRequest()
        {
            try
            {
                if (this.operation == null || request == null)
                {
                    return "ERROR - Event Account Verification operation was not successful";
                }
                evntAccntVerifManager.CallOperation(this.operation, request);
                return "Event Account Verification operation was successful";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "ERROR - Event Account Verification operation was not successful";
            }

        }
    }
}
