namespace LibraryWebApp.Models
{
    public class UserModel
    {
        public string? Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool? Active { get; set; }
        public int Account_ID { get; set; }
        public int Role_ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

    }
}
