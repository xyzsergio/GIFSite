using DoneInAGiffy.Pages.Model;
using GIFLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DoneInAGiffy.Pages.GIFs
{
    [BindProperties]
    // this page has a lot of stuff missing because our current mockup for the site doesn't have a dropdown to filter GIFs
    public class ViewGIFsModel : PageModel
    {
        public void OnGet()
        {
        }
        
        public void OnPost()
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "Select gifID, Title, Description, UploadDate, FilePath from GIF";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var gif = new GIF();
                        gif.gifID = reader.GetInt32(0);
                        gif.gifTitle = reader.GetString(1);
                        gif.gifDescription = reader.GetString(2);
                        gif.gifUploadDate = reader.GetDateTime(3);
                        gif.gifFilePath = reader.GetString(4);
                    }
                }
            }
        }
    }
}
