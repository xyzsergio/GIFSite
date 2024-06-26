using DoneInAGiffy.Pages.Model;
using GIFLibrary;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using DoneInAGiffy.Pages.Account.Model;
using Microsoft.AspNetCore.Authorization;

namespace DoneInAGiffy.Pages.GIFs
{
    [BindProperties]
    public class ViewGIFsModel : PageModel
    {

        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        public List<GIF> GIFs { get; set; } = new List<GIF>();

        public int CategoryId { get; set; }

        public void OnGet()
        {
            int userId = GetCurrentlyLoggedUserId();
            CategoryId = GetAngryCategoryId();
            PopulateCategoryDDL();
            PopulateViewGIF(userId, CategoryId);
        }

        private int GetAngryCategoryId()
        {
            int angryCategoryId = 0;

            // Retrieve the ID of the "Angry" category from the database
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "SELECT CategoryID FROM Category WHERE CategoryName = 'Angry'";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    angryCategoryId = Convert.ToInt32(result);
                }
            }

            return angryCategoryId;
        }

        private int GetCurrentlyLoggedUserId()
        {
            return int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Actor));
        }

        public void OnPost()
        {
            int userId = GetCurrentlyLoggedUserId(); // Get the user ID
            PopulateViewGIF(userId, CategoryId); // Populate GIFs for the logged-in user
            PopulateCategoryDDL();
        }

        private void PopulateViewGIF(int userId, int categoryId)
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "SELECT Title, Description, UploadDate, Link, GIFID FROM GIF WHERE CategoryId=@categoryId AND UserID=@userId";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@categoryId", categoryId);
                cmd.Parameters.AddWithValue("@userId", userId); // Pass the userId parameter to the query
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var item = new GIF();
                        item.gifTitle = reader.GetString(0);
                        item.gifDescription = reader.GetString(1);
                        item.gifUploadDate = reader.GetDateTime(2);
                        item.gifLink = reader.GetString(3);
                        item.gifID = reader.GetInt32(4);
                        GIFs.Add(item);
                    }
                }
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
                        if (category.Value == CategoryId.ToString())
                        {
                            category.Selected = true;
                        }
                        Categories.Add(category);
                    }

                }
            }
        }

    }
}
