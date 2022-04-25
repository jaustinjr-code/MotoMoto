namespace TheNewPanelists.MotoMoto.Models
{
    public class PartModel : IPart
    {
        public int partID { get; set; }
        public string? partName { get; set; }
        public string? rating { get; set; }
        public int ratingCount { get; set; }
        public string? productURL { get; set; }
        public double currentPrice { get; set; }
        public List<PartPrice>? partPrices = new List<PartPrice>();
    }
}
