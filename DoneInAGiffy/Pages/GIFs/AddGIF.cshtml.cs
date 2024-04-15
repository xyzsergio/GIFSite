using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DoneInAGiffy.Pages.Model;
using DoneInAGiffy.Pages.Account.Model;
using GIFLibrary;
using Microsoft.Data.SqlClient;

namespace DoneInAGiffy.Pages.GIFs
{
    [BindProperties]
    public class AddGIFModel : PageModel
    {
        public GIF newGIF { get; set; } = new GIF();

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
                {
                    string cmdText = "INSERT INTO GIF(Title, Description, UploadDate, FilePath)" +
                        "VALUES (@title, @description, @uploaddate, @filepath)";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@title", newGIF.gifTitle);
                    cmd.Parameters.AddWithValue("@description", newGIF.gifDescription);
                    cmd.Parameters.AddWithValue("@uploaddate", newGIF.gifUploadDate);
                    cmd.Parameters.AddWithValue("@filepath", newGIF.gifFilePath);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return RedirectToPage("ViewGIFs");
                }
            } else
            {
                return Page();
            }
        }

        public void OnGet()
        {
            
        }
    }
}
