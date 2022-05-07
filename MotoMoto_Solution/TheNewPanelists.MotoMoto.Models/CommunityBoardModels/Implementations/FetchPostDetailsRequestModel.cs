namespace TheNewPanelists.MotoMoto.Models
{
    public class FetchPostDetailsRequestModel : IRequestModel
    {
        public object input { get; }
        public FetchPostDetailsRequestModel(int id)
        {
            input = id;
        }
    }
}