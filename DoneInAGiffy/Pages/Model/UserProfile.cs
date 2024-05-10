using System;
using System.ComponentModel.DataAnnotations;

namespace DoneInAGiffy.Pages.Model
{
    public class UserProfile
    {
        public int UserId { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Last Login Time")]
        public DateTime LastLoginTime { get; set; }

        [Display(Name = "Profile Picture Link")]
        public string ProfilePictureLink { get; set; }

        [Display(Name = "Profile Description")]
        public string ProfileDescription { get; set; }
    }
}

