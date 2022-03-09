using TheNewPanelists.ServiceLayer.UserManagement;
using System.Text.RegularExpressions;
using System.Text;
using System.Net.Mail;


namespace TheNewPanelists.BusinessLayer
{

    public class RegistrationManager
    {
        private Dictionary<string, string> registrationInfo { get; set; }

        private string operation;

        public RegistrationManager()
        {
            registrationInfo = new Dictionary<string, string>();
            this.operation = String.Empty;
        }

        public RegistrationManager(string operation, Dictionary<string, string> registrationInfo)
        {
            this.operation = operation;
            this.registrationInfo = registrationInfo;
        }

        public bool IsValidRequest()
        {
            return (this.operation.Contains("ISVALID") && registrationInfo.ContainsKey("email") && registrationInfo.ContainsKey("url"))
                || (this.operation.Contains("DROPREG") && registrationInfo.ContainsKey("email"))
                || (this.operation.Contains("ACCOUNT REGISTRATION") && registrationInfo.ContainsKey("email") && registrationInfo.ContainsKey("password"))
                || (this.operation.Contains("RETURNREG") && registrationInfo.ContainsKey("email"))
                || (this.operation.Contains("CONFIRMREG") && registrationInfo.ContainsKey("url"))
                || (this.operation.Contains("REGDOESNOTEXIST") && registrationInfo.ContainsKey("email"));
        }
        public bool IsValidRequest(string op, Dictionary<string, string> regInfo)
        {
            return (op.Contains("ISVALID") && regInfo.ContainsKey("email") && regInfo.ContainsKey("url"))
                    || (op.Contains("DROPREG") && regInfo.ContainsKey("email"))
                    || (op.Contains("ACCOUNT REGISTRATION") && regInfo.ContainsKey("email") && regInfo.ContainsKey("password"))
                    || (op.Contains("RETURNREG") && regInfo.ContainsKey("email"))
                    || (op.Contains("CONFIRMREG") && regInfo.ContainsKey("url"))
                    || (op.Contains("REGDOESNOTEXIST") && registrationInfo.ContainsKey("email"));
        }

        public Dictionary<string, string> ReceiveOperation()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (this.IsValidRequest())
            {
                RegistrationService registrationService = new RegistrationService(this.operation, this.registrationInfo);
                result = registrationService.ReturnSqlGenerator();
            }
            return result;
        }

        public Dictionary<string, string> ReceiveOperation(string op, Dictionary<string, string> regInfo)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (this.IsValidRequest(op, regInfo))
            {
                RegistrationService registrationService = new RegistrationService(op, regInfo);
                result = registrationService.ReturnSqlGenerator();
            }
            return result;
        }

        public bool SendOperation()
        {
            bool success = false;
            if (this.IsValidRequest())
            {
                RegistrationService registrationService = new RegistrationService(this.operation, this.registrationInfo);
                success = registrationService.SqlGenerator();
            }
            return success;
        }

        public bool SendOperation(string op, Dictionary<string, string> regInfo)
        {
            bool success = false;
            if (this.IsValidRequest(op, regInfo))
            {
                RegistrationService registrationService = new RegistrationService(op, regInfo);
                success = registrationService.SqlGenerator();
            }
            return success;
        }

        public string URLGenerator()
        {
            Random rand = new Random();
            int urlSize = 6;
            string url = "";

            for (int i = 0; i < urlSize; i++)
            {
                int num = rand.Next(0, 3);

                if (num == 0)
                    url += (char)rand.Next(65, 91);     // upper case
                else if (num == 1)
                    url += (char)rand.Next(97, 123);   // lower case
                else if (num == 2)
                    url += (char)rand.Next(48, 58);    // number 0 - 9
            }
            return registrationInfo["url"] + url;
        }

        public string GenerateUniqueName()
        {
            Random rand = new Random();
            int nameSize = 5;
            string name = "";

            for (int i = 0; i < nameSize; i++)
            {
                int num = rand.Next(0, 3);

                if (num == 0)
                    name += (char)rand.Next(65, 91);
                else if (num == 1)
                    name += (char)rand.Next(97, 123);
                else if (num == 2)
                    name += (char)rand.Next(48, 58);
                else
                {
                    char sChar;
                    int sel = rand.Next(0, 4);

                    if (sel == 0)
                        sChar = '!';
                    else if (sel == 1)
                        sChar = '@';
                    else if (sel == 2)
                        sChar = '.';
                    else
                        sChar = ',';

                    name += sChar;
                }
            }
            return name + this.registrationInfo["url"];
        }

        public bool SendEmail(string userEmail)
        {
            StringBuilder input = new StringBuilder();

            string email = "projmotomoto@gmail.com";
            string pass = "Tester491!";
            //Console.WriteLine("Enter password:");
            //while (true)
            //{
            //    var key = Console.ReadKey(true);
            //    if (key.Key == ConsoleKey.Enter) break;
            //    if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
            //    else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
            //}
            //string pass = input.ToString();

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
                var checkedEmail = new MailAddress(userEmail);
                mail.From = new MailAddress(email, "MotoMoto");
                mail.To.Add(checkedEmail);
                // mail.To.Add(this.acct["email"]);
                // mail.To.Add("jacob.sunia@student.csulb.edu");
                mail.Subject = "Email Confirmation";

                mail.Body = @$"
                    <html>
                        <body>
                            <p></p>Hello,</p>
                            <p>Please click on the link below to complete  
                            your account registration.<br>
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
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}