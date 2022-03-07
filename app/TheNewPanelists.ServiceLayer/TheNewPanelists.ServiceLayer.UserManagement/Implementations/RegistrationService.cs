using TheNewPanelists.DataAccessLayer;
using TheNewPanelists.ServiceLayer.Logging;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Net.Mail;

namespace TheNewPanelists.ServiceLayer.UserManagement 
{
    class RegistrationService : IUserManagementService
    {
        private Dictionary<string, string> acct { get; set; }

        public RegistrationService()
        {
            this.acct = new Dictionary<string, string>();
        }
        public RegistrationService(Dictionary<string, string> acctInfo)
        {
            this.acct = acctInfo;
        }

        public bool ValidateRequest()
        {
            return acct.ContainsKey("email") && acct.ContainsKey("url");
        }

        public string URLGenerator()
        {
            Random rand = new Random();
            int urlSize = 6;
            string url = "";
     
            for (int i = 0; i < urlSize; i++)
            {
                int num = rand.Next(0,3);

                if (num == 0)
                    url += (char)rand.Next(65, 91);     // upper case
                else if (num == 1)
                    url += (char)rand.Next(97, 123);   // lower case
                else if (num == 2)
                    url += (char)rand.Next(48, 58);    // number 0 - 9
              }
            return acct["url"] + url;
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
            return name;
        }

        public bool SendEmail()
        {
            StringBuilder input = new StringBuilder();

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
                mail.To.Add("daniel.bribiesca@student.csulb.edu");
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

        public bool SqlGenerator()
        {
            return true;
        }
    }
}
