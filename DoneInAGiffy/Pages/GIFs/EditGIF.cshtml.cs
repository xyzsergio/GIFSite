using DoneInAGiffy.Pages.Model;
using GIFLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DoneInAGiffy.Pages.GIFs
{
    [BindProperties]
    public class EditGIFModel : PageModel
    {
        public GIF GifItem { get; set; } = new GIF();
        
        public void OnGet(int id)
        {
            PopulateGIF(id);
        }

        public IActionResult OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
                {
                    string cmdText = "UPDATE GIF SET Title=@title, Description=@description, UploadDate=@uploaddate, FilePath=@filepath " + 
                        "WHERE gifID=@gifID";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@title", GifItem.gifTitle);
                    cmd.Parameters.AddWithValue("@description", GifItem.gifDescription);
                    cmd.Parameters.AddWithValue("@uploaddate", GifItem.gifUploadDate);
                    cmd.Parameters.AddWithValue("@filepath", GifItem.gifLink);
                    cmd.Parameters.AddWithValue("@gifID", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return RedirectToPage("ViewGIFs");
                }
            }
            else
            {
                return Page();
            }
        }

        private void PopulateGIF(int id)
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "SELECT gifID, Title, Description, UploadDate, FilePath from GIF WHERE gifID=@gifid";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@gifId", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    GifItem.gifID = id;
                    GifItem.gifTitle = reader.GetString(1);
                    GifItem.gifDescription = reader.GetString(2);
                    GifItem.gifUploadDate = reader.GetDateTime(3);
                    GifItem.gifLink = reader.GetString(4);
                }
            }
        }
    }
}
