using DoneInAGiffy.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoneInAGiffy.Pages.Account
{
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public UserProfile profile {  get; set; }
        public void OnGet()
        {

        }
    }
}
