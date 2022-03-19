using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataAccess.Impementations;
using TheNewPanelists.MotoMoto.ServiceLayer.Contracts;
using TheNewPanelists.MotoMoto.Entities;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System.Data.SqlClient;
using System.Data;

namespace TheNewPanelists.MotoMoto.ServiceLayer.Implementations
{
    public class ProfileManagementService : IUserManagementService
    {
        private bool accountRecoveryFlag = false;
        private string? _operation { get; set; }
        private UserManagementDataAccess? userManagementDataAccess;
        private DataStoreUserProfile? _userProfile { get; set; }
        public bool SqlGenerator()
        {
            throw new NotImplementedException();
        }

        public ProfileEntity FindProfileOperation()
        {
            throw new NotImplementedException();
        }

        public bool CreateProfileOperation()
        {
            throw new NotImplementedException();
        }

        public bool DeleteProfileOperation()
        {
            throw new NotImplementedException();
        }

        public bool UserNamePasswordDSValidation()
        {
            throw new NotImplementedException();
        }

        public bool UpdateProfileOptionsOperation(string operation, string newValue)
        {
            throw new NotImplementedException();
        }
    }
}
