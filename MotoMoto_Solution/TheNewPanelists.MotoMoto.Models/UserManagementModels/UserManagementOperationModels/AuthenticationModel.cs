using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models
{
    public class AuthenticationModel
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? ValidatedUsername { get; set; }
        public string? ValidatedPasword { get; set; }
        public string? Otp { get; set; }
        public DateTime? OtpExpireTime { get; set; }
        public DateTime? SessionEndTime { get; set; }
        public string? UserOtp { get; set; }
        public string? UserIP { get; set; }
        private string? AccountStatus { get; set; }
        public string? Salt { get; set; }
        public bool ValidUsername = false;
        public bool Authenticated = false;
        public bool Repeat = true;
        public int Attempts = 0;
    }
}
