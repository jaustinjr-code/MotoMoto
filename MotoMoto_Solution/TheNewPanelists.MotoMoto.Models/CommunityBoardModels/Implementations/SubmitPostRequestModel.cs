namespace TheNewPanelists.MotoMoto.Models
{
    public class SubmitPostRequestModel : IRequestModel
    {
        public object input { get; }

        public SubmitPostRequestModel(IPostModel requestModel)
        {
            input = requestModel;
        }
    }
}