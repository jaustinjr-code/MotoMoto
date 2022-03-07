using TheNewPanelists.DataAccessLayer;
using TheNewPanelists.ServiceLayer.Logging;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace TheNewPanelists.ServiceLayer.EventAccountVerification
{
    class EvntAccntVerifService : IEvntAccntVerifService
    {
        
        private string? operation {get; set;}
        private EvntAccntVerifDataAccess? evntAccntVerifDataAccess;
        //private UserManagementManager EvntAcctVerifManager;
        private Dictionary<string, string>? userProfile {get; set;}
        
        public EvntAccntVerifService() 
        {
            operation = null;
            evntAccntVerifDataAccess = null;
            userProfile = null;
        }
        public EvntAccntVerifService(string operation, Dictionary<string, string>  userProfile) {
            this.operation = operation;
            this.userProfile = userProfile;
            this.evntAccntVerifDataAccess = new EvntAccntVerifDataAccess();
            //this.evntAccntVerifManager = new UserManagementManager;
        }

        public bool SqlGenerator()
        {   
            string query = "";
            if (this.operation == "FIND_RATING")
            {
                query = this.FindRating();
            }
            else if (this.operation == "FIND_REVIEW")
            {
                query = this.FindReview();
            }
            else if (this.operation == "POST_RATING_AND_REVIEW")
            {
                query = this.CreateRatingAndReview();
            }
            // else if (this.operation == "DROP")
            // {
            //     query = this.DropUser();
            // }
            // else if (this.operation == "UPDATE")
            // {
            //     query = this.UpdateOptions();
            // } 
            // else if (this.operation == "ACCOUNT RECOVERY")
            // {
            //     query = this.AccountRecovery();
            // }
            this.evntAccntVerifDataAccess = new EvntAccntVerifDataAccess(query);
            if ( (query.Contains("SELECT u.rating FROM EventAccount u WHERE u.username=") || query.Contains("SELECT u.review FROM EventAccount u WHERE u.username=") ) && this.evntAccntVerifDataAccess.FindRatingOrAccount() == false) 
            {
                return false;
            }
            else if (query.Contains("INSERT INTO EventAccount (username, rating, review)") && this.evntAccntVerifDataAccess.PostRatingAndReview() == false)
            {
                return false;
            } 
            else
            {
                return true;
            }
        }
        
        public bool IsValidRequest()
        {
            if (this.operation != null) 
            {
                bool containsOperation = this.operation.Contains("FIND_RATING") || this.operation.Contains("FIND_REVIEW") || this.operation.Contains("POST_RATING_AND_REVIEW");
                if (containsOperation)
                {
                    return HasValidAttributes();
                }
                return false;
            }
            else
            {
                return false;
            }

        }

        public string getQuery()
        {
            string query = "";
            switch (this.operation) 
            {
                case "FIND_RATING":
                    query = this.FindRating();
                    break;

                case "FIND_REVIEW":
                    query = this.FindReview();
                    break;

                case "POST_RATING_AND_REVIEW":
                    query = this.CreateRatingAndReview();
                    break;

                // case "DROP":
                //     query = this.DropUser();
                //     break;

                // case "UPDATE":
                //     query = this.UpdateOptions();
                //     break;
                // case "ACCOUNT RECOVERY":
                //     query = this.AccountRecovery();
                //     break;
            }
            return query;
        }

        private string FindRating()
        {
            if (this.userProfile != null)
            {
                return "SELECT u.rating FROM EventAccount u WHERE u.username='" + this.userProfile["username"] + "';";
            }
            else { return "False"; }
        }

        private string FindReview()
        {
            if (this.userProfile != null)
            {
                return "SELECT u.review FROM EventAccount u WHERE u.username='" + this.userProfile["username"] + "';";
            }
            else { return "False"; }
        }

        private string CreateRatingAndReview()
        {
            if (this.userProfile != null)
            {
                return "INSERT INTO EventAccount (username, rating, review) VALUES ('" + this.userProfile["username"] + "', '" + this.userProfile["rating"] + "', '" + this.userProfile["review"] + "');";
            }
            else { return "False"; }
        }

        public bool HasValidAttributes()
        {
            bool hasValidAttributes = false;
            string query = this.getQuery();

            switch (this.operation) 
            {
                case "FIND_RATING":
                    hasValidAttributes = query.Contains("SELECT u.rating FROM EventAccount u WHERE u.username=");
                    break;

                case "FIND_REVIEW":
                    hasValidAttributes = query.Contains("SELECT u.review FROM EventAccount u WHERE u.username=");
                    break;

                case "POST_RATING_AND_REVIEW":
                    hasValidAttributes = query.Contains("INSERT INTO EventAccount (username, rating, review)");
                    break;

                // case "DROP":
                //     hasValidAttributes = query.Contains("DELETE u FROM USER u WHERE u.username = ") 
                //                         && query.Contains("AND u.password =");
                //     break;
                // case "UPDATE":
                //     hasValidAttributes = (query.Contains("UPDATE USER u SET") && (query.Contains("u.username")
                //                         || query.Contains("password") || query.Contains("email")));
                //     break;
                // case "ACCOUNT RECOVERY":
                //     //hasValidAttributes = query.Contains();
                //     break;
            }
            return hasValidAttributes;
        }

    }
}