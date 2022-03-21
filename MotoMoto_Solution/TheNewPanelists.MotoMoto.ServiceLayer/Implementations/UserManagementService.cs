using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.DataAccess.Impementations;
using TheNewPanelists.MotoMoto.ServiceLayer.Contracts;
using TheNewPanelists.MotoMoto.Entities;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System.Data;
using System.Data.SqlClient;

namespace TheNewPanelists.MotoMoto.ServiceLayer.Implementations
{
    public class UserManagementService : IUserManagementService
    {
        // Readonly means that the object/variable cannot be defined outside of the
        // constructor
        private readonly UserManagementDataAccess _userManagementDAO;

        public UserManagementService(UserManagementDataAccess userManagementDAO) 
        {
            _userManagementDAO = userManagementDAO;
        }

        public ISet<DataStoreUser> RetrieveAllAccounts(AccountEntity userAccount)
        {
            throw new NotImplementedException();
        }
        public bool CreateAccount(DataStoreUser createdUser)
        {
            var dataStoreUser = new DataStoreUser()
            {
                _userType = createdUser!._userType,
                _username = createdUser!._username,
                _password = createdUser!._password,
                _email = createdUser!._email
            };
            return _userManagementDAO.InsertNewDataStoreAccountEntity(dataStoreUser);
        }
        public bool DeleteAccount(DataStoreUser deletedAccount)
        {
            var dataStoreUser = new DataStoreUser()
            {
                _username = deletedAccount!._username,
                _password = deletedAccount!._password,
                _email = deletedAccount!._email,
            };

            return _userManagementDAO.DeleteAccountEntity(dataStoreUser);
        }
    }
}
