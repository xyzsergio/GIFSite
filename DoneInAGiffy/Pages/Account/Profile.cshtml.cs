using DoneInAGiffy.Pages.Account.Model;
using DoneInAGiffy.Pages.Model;
using GIFLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace DoneInAGiffy.Pages.Account
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        public UserProfile profile { get; set; } = new UserProfile();

        public void OnGet()
        {
            PopulateProfile();
        }

        private void PopulateProfile()
        {
            // query the person table to populate "profile" object
            string email = HttpContext.User.FindFirstValue(ClaimValueTypes.Email);
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "SELECT Username, Email, LastLoginTime, ProfilePictureLink FROM [User] WHERE Email=@email";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@email", email);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    profile.Username = reader.GetString(0);
                    profile.Email = reader.GetString(1);
                    profile.LastLoginTime = reader.GetDateTime(2);

                    // Check if ProfilePictureLink is null
                    if (!reader.IsDBNull(3))
                    {
                        profile.ProfilePictureLink = reader.GetString(3);
                    }
                    else
                    {
                        profile.ProfilePictureLink = ""; // or assign a default link
                    }
                }
            }
        }
    }
}
