using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.BusinessLayer;
using TheNewPanelists.ApplicationLayer;
using System.Web;

namespace applayer.Controllers;

[ApiController]
[Route("[controller]")]
public class UsageAnalysisDashboardController : ControllerBase
{
    public UsageAnalysisDashboardController()
    {

    }

    [Route("GetAnalytics")]
    [HttpGet]
    // public IList<IList<IDictionary<string, string>>> Get()
    public IEnumerable<IList<IList<IDictionary<string, string>>>>? Get()
    {
        Console.WriteLine("In");
        string username = "", password = "";
        // Change to Authorization class
        // NOTE: authorization requires console input so maybe not use it right now?
        bool authNSession = true;
        string[] sessionInfo = { username, password };
        IAnalysisManager manager;

        if (authNSession)
            manager = new UsageAnalysisDashboardManager(sessionInfo);
        else
        {
            Console.WriteLine("Unauthorized user");
            return null; // route to error page
        }

        // Run request for every component
        IDictionary<string, string> viewRequest = new Dictionary<string, string>()
        {
            ["table"] = "ViewAnalytics"
        };
        // IDictionary<string, string> admissionRequest = new Dictionary<string, string>()
        // {

        // };
        // IDictionary<string, string> communityBoardRequest = new Dictionary<string, string>()
        // {

        // };
        // IDictionary<string, string> eventListRequest = new Dictionary<string, string>()
        // {

        // };

        IEnumerable<IList<IList<IDictionary<string, string>>>> enumerable = new List<IList<IList<IDictionary<string, string>>>>();
        // Combine all results for components to extract data from
        try
        {
            enumerable.Append(((UsageAnalysisDashboardManager)manager).RequestGetAnalytics(viewRequest)!);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        // try
        // {
        //     enumerable.Append(((UsageAnalysisDashboardManager)manager).RequestGetAnalytics(admissionRequest)!);
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine(e.Message);
        // }
        // try
        // {
        //     enumerable.Append(((UsageAnalysisDashboardManager)manager).RequestGetAnalytics(communityBoardRequest)!);
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine(e.Message);
        // }
        // try
        // {
        //     enumerable.Append(((UsageAnalysisDashboardManager)manager).RequestGetAnalytics(eventListRequest)!);
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine(e.Message);
        // }
        // if (enumerable.Count() == 0)
        // {
        //     Console.WriteLine("No data here");
        //     // return "No data here";
        // }

        return enumerable.ToArray();
    }

    [Route("UpdateAnalytics")]
    [HttpPost]
    // [HttpPost(Name = "UpdateAnalytics")]
    public string Post()
    {
        string username = "", password = "";
        bool authNSession = false;
        string[] sessionInfo = { username, password };
        IAnalysisManager manager;
        bool success = false;

        if (authNSession)
        {
            manager = new UsageAnalysisDashboardManager(sessionInfo);
            // What is it that needs updating? 
            // Include RequestUpdateAnalytics in the UsageAnalysisDashboardManager class
            // Timer for auto update every 60 seconds
            ((UsageAnalysisDashboardManager)manager).RequestUpdateAnalytics();
        }

        string successMessage = "Update was successful";
        string failureMessage = "No update was performed";
        return success ? successMessage : failureMessage;
    }
}