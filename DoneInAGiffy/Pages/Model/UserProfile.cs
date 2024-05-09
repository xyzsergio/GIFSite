using System.ComponentModel.DataAnnotations;

namespace DoneInAGiffy.Pages.Model
{
    public class UserProfile
    {
        
        public int UserId { get; set; }
        [Display(Name = "Username : ")]

        public string Username { get; set; }
        [Display(Name = "Email: ")]

        public string Email { get; set; }
        [Display(Name = "Password: ")]
        public string Password { get; set; }

        public DateTime LastLoginTime { get; set; }

        [Display(Name = "Profile Picture Link")]
        public string ProfilePictureLink { get; set; }

        [Display(Name = "Profile Description")]
        public string ProfileDescription { get; set; }
    }
}
