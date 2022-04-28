using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.Models
{
    public class PartModel
    {
        public int partID { get; set; }
        public string? partName { get; set; }
        public string? rating { get; set; }
        public int ratingCount { get; set; }
        public string? productURL { get; set; }
        public double currentPrice { get; set; }
        public IEnumerable<IPartPriceHistory>? partPrices = new List<IPartPriceHistory>();

        public PartModel ParseVehiclePartEntityToVehiclePartModel(IPartEntity part)
        {
            partID = part.partID;
            partName = part.partName;
            rating = part.rating;
            ratingCount = part.ratingCount;
            productURL = part.productURL;
            currentPrice = part.currentPrice;
            return this;
        }
    }
}
