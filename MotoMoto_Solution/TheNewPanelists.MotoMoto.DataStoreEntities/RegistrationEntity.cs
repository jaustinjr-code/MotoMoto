namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public class RegistrationEntity
    {
        public int? RegistrationID { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime? Expiration {  get; set; }
        public bool? Validated { get; set; }
    }
}
