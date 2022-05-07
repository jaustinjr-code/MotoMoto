namespace TheNewPanelists.MotoMoto.Models
{
    public class BarChartAnalyticModel : IUsageAnalyticModel
    {
        public string x_axis { get; }
        public string y_axis { get; }
        public object? x_metric { get; }
        public object? y_metric { get; }

        public BarChartAnalyticModel(string x_ax, string y_ax)
        {
            x_axis = x_ax;
            y_axis = y_ax;
        }

        public BarChartAnalyticModel(string x_ax, string y_ax, object x_met, object y_met)
        {
            x_axis = x_ax;
            y_axis = y_ax;
            x_metric = x_met;
            y_metric = y_met;
        }
    }
}