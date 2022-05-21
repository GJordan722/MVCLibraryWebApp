using System.ComponentModel.DataAnnotations;

namespace LibraryWebApp.Models
{
    public class UserModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-z0-9.@]{8,50}$")]
        public string? Email { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-z0-9.@]{8,50}$", ErrorMessage = "Please enter a valid username. Username must be from 8-50 characters long.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password incorrect")]
        [RegularExpression(@"^[a-zA-Z0-9!@.,?#$%^&*]{8,50}$", ErrorMessage = "Passwords may only contain letters, numbers, (!.,@?#$%^&*_-) and must be from 8-50 letters.")]
        public string Password { get; set; }
        public bool? Active { get; set; }
        public int? Account_ID { get; set; }
        public int? Role_ID { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid name.")]
        public string? FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]{2,}$",ErrorMessage = "Please enter a valid surname.")]
        public string? LastName { get; set; }
        public SearchModel _Search { get; set; }
    }
}
