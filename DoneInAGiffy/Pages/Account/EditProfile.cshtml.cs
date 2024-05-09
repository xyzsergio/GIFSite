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

        public void OnGet(int itemid)
        {
            PopulateProfile();
        }

        public IActionResult OnPost(int itemid)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
                {
                    string cmdText = "UPDATE [User] SET Username=@username, Email=@email, ProfilePictureLink=@profilePictureLink, ProfileDescription=@profileDescription WHERE UserID=@itemId";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@username", profile.Username);
                    cmd.Parameters.AddWithValue("@email", profile.Email);
                    cmd.Parameters.AddWithValue("@profilePictureLink", profile.ProfilePictureLink ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@profileDescription", profile.ProfileDescription ?? (object)DBNull.Value);
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    return RedirectToPage("Profile");
                }
            }
            else
            {
                foreach (var modelStateEntry in ModelState.Values)
                {
                    foreach (var error in modelStateEntry.Errors)
                    {
                        Console.WriteLine($"Error: {error.ErrorMessage}");
                    }
                }
                return Page();
            }
        }

        private void PopulateProfile()
        {
            string email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "SELECT Username, Email, ProfilePictureLink, ProfileDescription FROM [User] WHERE Email=@email";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@email", email);
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
    }
}


