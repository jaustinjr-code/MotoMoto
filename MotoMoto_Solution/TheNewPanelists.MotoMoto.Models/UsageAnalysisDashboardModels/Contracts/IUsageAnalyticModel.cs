namespace TheNewPanelists.MotoMoto.Models
{
    public interface IUsageAnalyticModel
    {
        string x_axis { get; }
        string y_axis { get; }
        object? x_metric { get; }
        object? y_metric { get; }
    }
}