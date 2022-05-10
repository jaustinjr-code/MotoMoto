using Xunit;
using System;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataAccess.Registration;
using MySql.Data.MySqlClient;

namespace TheNewPanelists.MotoMoto.UnitTests.RegistrationTests
{
    public class RegistrationDataAccessTests
    {
        //private MySqlConnection myConnection = new MySqlConnection("server=localhost;user=root;database=dev_um;port=3306;password=12345;");
        private RegistrationDataAccess registrationDAO = new RegistrationDataAccess();
        private static string conn_string = "server=localhost;user=root;database=dev_um;port=3306;password=12345;";
        private MySqlConnection connection = new MySqlConnection(conn_string);
        private static string fakeEmail = "fakeEmail@fakeserver.com";
        private static int fakeRegistrationId = -9;
        private int existingRegistrationId = 1;
        private static string existingUserEmail = "ROOT@LOCALHOST";
        private static string existingRegistrationEmail = "motomoto1ca@gmail.com";
        private static bool result;

        [Fact]
        public void Establish_Maria_DB_Connection_ReturnTrue()
        {
            result = registrationDAO.EstablishMariaDBConnection();

            if(result)
                registrationDAO.getConnection().Close();

            Assert.True(result, "EstablishMariaDBConnection() Test Failure");
        }
        
        [Fact]
        public void Query_User_Table_ReturnTrue()
        {
            connection.Open();
            result = registrationDAO.QueryUserTable(existingUserEmail);

            Assert.True(result, "QueryUserTable_True() Test Failure:" + result.ToString());
        }

        [Fact]
        public void Query_User_Table_ReturnFalse()
        {
            connection.Open();
            result = registrationDAO.QueryUserTable(fakeEmail);
            Assert.False(result, "QueryUserTable_False() Test Failure");
        }

        [Fact]
        public void Has_Active_Registration_ReturnTrue()
        {
            connection.Open();
            result = registrationDAO.HasActiveRegistration(existingRegistrationEmail);
            Assert.True(result, "HasActiveRegistration_True() Test Failure:");
        }

        [Fact]
        public void Has_Active_Registration_ReturnFalse()
        {
            connection.Open();
            result = registrationDAO.HasActiveRegistration(fakeEmail);
            Assert.False(result, "HasActiveRegistration_False() Test Failure");
        }

        [Fact]
        public void Update_Registration_To_Valid_ReturnTrue()
        {
            connection.Open();
            result = registrationDAO.UpdateRegistrationToValid(existingRegistrationId, true);
            Assert.True(result, "UpdateRegistrationToValid_True() Test Failure");
        }

        [Fact]
        public void Update_Registration_To_Valid_ReturnFalse()
        {
            connection.Open();
            result = registrationDAO.UpdateRegistrationToValid(fakeRegistrationId, true);
            Assert.False(result, "UpdateRegistrationToValid_False() Test Failure");
        }

        [Fact]
        public void Return_Registration_Id_ReturnTrue()
        {
            connection.Open();
            int expected = 1;
            int result = registrationDAO.ReturnRegistrationId(existingRegistrationEmail);
            Assert.True((expected == result), "ReturnRegistrationId_True() Test Failure");
        }

        [Fact]
        public void Return_Registration_Id_ReturnFalse()
        {
            connection.Open();
            int id = registrationDAO.ReturnRegistrationId(existingRegistrationEmail);
            Assert.False((id != 1) , "ReturnRegistrationId_False() Test Failure:" + id);
        }

        [Fact]
        public void Confirm_Registration_ReturnTrue()
        {
            connection.Open();
            RegistrationRequestModel model = new RegistrationRequestModel() {
                Email = existingRegistrationEmail,
                RegistrationId = existingRegistrationId
            };

            result = registrationDAO.ConfirmRegistration(ref model);
            Assert.True(result, "ConfirmRegistration_True() Test Failure");
        }

        [Fact]
        public void Confirm_Registration_ReturnFalse()
        {
            connection.Open();
            RegistrationRequestModel model = new RegistrationRequestModel() {
                Email = fakeEmail,
                RegistrationId = fakeRegistrationId
            };

            result = registrationDAO.ConfirmRegistration(ref model);
            Assert.False(result, "ConfirmRegistration_False() Test Failure");
        }

        [Fact]
        public void Insert_Registration_Entry_ReturnTrue()
        {
            connection.Open();
            RegistrationRequestModel model = new RegistrationRequestModel() {
                Email = "newuser@email.com",
                Password = "password1"
            };

            result = registrationDAO.InsertRegistrationEntry(ref model, true);
            Assert.True(result, "InsertRegistrationEntry_True() Test Failure");
        }

        [Fact]
        public void Insert_Registration_Entry_ReturnFalse()
        {
            connection.Open();
            RegistrationRequestModel model = new RegistrationRequestModel() {
                Email = "newuser@email.com",
                Password = "short"
            };

            result = registrationDAO.InsertRegistrationEntry(ref model, true);
            Assert.False(result, "InsertRegistrationEntry_False() Test Failure");
        }

        [Fact]
        public void Delete_Active_Registration_ReturnTrue()
        {
            connection.Open();
            result = registrationDAO.DeleteActiveRegistration(existingRegistrationId, true);
            Assert.True(result, "DeleteActiveRegistration_True() Test Failure");
        }

        [Fact]
        public void Delete_Active_Registration_ReturnFalse()
        {
            connection.Open();
            result = registrationDAO.DeleteActiveRegistration(fakeRegistrationId, true);
            Assert.False(result, "DeleteActiveRegistration_False() Test Failure");
        }
    }
}