using MySql.Data.MySqlClient;
// using System.Collections.Generic;

namespace TheNewPanelists.ServiceLayer.UsageAnalysisDashboard
{
    interface IAnalysisService
    {
        // bool SqlGenerator();
        // List<Dictionary<string, string>> RefineData(MySqlDataReader reader);
        IList<IList<IDictionary<string, string>>?> GetAnalytics();
        bool UpdateAnalytics();
    }
}