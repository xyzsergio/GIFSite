using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DoneInAGiffy.Pages.Account
{
    public class ViewProfileModel : PageModel
    {
        // Properties representing the profile details
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Last Login Time")]
        public string LastLoginTime { get; set; }

        [Display(Name = "Profile Picture Link")]
        public string ProfilePictureLink { get; set; }

        [Display(Name = "Profile Description")]
        public string ProfileDescription { get; set; }

        // OnGet method to populate profile details
        public void OnGet(string username, string email, string lastLoginTime, string profilePictureLink, string profileDescription)
        {
            Username = username;
            Email = email;
            LastLoginTime = lastLoginTime;
            ProfilePictureLink = profilePictureLink;
            ProfileDescription = profileDescription;
        }
    }
}
