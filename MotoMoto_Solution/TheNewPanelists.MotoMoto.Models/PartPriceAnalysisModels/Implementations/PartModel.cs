namespace TheNewPanelists.MotoMoto.Models
{
    public class PartModel : IPart
    {
        public int _partID { get; set; }
        public string? _partName { get; set; }
        public string? _rating { get; set; }
        public int _ratingCount { get; set; }
        public string? _productURL { get; set; }
        public double _currentPrice { get; set; }
        public List<PartPrice>? _partPrices = new List<PartPrice>();
    }
}
