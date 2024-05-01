using DoneInAGiffy.Pages.Model;
using GIFLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DoneInAGiffy.Pages.GIFs
{
    [BindProperties]
    [Authorize(Roles = "1,2")]
    // this page has a lot of stuff missing because our current mockup for the site doesn't have a dropdown to filter GIFs
    public class ViewGIFsModel : PageModel
    {
        public List<GIF> GIFs { get; set; } = new List<GIF>();

        public void OnGet()
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "Select gifID, Title, Description, UploadDate, Link from GIF";
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
                        gif.gifLink = reader.GetString(4);
                        GIFs.Add(gif);
                    }
                }
            }
        }
        
        public void OnPost()
        {
            
        }
    }
}
