namespace TheNewPanelists.MotoMoto.Models
{
    public class SubmitCommentRequestModel : IRequestModel
    {
        public object input { get; }

        public SubmitCommentRequestModel(IPostModel requestModel)
        {
            input = requestModel;
        }
    }
}