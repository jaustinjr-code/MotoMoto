using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.App.DataStoreEntities
{
    internal class DataStoreUserProfile : IBaseUser
    {
        /// <summary>
        /// username and userId are connected by foreign keys to the users table. 
        /// Where profile is dependent on the object of User
        /// </summary>
        public int _userId { get; set; }
        public string? _username { get; set; }

        public bool _status { get; set; }
        public bool _eventAccount { get; set; }
    }
}
