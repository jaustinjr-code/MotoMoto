namespace TheNewPanelists.MotoMoto.Models
{
    public class PartModel : IPart
    {
        public string? PartID { get; set; }
        public string? PartName { get; set; }
        public List<PartDatePrice>? PartDatePrices { get; set; }
    }
}
