namespace TheNewPanelists.ApplicationLayer 
{
    interface IAuthorization
    {
        /**
        * Set the credentials used to obtain authorization level of user from the database
        */
        public void setCredentials();

        /**
        * Returns the auth type set during construction of the authoriztation
        */
        public string getAuthType();

        /**
        * Checks for a given operation if the set user is authorized to call the operation
        */
        public bool checkAuthorized(string operation);
    }
}