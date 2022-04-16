using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class AuthenticationService
    {
        private readonly AuthenticationDataAccess? _authenticationDAO;

        public AuthenticationService(AuthenticationDataAccess authenticationDAO)
        {
            _authenticationDAO = authenticationDAO;
        }
        public DataStoreUser RetrieveUserFromDataStoreService(AuthenticationModel authenticationModel)
        {
            return _authenticationDAO!.RetrieveDataStoreSpecifiedUserEntity(authenticationModel.Username!);
        }

        public void DeleteAuthenticatedSessionWithValidOTP(AuthenticationModel authenticationModel)
        {
            _authenticationDAO!.RemoveUserFromAuthenticationTable(authenticationModel);
        }

        public void UpdateAuthenticatedSessionWithInvalidInput(AuthenticationModel authenticationModel)
        {
            _authenticationDAO!.UpdateAuthenticationReset(authenticationModel);
        }
        private void SendEmailToAuthorizedUser(AuthenticationModel authenticationModel)
        {
            StringBuilder input = new StringBuilder();

            string email = "projmotomoto@gmail.com";
            string pass = "Tester491!";
            DataStoreUser dataStoreUser = RetrieveUserFromDataStoreService(authenticationModel);
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(email, "MotoMoto");
                mail.To.Add(dataStoreUser!._email!);
                mail.Subject = "One-Time Password";

                mail.Body = @$"
                    <html>
                        <body>
                            <p></p>Hello,</p>
                            <p>Here is the One-Time Password you need to confirm your 
                            identity and access your account.<br>
                            <p>Your One-Time Password is: {authenticationModel.Otp}</p>
                            <p>The One-Time Password will expire in 2 minutes, so please complete this 
                            step as soon as possible.</p><br>
                            <p>Sincerely,<br><br>
                            MotoMoto Customer Care</p>
                        </body>
                    </html>";
                // mail.BodyEncoding = System.Text.Encoding.UTF8;
                // text or html
                mail.IsBodyHtml = true;

                smtpServer.Port = 587;
                smtpServer.Credentials = new System.Net.NetworkCredential(email, pass);
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
                DateTime sentTime = DateTime.Now;
                authenticationModel.OtpExpireTime = sentTime.AddMinutes(2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void GenerateOneTimePassword(AuthenticationModel authentiactionModel)
        {
            Random rand = new Random();
            char[] charArray = new char[9];
            string OTP = "";

            for (int i = 0; i < charArray.Length; i++)
            {
                int num = i < 3 ? num = i : num = rand.Next(0, 3);
                switch (num)
                {
                    case 0:
                        charArray[i] = (char)rand.Next(65, 91);
                        break;
                    case 1:
                        charArray[i] = (char)rand.Next(97, 123);
                        break;
                    default:
                        charArray[i] = (char)rand.Next(48, 58);
                        break;
                }
            }
            for (int i = 0; i < 100; i++)
            {
                int randomNumOne = rand.Next(charArray.Length);
                int randomNumTwo = rand.Next(charArray.Length);
                char temp = charArray[randomNumOne];
                charArray[randomNumOne] = charArray[randomNumTwo];
                charArray[randomNumTwo] = temp;
            }
            foreach (char ch in charArray)
                OTP += ch;
            authentiactionModel.Otp = OTP;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authentiactionModel"></param>
        private void GenerateOneTimePw(AuthenticationModel authentiactionModel)
        {
            Random rand = new Random();
            char[] charArray = new char[9];
            string OTP = "";

            for (int i = 0; i < charArray.Length; i++)
            {
                int num = i < 3 ? num = i : num = rand.Next(0, 3);
                switch (num)
                {
                    case 0:
                        charArray[i] = (char)rand.Next(65, 91);
                        break;
                    case 1:
                        charArray[i] = (char)rand.Next(97, 123);
                        break;
                    default:
                        charArray[i] = (char)rand.Next(48, 58);
                        break;
                }
            }
            for (int i = 0; i < 100; i++)
            {
                int randomNumOne = rand.Next(charArray.Length);
                int randomNumTwo = rand.Next(charArray.Length);
                char temp = charArray[randomNumOne];
                charArray[randomNumOne] = charArray[randomNumTwo];
                charArray[randomNumTwo] = temp;
            }
            foreach (char ch in charArray)
                OTP += ch;
            authentiactionModel.Otp = OTP;
        }
    }
}