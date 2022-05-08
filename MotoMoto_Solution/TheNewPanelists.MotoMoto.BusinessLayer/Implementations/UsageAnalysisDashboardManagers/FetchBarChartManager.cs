using System.Reflection;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class FetchBarChartManager : IFetchChartManager
    {
        public string analyticType { get; }

        public FetchBarChartManager(string type)
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
                        //foreach (var item in (List<IAxisDetailsEntity>)((IUsageAnalyticEntity)response.output!).metricList)
                        //{
                        //Console.WriteLine(item.xData);
                        //Console.WriteLine(item.yData);
                        //}
                        return response;
                    }
                    throw new Exception("Invalid Response");
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e.Message);
                    return new ExceptionResponseModel(e.Message);
                }
            }
            return new ExceptionResponseModel("Unauthorized Request");
        }

        private IResponseModel ProcessRequest(IUsageAnalyticModel analyticModel)
        {
            // Janky, not extensible at all, use a creational Design Pattern if time permits
            IFetchBarChartService service;
            //string serviceType = analyticModel.x_axis.ToLower();
            if (analyticType.Equals("view"))
            {
                service = new FetchViewBarChartService();
            }
            else if (analyticType.Equals("feed"))
            {
                service = new FetchFeedBarChartService();
            }
            else
            {
                throw new Exception("Invalid Request");
            }
            IResponseModel response = service.FetchBarChartMetrics(analyticModel);
            return response;
        }

        private bool IsAnalyticResponseValid(IResponseModel response)
        {
            // Console.WriteLine(response)
            if (response.output != null && (response.isComplete))
            {
                //Console.WriteLine(response.output.GetType());
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