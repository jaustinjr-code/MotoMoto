namespace TheNewPanelists.MotoMoto.DataStoreEntities.PersonalizedRecommendations
{
    public class DataStoreRequestPreferences
    {
        public List<Country>? followedCountries { get; set; }
        public List<Make>? followedMakes { get; set; }
        public List<Model>? followedModels { get; set; }
        public bool status { get; set; }
        public List<string>? messages { get; set; }
    }
}
