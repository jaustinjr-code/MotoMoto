namespace TheNewPanelists.MotoMoto.Models
{
    public class PartModel : IPart
    {
        public string? _partID { get; set; }
        public string? _partName { get; set; }
        public List<PartDatePrice>? PartDatePrices { get; set; }
    }
}
