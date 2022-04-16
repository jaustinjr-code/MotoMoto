/*
using TheNewPanelists.DataAccessLayer;
using TheNewPanelists.ServiceLayer.Logging;
using TheNewPanelists.BusinessLayer;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace TheNewPanelists.ServiceLayer.Authentication
{
    class AuthenticationService : IAuthenticationService
    {

        private readonly AuthenticationDataAccess authenticationDataAccess;
        public AuthenticationService() { }
        public AuthenticationService(AuthenticationDataAccess)
        {
            authenticationDataAccess
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

        private void UpdateTable(int code)
        {
            // code: 
            //  0: checks wheather the user is already in the Authentication table or not
            //  1: updates number of attempts, otp, otpExpireTime
            //  2: updates accountStatus
            //  3: delete user from the authentication table once the user successfully authenticated the account
            //  4: starts 24 hour timer
            //  5: resets 24 hour timer

            string query = "";

            if (code == 0)
            {
                query = $@"SELECT userId, username FROM Authentication
                        WHERE username = '{this.userAccount["username"]}';";

                this.authenticationDataAccess = new AuthenticationDataAccess(query);

                if (authenticationDataAccess.SelectUser().Count == 0)
                {
                    query = $@"INSERT INTO AUTHENTICATION (userId, username, attempts)
                                    VALUES ((SELECT userId
                                    FROM User
                                    WHERE userId = {this.userId}),
                                    (SELECT username
                                    FROM User
                                    WHERE userId = {this.userId}), '{this.attempts}');";
                    this.authenticationDataAccess = new AuthenticationDataAccess(query);
                    if (authenticationDataAccess.UpdateAuthenticationTable())
                    {
                        Console.WriteLine("Inserted successessfully!");
                    }
                    else
                    {
                        Console.WriteLine("Insertion failed!!");
                    }
                }
            }
            else if (code == 1)
            {
                query = $@"UPDATE AUTHENTICATION
                            SET attempts = '{this.attempts}', otp = '{this.otp}',
                            otpExpireTime = '{this.otpExpireTime}'
                            WHERE userId = {this.userId};";
                this.authenticationDataAccess = new AuthenticationDataAccess(query);
                authenticationDataAccess.UpdateAuthenticationTable();
            }
            else if (code == 2)     // locks user account when attempts reaches 5
            {
                query = $@"UPDATE AUTHENTICATION
                            SET accountStatus = '{this.accountStatus}'
                            WHERE userId = {this.userId};";
                this.authenticationDataAccess = new AuthenticationDataAccess(query);
                authenticationDataAccess.UpdateAuthenticationTable();
            }
            else if (code == 3)
            {
                query = $@"DELETE FROM AUTHENTICATION
                            WHERE userId = {this.userId};";
                this.authenticationDataAccess = new AuthenticationDataAccess(query);
                authenticationDataAccess.UpdateAuthenticationTable();
            }
            else if (code == 4)
            {
                query = $@"UPDATE AUTHENTICATION
                            SET sessionEndTime = '{this.sessionEndTime}'
                            WHERE userId = {this.userId};";
                this.authenticationDataAccess = new AuthenticationDataAccess(query);
                authenticationDataAccess.UpdateAuthenticationTable();
            }
            else if (code == 5)
            {
                query = $@"UPDATE AUTHENTICATION
                            SET attempts = {this.attempts}, sessionEndTime = '{this.sessionEndTime}'
                            WHERE userId = {this.userId};";
                this.authenticationDataAccess = new AuthenticationDataAccess(query);
                authenticationDataAccess.UpdateAuthenticationTable();
            }
        }

        private Dictionary<string, string> SelectUser(string tableName)
        {
            Dictionary<string, string> userInfo = new Dictionary<string, string>();
            string query = "";

            // checks if the user is in the User table with the given username & password
            if (tableName == "User")
            {
                query = $@"SELECT userId, email FROM {tableName}
                        WHERE username = '{this.userAccount["username"]}'
                        AND password = '{this.userAccount["password"]}';";

                this.authenticationDataAccess = new AuthenticationDataAccess(query);
                userInfo = this.authenticationDataAccess.SelectUser();
            }

            // checks if the user is already in the Authentication table
            // if so, sets otp, otpExpiretime, attempts, userIp...
            else if (tableName == "Authentication")
            {
                query = $@"SELECT * FROM {tableName}
                        WHERE userId = {this.userId};";

                this.authenticationDataAccess = new AuthenticationDataAccess(query);
                userInfo = this.authenticationDataAccess.SelectUser();
                if (userInfo.Count != 0)
                {
                    this.otp = !string.IsNullOrEmpty(userInfo["otp"]) ? userInfo["otp"] : null;
                    this.otpExpireTime = !string.IsNullOrEmpty(userInfo["otpExpireTime"]) ?
                                        DateTime.Parse(userInfo["otpExpireTime"]) : null;
                    this.attempts = int.Parse(userInfo["attempts"]);
                    this.sessionEndTime = DateTime.Parse(userInfo["sessionEndTime"]);
                    this.userIp = userInfo["userIp"];
                }

            }
            return userInfo;
        }

        private bool ValidateInput(string type, string typeValue)
        {
            Regex lowerCase = new Regex(@"[a-z]");
            Regex upperCase = new Regex(@"[A-Z]");
            Regex num = new Regex(@"[0-9]");
            Regex specialChar = new Regex(@"[.,@!]");
            Regex otpLength = new Regex(@"[a-zA-Z0-9.,@!]{8,}");

            bool IsValidPattern;

            switch (type)
            {
                case "username":
                    IsValidPattern = lowerCase.IsMatch(typeValue) && num.IsMatch(typeValue)
                        && specialChar.IsMatch(typeValue);
                    if (!IsValidPattern)
                    {
                        Console.WriteLine("failed username test");
                        return false;
                    }
                    Console.WriteLine("passed username test");
                    return true;

                case "otp":
                    IsValidPattern = lowerCase.IsMatch(typeValue) && upperCase.IsMatch(typeValue)
                        && num.IsMatch(typeValue)
                        && num.IsMatch(typeValue) && otpLength.IsMatch(typeValue);
                    if (!IsValidPattern)
                    {
                        return false;
                    }
                    return true;

                default:
                    return false;
            }
        }

        private void SendEmail(string otp, string userEmail)
        {
            StringBuilder input = new StringBuilder();

            string email = "projmotomoto@gmail.com";
            string pass = "Tester491!";
            // Console.WriteLine($"Enter password for {email}:");
            // while (true)
            // {
            //     var key = Console.ReadKey(true);
            //     if (key.Key == ConsoleKey.Enter) break;
            //     if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
            //     else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
            // }
            // string pass = input.ToString();

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(email, "MotoMoto");
                mail.To.Add(userEmail);
                // mail.To.Add("jacob.sunia@student.csulb.edu");
                mail.Subject = "One-Time Password";

                mail.Body = @$"
                    <html>
                        <body>
                            <p></p>Hello,</p>
                            <p>Here is the One-Time Password you need to confirm your 
                            identity and access your account.<br>
                            <p>Your One-Time Password is: {otp}</p>
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
                this.otpExpireTime = sentTime.AddMinutes(2);
                Console.WriteLine("sent time: " + sentTime);
                Console.WriteLine("expire time: " + otpExpireTime);
                Console.WriteLine("An One-Time Password has been sent to your email.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private string GenerateOTP()
        {
            // A - Z: ASCII 65 - 90 rand.Next(65, 91)
            // a - z: ASCII 97 - 122 rand.Next(97, 123)
            // 0 - 9: ASCII 48 - 57 rand.Next(48, 58)
            Random rand = new Random();
            char[] chArr = new char[9];
            string otp = "";

            for (int i = 0; i < chArr.Length; i++)
            {
                int num = i < 3 ? num = i : num = rand.Next(0, 3);

                if (num == 0)
                {
                    chArr[i] = (char)rand.Next(65, 91);     // upper case
                }

                else if (num == 1)
                {
                    chArr[i] = (char)rand.Next(97, 123);   // lower case
                }

                else if (num == 2)
                {
                    chArr[i] = (char)rand.Next(48, 58);    // number 0 - 9
                }
            }

            Console.Write("before shuffle: ");
            foreach (char ch in chArr)
            {
                Console.Write(ch);
            }

            for (int i = 0; i < 100; i++)
            {
                int randNum1 = rand.Next(chArr.Length);
                int randNum2 = rand.Next(chArr.Length);
                char temp = chArr[randNum1];
                chArr[randNum1] = chArr[randNum2];
                chArr[randNum2] = temp;
            }

            foreach (char ch in chArr)
            {
                otp += ch;
            }
            Console.WriteLine("\notp: " + otp);
            return otp;
        }
    }
}*/