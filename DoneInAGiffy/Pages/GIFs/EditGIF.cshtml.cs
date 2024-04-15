using DoneInAGiffy.Pages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoneInAGiffy.Pages.GIFs
{
    [BindProperties]
    public class EditGIFModel : PageModel
    {
        public GIF Item { get; set; } = new GIF();
        
        public void OnGet(int id)
        {
        }
    }
}
