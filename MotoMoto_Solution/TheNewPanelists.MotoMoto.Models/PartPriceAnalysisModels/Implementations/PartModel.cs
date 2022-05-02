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
        public double newPrice { get; set; }
        public bool returnValue = true;
        public IEnumerable<IPartPriceHistory>? historicalPrices { get; set; }
        public IEnumerable<DateTime>? historicalDate { get; set; }
        public IEnumerable<double>? histroicalListingPrice { get; set; }
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
        public PartModel ReturnValueInvalidation()
        {
            if (partID < 0 || partName == null)
            {
                returnValue = false;
                return this;
            }
            return this;
        }
        public PartModel ReturnInvalidPriceUpdate()
        {
            if (currentPrice == newPrice || newPrice <= 0)
            {
                returnValue = false;
                return this;
            }
            return this;
        }
    }
}
