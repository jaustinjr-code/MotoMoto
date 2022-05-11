using System;
using System.Collections.Generic;
using TheNewPanelists.MotoMoto.DataAccess.Implementations.PersonalizedRecommendations;
using TheNewPanelists.MotoMoto.DataStoreEntities.PersonalizedRecommendations;

namespace TheNewPanelists.MotoMoto.ServiceLayer.Implementations
{
    public class PersonalizedRecommendationsService
    {
        private readonly PersonalizedRecommendationsDataAccess _personalizedRecommendationsDAO;

        public PersonalizedRecommendationsService()
        {
            _personalizedRecommendationsDAO = new PersonalizedRecommendationsDataAccess();
        }

        public DataStoreRequestPreferences GetUserPreferences(int userId)
        {
            DataStoreRequestPreferences dataStoreRequest = new DataStoreRequestPreferences() {
                followedCountries = new List<Country>(),
                followedMakes = new List<Make>(),
                followedModels = new List<Model>(),
                status = false,
                messages = new List<string>()
            };

            try
            {
                _personalizedRecommendationsDAO.ReturnCountriesFollowed(userId, ref dataStoreRequest);
                _personalizedRecommendationsDAO.ReturnMakesFollowed(userId, ref dataStoreRequest);
                _personalizedRecommendationsDAO.ReturnModelsFollowed(userId, ref dataStoreRequest);
                dataStoreRequest.status = true;
                return dataStoreRequest;
            }
            catch (Exception e)
            {
                dataStoreRequest.messages!.Add("ErrorType: " + e.GetType() + "\nErrorMessage: " + e.Message);
                return dataStoreRequest;
            }
        }

        public void UpdateUserPreferences(int userId, ref DataStoreRequestPreferences dataStoreRequest)
        {
            List<string> messages = new List<string>();

            if (_personalizedRecommendationsDAO.RemoveCountryPreferences(userId, ref messages))
            {
                List<Country> countries = dataStoreRequest.followedCountries!;

                if (_personalizedRecommendationsDAO.AddCountryPreferences(userId, ref countries, ref messages))
                    messages.Add("Success: Country preferences updated.");
            }
            if (_personalizedRecommendationsDAO.RemoveMakePreferences(userId, ref messages))
            {
                List<Make> makes = dataStoreRequest.followedMakes!;

                if (_personalizedRecommendationsDAO.AddMakePreferences(userId, ref makes, ref messages))
                    messages.Add("Success: Make preferences updated.");
            }
            if (_personalizedRecommendationsDAO.RemoveModelPreferences(userId, ref messages))
            {
                List<Model> models = dataStoreRequest.followedModels!;

                if (_personalizedRecommendationsDAO.AddModelPreferences(userId, ref models, ref messages))
                    messages.Add("Success: Model preferences updated.");
            }
            dataStoreRequest.messages = messages;
        }
    }
}
