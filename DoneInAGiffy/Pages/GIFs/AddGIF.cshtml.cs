using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DoneInAGiffy.Pages.Model;
using GIFLibrary;
using Microsoft.Data.SqlClient;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace DoneInAGiffy.Pages.GIFs
{
    // Use to connect to claim
    [Authorize]
    [BindProperties]
    public class AddGIFModel : PageModel
    {
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public GIF newGIF { get; set; } = new GIF();

        public IActionResult OnPost()
        { //Actor
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
                {
                    // The format for pulling the claim from login, the parse is because its return as a int
                    int userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Actor));
                    string cmdText = "INSERT INTO GIF(Title, Description, UploadDate, Link, CategoryID, UserID)" +
                        "VALUES (@title, @description, @uploaddate, @link, @categoryID, @userId)";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@title", newGIF.gifTitle);

                    cmd.Parameters.AddWithValue("@description", newGIF.gifDescription); 
                    cmd.Parameters.AddWithValue("@uploaddate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@link", newGIF.gifLink);
                    cmd.Parameters.AddWithValue("@categoryID", newGIF.gifCategoryID);
                    cmd.Parameters.AddWithValue("@userId", userId);

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

        public void OnGet()
        {
            PopulateCategoryDDL();
        }

        private void PopulateCategoryDDL()
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "SELECT CategoryID, CategoryName FROM Category ORDER BY CategoryName";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        var category = new SelectListItem();
                        category.Value = reader.GetInt32(0).ToString();
                        category.Text = reader.GetString(1);
                        Categories.Add(category);
                    }
                    
                }
            }
        }
    }
}
