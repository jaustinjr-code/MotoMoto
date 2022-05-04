namespace TheNewPanelists.MotoMoto.Models.PersonalizedRecommendations
{
    public class PreferenceModel
    {
        public int UserId { get; set; }
        public string? Make { get; set; }
        public string? Country { get; set; }
        public string? model { get; set; }
        public bool? status { get; set; }
        public string? message { get; set; }
    }
}
