namespace TheNewPanelists.MotoMoto.Models
{
    ///<summary>Data model representing an incoming registration request.</summary>
    ///<value>Property <c>Email</c> represents the new user's email address</value>
    ///<value>Property <c>Password</c> represents the new user's password</value>
    ///<value>Property <c>username</c> represents the new user's username</value>
    ///<value>Property <c>RegistrationId</c> represents the new user's registrationId</value>
    ///<value>Property <c>status</c> represents the success status of the request</value>
    ///<value>Property <c>message</c> represents the message associated the the function result</value>
    public class RegistrationRequestModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public int RegistrationId { get; set; }
        public bool status { get; set; }
        public string? message { get; set; }
    }
}
