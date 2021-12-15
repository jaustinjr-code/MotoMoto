namespace TheNewPanelists.ServiceLayer.UserManagement {
    class UserManagementService : IServiceLayer {

        //private Dictionary<string, string> userAccount;
        private IBusinessLayer operation;
        private IDataAccess userAccount;
        
        public UserManagementService()
        {
        }
        public UserManagementService(IBusinessLayer operation, IDataAccess userAccount) {
            this.userAccount = userAccount;
            this.operation = operation;
        }
        // protected bool ValidateAccount(string accountToValidate) {
        //     if(accountToValidate) { //if username already exists in database
        //         return true;
        //     } else if (accountToValidate != ) { //if username does not exist in database
        //         return false;
        //     }
        // }
        public bool CreateAccount() {
            if(userAccount.ValidateAccount == true) { //if account exists in database
                return false; //can not create new account
            } else {
                try {
                    userAccount.CreateAccount();
                } catch {
                    return false;
                }
                return true;
            }
        }
        public bool DeleteAccount() {
            if(userAccount.ValidateAccount == true) {
                try {
                    userAccount.DeleteAccount();
                } catch {
                    return false;
                }
                //trigger database to delete account
                return true;
            } else {
                //account already does not exist
                return false;
            }
        }
        public bool UpdateAccount() {
            if(userAccount.ValidateAccount == true) {
                try {
                    userAccount.UpdateAccount();
                } catch {
                    return false;
                }
                return true;
            } else {
                return false;
            }
        }
        public bool EnableAccount() {
            if(userAccount.ValidateAccount == true) {
                try {
                    userAccount.EnableAccount();
                } catch {
                    return false;
                }
                return true;
            } else {
                return false;
            }
        }
        public bool DisableAccount() {
            if(userAccount.ValidateAccount == true) {
                try {
                    userAccount.DisableAccount();
                } catch {
                    return false;
                }
                return true;
            } else {
                return false;
            }
        }
    }
}