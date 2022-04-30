namespace TheNewPanelists.MotoMoto.Models
{
    public interface IInteractionModel
    {
        int contentId { get; set; }
        string? contentTitle { get; set; }
        string interactUsername { get; set; }
    }
}