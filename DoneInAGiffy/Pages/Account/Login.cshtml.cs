using DoneInAGiffy.Pages.Model;
using GIFLibrary;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace DoneInAGiffy.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login loginUser { get; set; }
        public void OnGet()
        {
        }
        public ActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (ValidateCredentials())
                {
                    return RedirectToPage("Profile"); // Edit what page it returns to
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Invalid credentials. Try again.");
                    return Page();
                }
            }
            else
            {
                return Page();
            }

        }

        private bool ValidateCredentials()
        {
            bool returnThis = false;

            SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString());
            // Fix this line later - we don't have RoleId in user table
            string cmdText = "SELECT Password, [User].UserID, Username, Email, ProfilePictureLink, ProfileDescription, PermissionName FROM [User] INNER JOIN [Permissions] ON [User].PermissionID = [Permissions].PermissionID WHERE Email=@email";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.Parameters.AddWithValue("@Email", loginUser.Email);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                if (!reader.IsDBNull(0))
                {
                    string passwordHash = reader.GetString(0);   //There is only one row "0", since it filtered by email
                    if (SecurityHelper.VerifyPassword(loginUser.Password, passwordHash))
                    {
                        // get the userID and use it to update the user record
                        int userID = reader.GetInt32(1);
                        UpdateUserLoginTime(userID);
                        // create a principal
                        string username = reader.GetString(2);
                        string roleName = reader.GetString(6);
                        string profilePicture = reader.GetString(4);
                        string profileDescription = reader.GetString(5);
                        // 1. create a list of claims
                        Claim emailClaim = new Claim(ClaimTypes.Email, loginUser.Email);
                        Claim nameClaim = new Claim(ClaimTypes.Name, username);
                        Claim roleClaim = new Claim(ClaimTypes.Role, roleName);
                        Claim userIDClaim = new Claim(ClaimTypes.Actor, userID.ToString());
                        Claim pictureClaim = new Claim(ClaimTypes.Name, profilePicture);
                        Claim descriptionClaim = new Claim(ClaimTypes.Name, profileDescription);

                        List<Claim> claims = new List<Claim> { emailClaim, nameClaim, roleClaim, userIDClaim, pictureClaim, descriptionClaim };
                        // 2. add the list of claims to a ClaimsIdentity
                        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        // 3. add the identity to a ClaimsPrincipal
                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                        // 4. call HttpContext.SignInAsync() method to encrypt the principal
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        returnThis = true;
                    }
                    else
                    {
                        ModelState.AddModelError("LoginError", "Invalid credentials. Try again.");
                        returnThis = false;
                    }
                }
            }
            conn.Close();

            return returnThis;
        }

        private void UpdateUserLoginTime(int userID)
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "Update \"User\" Set LastLoginTime=@lastLoginTime Where UserID=@userID";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@lastLoginTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@userID", userID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
