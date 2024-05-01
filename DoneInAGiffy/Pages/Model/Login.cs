using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DoneInAGiffy.Pages.Model
{
    public class Login
    {
        [Required(ErrorMessage = "Your Email is required.")]
        [Display(Name = "Email: ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The password is required.")]
        [Display(Name = "Password: ")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{10,}$", 
            ErrorMessage = "Password must be at least 10 characters long, contain at least one number, one uppercase, and one lowercase letter.")]
        public string Password { get; set; }
    }
}

