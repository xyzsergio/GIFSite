using DoneInAGiffy.Pages.Model;
using GIFLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

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
            string cmdText = "SELECT Password, UserID FROM [User] WHERE Email=@email";
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
