using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using DoneInAGiffy.Pages.Model;
using GIFLibrary;

namespace DoneInAGiffy.Pages
{
    public class IndexModel : PageModel
    {
        public List<GIF> GIFs { get; set; } = new List<GIF>();

        public void OnGet()
        {
            // Connect to the database and retrieve GIFs
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "SELECT GIFID, Title, Description, UploadDate, Link FROM GIF";
                SqlCommand cmd = new SqlCommand(cmdText, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        GIF gif = new GIF
                        {
                            gifID = reader.GetInt32(0),
                            gifTitle = reader.GetString(1),
                            gifDescription = reader.IsDBNull(2) ? null : reader.GetString(2),
                            gifUploadDate = reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3),
                            gifLink = reader.GetString(4)
                        };
                        GIFs.Add(gif);
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions
                }
            }
        }
    }
}