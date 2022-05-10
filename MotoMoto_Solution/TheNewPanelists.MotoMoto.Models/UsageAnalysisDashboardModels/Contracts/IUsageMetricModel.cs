namespace TheNewPanelists.MotoMoto.Models
{
    public interface IUsageMetricModel
    {
        string type { get; }
        string? subType { get; }
        string? title { get; }
        int metric { get; }
    }
}