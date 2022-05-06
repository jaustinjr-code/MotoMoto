namespace TheNewPanelists.MotoMoto.Models
{
    public class LoadFeedRequestModel : IRequestModel
    {
        public object input { get; }
        public LoadFeedRequestModel(IFeedModel requestModel)
        {
            input = requestModel;
        }
    }
}