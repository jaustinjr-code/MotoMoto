using TheNewPanelists.DataAccessLayer;
using TheNewPanelists.ServiceLayer.Logging;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace TheNewPanelists.ServiceLayer.EventAccountVerification
{
    class EvntAccntVerifService : IEvntAccntVerifService
    {
        
        private string operation {get; set;}
        private UserManagementDataAccess evntAccntVerifDataAccess;
        //private UserManagementManager EvntAcctVerifManager;
        private Dictionary<string, string> userProfile {get; set;}
        
        public EvntAccntVerifService() {}
        public EvntAccntVerifService(string operation, Dictionary<string, string>  userProfile) {
            this.operation = operation;
            this.userProfile = userProfile;
            this.evntAccntVerifDataAccess = new UserManagementDataAccess();
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
            this.evntAccntVerifDataAccess = new UserManagementDataAccess(query);
            if (this.evntAccntVerifDataAccess.SelectAccount() == false) 
            {
                return false;
            }
            return true;
        }
        
        public bool IsValidRequest()
        {
            bool containsOperation = this.operation.Contains("FIND_RATING") || this.operation.Contains("FIND_REVIEW") || this.operation.Contains("POST_RATING_AND_REVIEW");
            if (containsOperation) {
                return HasValidAttributes();
            }
            return false;
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
            return "SELECT u.rating FROM EventAccount u WHERE u.userId=" + this.userProfile["userId"] + ";";
        }

        private string FindReview()
        {
            return "SELECT u.review FROM EventAccount u WHERE u.userId=" + this.userProfile["userId"] + ";";
        }

        private string CreateRatingAndReview()
        {
            return "INSERT INTO EventAccount (userId, rating, review) VALUES ('" + this.userProfile["userId"] + "', '" + this.userProfile["rating"] + "', '" + this.userProfile["review"] + "');";
        }

        public bool HasValidAttributes()
        {
            bool hasValidAttributes = false;
            string query = this.getQuery();

            switch (this.operation) 
            {
                case "FIND_RATING":
                    hasValidAttributes = query.Contains("SELECT u.rating FROM EventAccount u WHERE u.userId=");
                    break;

                case "FIND_REVIEW":
                    hasValidAttributes = query.Contains("SELECT u.review FROM EventAccount u WHERE u.userId=");
                    break;

                case "POST_RATING_AND_REVIEW":
                    hasValidAttributes = query.Contains("INSERT INTO EventAccount (userId, rating, review)");
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