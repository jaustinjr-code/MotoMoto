using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TheNewPanelists.ServiceLayer.EventAccountVerification
{
    public class EvntAccntVerifServiceTest
    {
        private string? operation;
        private bool result;
        Stopwatch timer = new Stopwatch();

        [Fact]
        public void DisplayRating_OfDesiredUser_ReturnTrue() 
        {
            Dictionary<string, string> userProfile = new Dictionary<string, string>();

            operation = "FIND_RATING";
            userProfile.Add("username", "justin@test");
            userProfile.Add("rating", "5");

            EvntAccntVerifService service = new EvntAccntVerifService(operation, userProfile);

            result = service.IsValidRequest();
            Assert.True(result, "Valid Result For Valid Find Rating Input");

        }

        [Fact]
        public void DisplayReview_OfDesiredUser_ReturnTrue()
        {
            Dictionary<string, string> userProfile = new Dictionary<string, string>();

            operation = "FIND_REVIEW";
            userProfile.Add("username", "justin@test");
            userProfile.Add("review", "Trusted event account holder.");

            EvntAccntVerifService service = new EvntAccntVerifService(operation, userProfile);

            result = service.IsValidRequest();
            Assert.True(result, "Valid Result For Valid Find Review Input");

        }

        [Fact]
        public void PostRatingAndReview_Within3Seconds_ReturnTrue()
        {
            Dictionary<string, string> userProfile = new Dictionary<string, string>();

            operation = "POST_RATING_AND_REVIEW";
            userProfile.Add("username", "justin@test");
            userProfile.Add("rating", "5");
            userProfile.Add("review", "Trusted event account holder.");

            timer.Start();
            EvntAccntVerifService service = new EvntAccntVerifService(operation, userProfile);
            service.IsValidRequest();
            timer.Stop();

            result = timer.Elapsed.TotalSeconds <= 3;

            Assert.True(result, "Valid Result For Valid Posting Rating and Review Input");

        }

    }
}
