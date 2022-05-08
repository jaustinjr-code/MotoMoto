using System.Reflection;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class FetchTrendChartManager : IFetchChartManager
    {
        public string analyticType { get; }

        public FetchTrendChartManager(string type)
        {
            analyticType = type.ToLower();
        }

        public IResponseModel IsAnalyticRequestValid(IUsageAnalyticFetchRequestModel request)
        {
            AuthorizationService auth = new AuthorizationService();
            if (auth.CheckAuthorized(request.username))
            {
                try
                {
                    IResponseModel response = ProcessRequest((IUsageAnalyticModel)request.input);
                    if (IsAnalyticResponseValid(response))
                    {
                        foreach (var item in (List<IAxisDetailsEntity>)((IUsageAnalyticEntity)response.output!).metricList)
                        {
                            Console.WriteLine(item.xData);
                            Console.WriteLine(item.yData);
                        }
                        return response;
                    }
                }
                catch (Exception e)
                {
                    return new ExceptionResponseModel(e.Message);
                }
            }
            return new ExceptionResponseModel("Unauthorized Request");
        }

        private IResponseModel ProcessRequest(IUsageAnalyticModel analyticModel)
        {
            // Janky, not extensible at all, use a creational Design Pattern if time permits
            IFetchTrendChartService service;
            //string serviceType = analyticModel.x_axis.ToLower();
            if (analyticType.Equals("login"))
            {
                service = new FetchLoginTrendChartService();
            }
            else if (analyticType.Equals("registration"))
            {
                service = new FetchRegistrationTrendChartService();
            }
            else if (analyticType.Equals("event"))
            {
                service = new FetchEventTrendChartService();
            }
            else
            {
                throw new Exception("Invalid Request");
            }
            IResponseModel response = service.FetchTrendChartMetrics(analyticModel);
            return response;
        }

        private bool IsAnalyticResponseValid(IResponseModel response)
        {
            // Might give an error if output is the exact type instead of the interface type
            // if (response.output != null && response.output is IUsageAnalyticEntity && (response.isComplete))
            if (response.output != null && (response.isComplete))
            {
                Console.WriteLine(response.output.GetType());
                if (typeof(IUsageAnalyticEntity).IsAssignableFrom(response.output.GetType()))
                    return true;
            }
            else if (response.isComplete == false && response.isSuccess == true)
            {
                throw new Exception("Improper Response");
            }
            return false;
        }
    }
}