using System.ComponentModel.DataAnnotations;

namespace DoneInAGiffy.Pages.Model
{
    public class Login
    {
        [Required(ErrorMessage = "Your Email is required.")]
        [Display(Name = "Email: ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The password is required.")]
        [Display(Name = "Password: ")]
        public string Password { get; set; }
    }
}
