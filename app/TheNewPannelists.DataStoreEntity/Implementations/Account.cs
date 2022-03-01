using System.Linq;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Text;
using TheNewPanelists.ServiceLayer.Logging;

namespace TheNewPannelists.DataStoreEntity
{
    class Account
    {
        private int _id {get; set;}
        private string _username {get; set;}
        private string _password {set;}
        private string _email {set;}
        private bool _status {set;}
        private bool _eventAccount {get; set;}
    }
}