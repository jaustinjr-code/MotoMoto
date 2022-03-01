using System.Linq;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Text;
using TheNewPanelists.ServiceLayer.Logging;

namespace TheNewPannelists.Account
{
    class Account
    {
        private int id;
        private string _username;
        private string _password; 
        private string _email;
        
        private bool _status; 
        private bool _eventAccount;

        public Account() {}

        public Account(int id, string username, string password, string email, bool status, bool eventAccount)
        {
            this._id = id;
            this._username = username;
            this._password = password;
            this._email = email;
            this._status = status;
            this._eventAccount = false;
        }

        private int GetAccountID()
        {
            return this._id;
        }

        private string GetUsername()
        {
            return this._username;
        } 

        private bool GetStatus()
        {
            return this._status;
        }

        private int EnableEventAccount(bool EventVerificationflag)
        {
            //enter event account verification code here
            //pseudo code
            //if event account flag is verified
            //then activate event account permissions
            //otherwise return original false flag
            if (EventVerificationFlag) 
            {
                this._eventAccount = true;
                return this._id; 
                //run new query that updates event account status on UID
            }   
            this._eventAccount = false;
            return -1; //non valid user within account directory and forces exit
            // need to resolve this solution
        }

        private string updatePassword(string OTP, string UserEntry)
        {
            //if UserEntry is not the same as the OTP
            //invalid updatePassword request
            //otherwise if the userEntry is the same as the OTP,
            //then we will update the pasword and make a a call request to the application user management
            return "";
        }
    }
}