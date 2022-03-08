using TheNewPanelists.ServiceLayer.UsageAnalysisDashboard;
using TheNewPanelists.ApplicationLayer;
using System.Threading.Tasks;

namespace TheNewPanelists.BusinessLayer
{
    public class UsageAnalysisDashboardManager : IAnalysisManager
    {
        // IAuthorization _authorization;
        IAnalysisService? _service;
        private string _authType;
        // public UsageAnalysisDashboardManager()
        // {
        //     Console.WriteLine("Yes");
        // }
        public UsageAnalysisDashboardManager(string[] sessionInfo)
        {
            // Call Authorization service on sessionInfo
            // _authorization = new ApplicationLayer.Authorization.UserManagementAuthorization();
            _authType = "ADMIN";
            // _authType = _authorization.getAuthType();
            if (!IsValidRequest()) throw new Exception("Unauthorized user");
        }

        // Does this take care of Update Analytics as well?
        public IList<IList<IDictionary<string, string>>?> RequestGetAnalytics(IDictionary<string, string> request)
        {
            if (_authType == null) throw new Exception("Authorization not found");
            if (_authType != null && !(_authType.Equals("ADMIN"))) throw new Exception("Unauthorized user");

            // Automatically refresh data every 60 seconds here?
            string[] tables = { "ViewAnalytics", "AdmissionAnalytics", "CommunityBoardAnalytics", "EventListAnalytics" };
            if (request["table"].Equals(tables[0]))
                _service = new ViewAnalyticService();
            // _service = new ViewAnalyticService(request);
            else if (request["table"].Equals(tables[1]))
                _service = new AdmissionAnalyticService(request);
            else if (request["table"].Equals(tables[2]))
                _service = new CommunityBoardAnalyticService(request);
            else if (request["table"].Equals(tables[3]))
                _service = new EventListAnalyticService(request);
            else throw new Exception("Invalid request");

            // Time for 15 seconds then give error if result is not returned in time
            //IList<IList<IDictionary<string, string>>?> result = new List<IList<IDictionary<string, string>>?>();
            //bool Completed = ExecuteWithTimeLimit(TimeSpan.FromSeconds(15.0), () =>
            //{
            //result = _service.GetAnalytics();
            //});
            //return result;
            //if (Completed) return result;
            //throw new Exception("Task exceeded time limit");
            // Console.WriteLine("\n\n\nReturning\n\n\n");
            return _service.GetAnalytics();
        }

        public bool RequestUpdateAnalytics()
        {
            return false;
        }

        public bool IsValidRequest()
        {
            if (_authType != null && _authType.Equals("ADMIN"))
            {
                return true;
            }
            //string[] tables = { "ViewAnalytics", "AdmissionAnalytics", "CommunityBoardAnalytics", "EventListAnalytics" };
            //IDictionary<string, string[]> tableColumns = new Dictionary<string, string[]>()
            //{
            //["ViewAnalytics"] = new string[] { "viewTitle", "displayTotal", "durationAvg" },
            //["AdmissionAnalytics"] = new string[] { "accessDate", "loginTotal", "registrationTotal" },
            //["CommunityBoardAnalytics"] = new string[] { "feedTitle", "feedPostTotal" },
            //["EventListAnalytics"] = new string[] { "eventAccountUsername", "eventRegistrationTotal" }
            //};
            //if (tableColumns.ContainsKey(request["analytic"]))
            //{


            // ViewAnalytics
            //if (request["analytic"].Equals(tables[0]))
            //{
            //if (request.ContainsKey(tableColumns[tables[0]][0])
            //&& request.ContainsKey(tableColumns[tables[0]][1])
            //&& request.ContainsKey(tableColumns[tables[0]][2]))
            //if (request.ContainsKey(tableColumns[tables[0]][1]) && request[tableColumns[tables[0]][1]] > 0
            //&& request.ContainsKey(tableColumns[tables[0]][2]))
            //{
            // View Title should be static so no check would be necessary
            //}
            //else return false;
            //if (request.ContainsKey(tableColumns[tables[0]][1]))
            //{
            // Check for valid integer
            //}
            //else return false;
            //if (request.ContainsKey(tableColumns[tables[0]][2]))
            //{
            // Check for valid integer
            //}
            //}
            //}
            //else
            //{
            //return false;
            //}
            return false;
        }

        // Valid input can be checked in the front end
        //private bool IsValidInput(string type, object value)
        //{
        //return false;
        //}

        // Source: https://stackoverflow.com/questions/7413612/how-to-limit-the-execution-time-of-a-function-in-c-sharp
        //private bool ExecuteWithTimeLimit(TimeSpan timeSpan, Action codeBlock)
        //{
        //try
        //{
        //Task task = Task.Factory.StartNew(() => codeBlock());
        //task.Wait(timeSpan);
        //return task.IsCompleted;
        //}
        //catch (AggregateException ae)
        //{
        //throw ae.InnerExceptions[0];
        //}
        //}
    }
}