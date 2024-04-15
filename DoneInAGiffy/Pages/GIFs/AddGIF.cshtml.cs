using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DoneInAGiffy.Pages.Model;

namespace DoneInAGiffy.Pages.GIFs
{
    public class AddGIFModel : PageModel
    {
        public GIF newGIF { get; set; } = new GIF();

        public void OnGet()
        {
        }
    }
}
