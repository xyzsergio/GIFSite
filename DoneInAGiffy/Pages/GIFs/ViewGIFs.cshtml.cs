using DoneInAGiffy.Pages.Model;
using GIFLibrary;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

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
            PopulateCategoryDDL();
        }

        public void OnPost()
        {
            PopulateViewGIF(CategoryId);
            PopulateCategoryDDL();
        }

        private void PopulateViewGIF(int categoryId)
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "SELECT Title, Description, UploadDate, Link FROM GIF WHERE CategoryId=@categoryId";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@categoryId", categoryId);
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
