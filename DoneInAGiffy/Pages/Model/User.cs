using System.ComponentModel.DataAnnotations;

namespace DoneInAGiffy.Pages.Account.Model
{
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "The Username is required.")]
        [Display(Name = "Username : ")]

        public string Username { get; set; }
        [Required(ErrorMessage = "Your Email is required.")]
        [Display(Name = "Email: ")]

        public string Email { get; set; }
        [Required(ErrorMessage = "The password is required.")]
        [StringLength(100, ErrorMessage = "The password must be at least 10 characters long.", MinimumLength = 10)]
        [DataType(DataType.Password)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }
    }
}
