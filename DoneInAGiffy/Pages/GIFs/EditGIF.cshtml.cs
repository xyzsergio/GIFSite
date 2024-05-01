using DoneInAGiffy.Pages.Model;
using GIFLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace DoneInAGiffy.Pages.GIFs
{
    [Authorize]
    [BindProperties]
    public class EditGIFModel : PageModel
    {
        public GIF Item { get; set; } = new GIF();
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        

        public void OnGet(int itemid)
        {
            PopulateEditGIF(itemid);
            PopulateCategoryDDL();

        }

        public IActionResult OnPost(int itemid)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
                {
                    string cmdText = "UPDATE GIF SET Title=@title, Description=@description, UploadDate=@uploaddate, Link=@link, CategoryID=@categoryID WHERE GIFID=@itemId";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@title", Item.gifTitle);
                    cmd.Parameters.AddWithValue("@description", Item.gifDescription);
                    cmd.Parameters.AddWithValue("@uploaddate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@link", Item.gifLink);
                    cmd.Parameters.AddWithValue("@categoryID", Item.gifCategoryID);
                    cmd.Parameters.AddWithValue("@itemId", itemid);

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
                    while (reader.Read())
                    {
                        var category = new SelectListItem();
                        category.Value = reader.GetInt32(0).ToString();
                        category.Text = reader.GetString(1);
                        if (category.Value == Item.gifCategoryID.ToString())
                        {
                            category.Selected = true;
                        }
                        Categories.Add(category);
                    }

                }
            }
        }

        private void PopulateEditGIF(int id)
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "SELECT GIFID, Title, Description, UploadDate, Link, CategoryID FROM GIF WHERE GIFID=@itemId";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@itemId", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    Item.gifID = id;
                    Item.gifTitle = reader.GetString(1);
                    Item.gifDescription = reader.GetString(2);
                    Item.gifUploadDate = reader.GetDateTime(3);
                    Item.gifLink = reader.GetString(4);
                    Item.gifCategoryID = reader.GetInt32(5);
                  
                }
            }
        }
    }
}
