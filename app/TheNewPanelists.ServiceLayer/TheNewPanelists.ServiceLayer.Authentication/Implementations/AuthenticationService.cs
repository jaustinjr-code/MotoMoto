using TheNewPanelists.DataAccessLayer;
using TheNewPanelists.ServiceLayer.Logging;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace TheNewPanelists.ServiceLayer.Authentication
{
    class AuthenticationService : IAuthenticationService
    {
        private string operation {get; set;}
        private UserManagementDataAccess userManagementDataAccess;
        private Dictionary<string, string> userAccount {get; set;}
        
        public AuthenticationService() {}
        
        public AuthenticationService(string operation, Dictionary<string, string> userAccount) 
        {
            this.operation = operation;
            this.userAccount = userAccount;
            this.userManagementDataAccess = new UserManagementDataAccess();
        }

        public bool validateRequest()
        {
            Regex letter = new Regex(@"[a-zA-Z]");
            Regex num = new Regex(@"[0-9]");
            Regex specialChar = new Regex(@"[.,@!]");
            Regex length = new Regex(@"[a-zA-Z0-9.,@!]{8,}");

            foreach(KeyValuePair<string, string> entry in userAccount){
                if (string.IsNullOrEmpty(entry.Value))
                {
                    return false;
                }
                else if (entry.Key == "username")
                {
                    bool IsValidPattern = letter.IsMatch(entry.Value) && num.IsMatch(entry.Value) 
                        && specialChar.IsMatch(entry.Value) && length.IsMatch(entry.Value);
                    if (!IsValidPattern)
                    {
                        Console.WriteLine("failed username test");
                        return false;
                    }
                    Console.WriteLine("passed username test");
                }
                else if (entry.Key == "password")
                {

                }
                else if (entry.Key == "otp")
                {
                    bool IsValidPattern = letter.IsMatch(entry.Value) && num.IsMatch(entry.Value) 
                        && specialChar.IsMatch(entry.Value) && length.IsMatch(entry.Value);
                    if (!IsValidPattern)
                    {
                        Console.WriteLine("failed OTP test");
                        return false;
                    }
                    Console.WriteLine("passed OTP test");
                }
            }
            return true;
        }
        
        public bool SqlGenerator()
        {   
            Dictionary<string, string> informationLog = new Dictionary<string, string>();
            string query = "";
        
            query = this.FindUser();
            

            this.userManagementDataAccess = new UserManagementDataAccess(query);
            if (this.userManagementDataAccess.SelectAccount() == false) 
            {
                return false;
            }
            informationLog.Add("categoryname", "DATA STORE");
            informationLog.Add("levelname", "INFO");
            informationLog.Add("description","Account Selection COMPLETION, Information in CRUD Operation Queries Executed.");
            ILogService loggingSuccess = new LogService("CREATE", informationLog, true);
            loggingSuccess.SqlGenerator();
            return true;
        }

        private string FindUser()
        {
            return "SELECT u.usernameFROM User u WHERE u.username =" + this.userAccount["username"] + ";";
        }

        public void SendEmail(string otp)
        {   
            StringBuilder input = new StringBuilder();

            // Console.WriteLine("Enter emai:");
            // string email = Console.ReadLine();
            string email = "projmotomoto@gmail.com";
            Console.WriteLine("Enter password:");
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
                else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
            }
            string pass = input.ToString();

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(email, "MotoMoto");
                mail.To.Add("naeun.yu@student.csulb.edu");
                mail.To.Add("jacob.sunia@student.csulb.edu");
                mail.Subject = "Verification Code";
                mail.Body = "This is for testing SMTP mail from GMAIL";

                mail.Body = @$"
                    <html>
                        <body>
                            <p></p>Hello,</p>
                            <p>Here is the Verification Code you need to confirm your 
                            identity and access your account.<br>
                            <p>Your Verification Code is: {otp}</p>
                            <p>The code will expire in 2 minutes, so please complete this 
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
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public string CreateOTP()
        {
            // A - Z: ASCII 65 - 90 rand.Next(65, 91)
            // a - z: ASCII 97 - 122 rand.Next(97, 123)
            // 0 - 9: ASCII 48 - 57 rand.Next(48, 58)
            Random rand = new Random();
            char[] chArr = new char[9];
            string otp = "";

            for (int i = 0; i < chArr.Length; i++)
            {
                int num =  i < 3? num = i : num = rand.Next(0, 3);

                if (num == 0) 
                {
                    chArr[i] = (char) rand.Next(65, 91);     // upper case
                }

                else if (num == 1)
                {
                    chArr[i] = (char) rand.Next(97, 123);   // lower case
                }

                else if (num == 2)
                {
                    chArr[i] = (char) rand.Next(48, 58);    // number 0 - 9
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
}