using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class AuthenticationManager
    {
        private readonly AuthenticationService _authenticationService;

        public AuthenticationManager(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationModel"></param>
        /// <returns></returns>
        public ReturnAuthenticationModel AuthenticateLoginInformation(AuthenticationModel authenticationModel)
        {
            ReturnAuthenticationModel returnAuthenticationModel = new ReturnAuthenticationModel();
            returnAuthenticationModel._authenticationModel = authenticationModel;

            if (!ValidateUsernameInput(authenticationModel))
                return returnAuthenticationModel;

            DataStoreUser dataStoreUser = _authenticationService.RetrieveUserFromDataStoreService(authenticationModel);
            if (dataStoreUser == null)
                return returnAuthenticationModel;

            authenticationModel.Salt = dataStoreUser!._salt;
            GenerateSHA256ValidatePassword(authenticationModel);

            if ((dataStoreUser!._username == authenticationModel.Username) && (dataStoreUser._password == authenticationModel.Password))
            {
                authenticationModel.UserId = dataStoreUser.UserId;
                _authenticationService.GenerateOneTimePassword(authenticationModel);

                returnAuthenticationModel._authenticationModel = authenticationModel;
                return returnAuthenticationModel;
            }
            return returnAuthenticationModel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationModel"></param>
        public bool ValidateGeneratedOTP(AuthenticationModel authenticationModel)
        {
            DateTime dateTime = DateTime.Now;

            if ((authenticationModel.Otp == authenticationModel.OtpEntry) && (dateTime <= authenticationModel.OtpExpireTime))
            {
                authenticationModel.Authenticated = true;
                _authenticationService.DeleteAuthenticatedSessionWithValidOTP(authenticationModel);
                return true;
            }
            else
            {
                authenticationModel.Attempts++;
                authenticationModel.Otp = "";
                authenticationModel.OtpExpireTime = null;
                _authenticationService.UpdateAuthenticatedSessionWithInvalidInput(authenticationModel);
                return false;
            }
            /*
            switch (authenticationModel.Attempts)
            {
                case 5:
                    authenticationModel.AccountStatus = "LOCKED";
                    break;
                default:
                    break;
            }
            */
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationModel"></param>
        /// <returns></returns>
        private bool ValidateUsernameInput(AuthenticationModel authenticationModel)
        {
            Regex lowerCase = new Regex(@"[a-z]");
            Regex num = new Regex(@"[0-9]");
            Regex specialChar = new Regex(@"[.,@!]");

            bool IsValidPattern = lowerCase.IsMatch(authenticationModel!.Username!) && num.IsMatch(authenticationModel!.Username!)
                                && specialChar.IsMatch(authenticationModel!.Username!);
            return IsValidPattern;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationModel"></param>
        /// <returns></returns>
        private bool ValidOneTimePasswordInput(AuthenticationModel authenticationModel)
        {
            Regex lowerCase = new Regex(@"[a-z]");
            Regex upperCase = new Regex(@"[A-Z]");
            Regex num = new Regex(@"[0-9]");
            Regex specialChar = new Regex(@"[.,@!]");
            Regex otpLength = new Regex(@"[a-zA-Z0-9.,@!]{8,}");

            bool IsValidPattern = lowerCase.IsMatch(authenticationModel!.ValidatedOTP!)
                                && upperCase.IsMatch(authenticationModel!.ValidatedOTP!)
                                && num.IsMatch(authenticationModel!.ValidatedOTP!)
                                && num.IsMatch(authenticationModel!.ValidatedOTP!)
                                && otpLength.IsMatch(authenticationModel!.ValidatedOTP!);
            return IsValidPattern;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationModel"></param>
        /// <returns></returns>
        public bool AuthenticateUsernameInformation(AuthenticationModel authenticationModel)
        {
            if (!ValidateUsernameInput(authenticationModel))
                return false;
            DataStoreUser dataStoreUser = _authenticationService.RetrieveUserFromDataStoreService(authenticationModel);
            if (dataStoreUser == null)
                return false;
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationModel"></param>
        private void GenerateSHA256ValidatePassword(AuthenticationModel authenticationModel)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string saltAndPwd = string.Concat(authenticationModel.Password, authenticationModel.Salt);

            using (SHA256 hash = SHA256.Create())
            {
                Encoding encoder = Encoding.UTF8;
                byte[] resultant = hash.ComputeHash(encoder.GetBytes(saltAndPwd));

                foreach (byte b in resultant)
                    stringBuilder.Append(b.ToString("x2"));
            }
            authenticationModel.Password = stringBuilder.ToString();
        }
    }
}