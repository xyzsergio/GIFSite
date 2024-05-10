using DoneInAGiffy.Pages.Model;
using GIFLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace DoneInAGiffy.Pages.Account
{
    [BindProperties]
    public class EditProfileModel : PageModel
    {
        public UserProfile profile { get; set; } = new UserProfile();

        public void OnGet()
        {
            PopulateProfile();
        }

        public void OnPost()
        {
            UpdateProfile();
        }

        private void PopulateProfile()
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                int userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Actor));
                string cmdText = "SELECT Username, Email, ProfilePictureLink, ProfileDescription FROM [User] WHERE UserId=@userid";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@userid", userId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    profile.Username = reader.GetString(0);
                    profile.Email = reader.GetString(1);
                    profile.ProfilePictureLink = reader.IsDBNull(2) ? null : reader.GetString(2);
                    profile.ProfileDescription = reader.IsDBNull(3) ? null : reader.GetString(3);
                }
            }
        }

        private IActionResult UpdateProfile()
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
                {
                    int userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Actor));
                    int permissionId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Role));
                    String password = (int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Hash))).ToString();
                    string cmdText = "UPDATE [User] SET Username=@username, Email=@email,Password=@password, LastLoginTime=@lastlogintime, PermissionID=@permissionid, ProfilePictureLink=@profilePictureLink, ProfileDescription=@profileDescription WHERE UserId=@userid";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.Parameters.AddWithValue("@username", profile.Username);
                    cmd.Parameters.AddWithValue("@email", profile.Email);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@lastlogintime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@permissionid", permissionId);
                    cmd.Parameters.AddWithValue("@profilePictureLink", profile.ProfilePictureLink ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@profileDescription", profile.ProfileDescription ?? (object)DBNull.Value);
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    return RedirectToPage("Profile");
                }
            }
            else
            {
                return Page();
            }
        }
    }
}


