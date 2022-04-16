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

/*
  private string operation {get; set;}
        private Dictionary<string, string> userAccount {get; set;}
        private int userId {get; set;}
        private string username {get; set;}
        private string userEmail {get; set;}
        private string otp {get; set;}
        private DateTime? otpExpireTime {get; set;}  
        private string userOtp {get; set;}
        private int attempts {get; set;}
        private DateTime? sessionEndTime {get; set;}
        private string userIp {get; set;}
        private string accountStatus {get; set;}
        private AuthenticationDataAccess authenticationDataAccess;
        public AuthenticationService() {}
        public AuthenticationService(string operation) 
        {
            this.operation = operation;
            this.userAccount = new Dictionary<string, string> ()
                {
                    {"username", ""},
                    {"password", ""},
                    {"attempts", null},
                    {"accountStatus", null}
                };
            this.userId = -999;
            this.username = userAccount["username"];
            this.userEmail = "";
            this.otp = "";
            this.otpExpireTime = null;
            this.userOtp = "";
            this.attempts = 0;
            this.sessionEndTime = null;
            this.userIp = null;
            this.accountStatus = null;
            this.authenticationDataAccess = new AuthenticationDataAccess();
        }

        public Dictionary<string, string> RequestInput()
        {
            bool validUsername = false;
            bool authenticated = false;
            Dictionary<string, string> userInfo;
            bool repeat = true;

            while (this.attempts < 5 && !authenticated && repeat)
            {
                while (!validUsername || string.IsNullOrEmpty(this.userEmail))
                {
                    Console.WriteLine("Enter the account information to authenticate");

                    Console.Write("Username: ");
                    string username = Console.ReadLine();
                    this.userAccount["username"] = username;

                    Console.Write("Password: ");
                    string password = Console.ReadLine();
                    this.userAccount["password"] = password;

                    validUsername = ValidateInput("username", userAccount["username"]);
                    userInfo = SelectUser("User");

                    if (!validUsername || userInfo.Count == 0)
                    {
                        Console.WriteLine("Invalid username or password provided." +
                            " Try again or contact system administrator.");
                        userAccount["attempts"] = this.attempts.ToString();
                        userAccount["accountStatus"] = this.accountStatus;
                        return userAccount;
                    }
                    else if (validUsername)
                    {
                        Console.WriteLine("Valid user");
                        this.userId = int.Parse(userInfo["userId"]);
                        SelectUser("Authentication");
                        this.userId = int.Parse(userInfo["userId"]);
                        this.userEmail = userInfo["email"];
                        if (string.IsNullOrEmpty(this.otp) && this.attempts < 5)
                        {
                            this.otp = GenerateOTP();
                            if (this.attempts == 0)
                            {
                                UpdateTable(0);
                            }

                            else if (DateTime.Now > this.sessionEndTime && this.attempts < 5)
                            {
                                this.attempts = 0;
                                this.sessionEndTime = null;
                                UpdateTable(5);  // resets attempts & 24 hour timer
                            }
                            SendEmail(this.otp, userEmail);     
                            UpdateTable(1);     // updates the OTP and its expiration time
                        }
                    }
                }

                // while (!authenticated && repeat && this.attempts < 5)
                // {   
                DateTime currentTime;
                Console.Write("Enter received OTP: ");
                userOtp = Console.ReadLine();
                this.userOtp = userOtp;
                currentTime = DateTime.Now;

                // checks if the user entered the correct otp before the otp expires
                // then authenticate the user and delete the user info from
                // Authentication table
                if (this.otp == this.userOtp && currentTime <= this.otpExpireTime)
                {
                    Console.WriteLine("Authentication Successful!");
                    authenticated = true;
                    repeat = false;
                    UpdateTable(3);     // code 3: deletes the authenticated user info from the table
                    userAccount["attempts"] = this.attempts.ToString();
                    userAccount["accountStatus"] = this.accountStatus;
                }
                else if (this.otp == this.userOtp && currentTime > this.otpExpireTime)
                {   
                    Console.WriteLine("Authentication Failed!");
                    Console.WriteLine("Entered expired OTP!");
                    this.attempts++;
                    this.otp = "";
                    this.otpExpireTime = null;
                    // this.userEmail = null;
                    UpdateTable(1);
                    repeat = false;
                    // SelectUser("Authentication");
                    userAccount["attempts"] = this.attempts.ToString();
                    userAccount["accountStatus"] = this.accountStatus;
                }
                else if (this.otp != this.userOtp)
                {
                    Console.WriteLine("Authentication Failed!");
                    Console.WriteLine("Invalid username, password, and/or OTP." +
                            " Retry again or contact system administrator.");
                    this.attempts++;
                    this.otp = "";
                    this.otpExpireTime = null;
                    // this.userEmail = null;
                    UpdateTable(1);
                    // SelectUser("Authentication");
                    userAccount["attempts"] = this.attempts.ToString();
                    userAccount["accountStatus"] = this.accountStatus;
                    repeat = false;
                }
                if (this.attempts == 1)
                {
                    this.sessionEndTime = currentTime.AddDays(1);
                    UpdateTable(4);      // code 4: starts 24 hour timer
                }
            }
            if (this.attempts == 5) 
            {
                Console.WriteLine("You've reached the maximum authentication attempts."
                                + "\nYour account has been disabled for security reasons.");
                UpdateTable(2);
                this.accountStatus = "LOCKED";
                userAccount["attempts"] = this.attempts.ToString();
                userAccount["accountStatus"] = this.accountStatus;
            }
            return userAccount;
        }
>>>>>>> 63a8123d052f53c03e8abbc6a94e614947c8277a:MotoMoto_Solution/TheNewPanelists.MotoMoto.ServiceLayer/Implementations/UserManagementServices/AuthenticationService.cs

        public AuthenticationService(AuthenticationDataAccess authenticationDAO)
        {
            _authenticationDAO = authenticationDAO;
        }
        public DataStoreUser RetrieveUserFromDataStoreService(AuthenticationModel authenticationModel)
        {
            return _authenticationDAO!.RetrieveDataStoreSpecifiedUserEntity(authenticationModel.Username!);
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
    }
 */