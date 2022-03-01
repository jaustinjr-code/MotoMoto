using System.Linq;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Text;
using TheNewPanelists.ServiceLayer.Logging;

namespace TheNewPannelists.DataStoreEntity
{
    class Profile
    {
        private string _typeName {get; set;} 
        private int _id {get; set;}
        private string _username {get; set;}
        private bool _status {set;}
        private bool _eventAccount {get; set;}
    }
}