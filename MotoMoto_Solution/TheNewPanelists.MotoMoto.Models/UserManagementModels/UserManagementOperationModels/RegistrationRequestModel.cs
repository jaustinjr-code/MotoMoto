namespace TheNewPanelists.MotoMoto.Models
{
    public class RegistrationRequestModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int RegistrationId { get; set; }
        public bool status { get; set; }
        public string? message { get; set; }
    }
}
