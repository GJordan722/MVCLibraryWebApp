using System.ComponentModel.DataAnnotations;

namespace LibraryWebApp.Models
{
    public class UserModel
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Range(5, 20)]
        [Required(ErrorMessage ="Password incorrect")]
        public string Password { get; set; }
        public bool? Active { get; set; }
        public int? Account_ID { get; set; }
        public int? Role_ID { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public SearchModel _Search { get; set; }
    }
}
