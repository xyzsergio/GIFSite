using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DoneInAGiffy.Pages.Account.Model;
using Microsoft.Data.SqlClient;
using GIFLibrary;

namespace DoneInAGiffy.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public User newUser {  get; set; }
        public void OnGet()
        {
        }

        public ActionResult OnPost()
        {
            if (ModelState.IsValid)  // Checks if theres anything in the textbox
            {
                // Ensure the email doesn't exist before registering the user
                if (EmailDNE(newUser.Email))
                {
                    RegisterUser();
                    return RedirectToPage("Login");
                } else
                {
                    ModelState.AddModelError("RegisterError", "That email already exists. Try a different one.");// Bug: doesn't appear for some reason
                }
            }
            return Page();
        }

        private void RegisterUser()
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                // 2) Create a insert command
                string cmdText = "INSERT INTO [User](Username, Email, Password) " + "VALUES(@username, @email, @password)";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@username", newUser.Username);      //Order doesn't matter
                cmd.Parameters.AddWithValue("@email", newUser.Email);
                cmd.Parameters.AddWithValue("@password", SecurityHelper.GeneratePasswordHash(newUser.Password));   //Encrypts password
                // 3) Open the database
                conn.Open();
                // 4) Execute the command
                cmd.ExecuteNonQuery();
                // 5) Close the database
                // conn.Close();
            }
        }

        private bool EmailDNE(string email)
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "Select * From \"User\" where Email=@email";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@email", email);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return false;
                } else
                {
                    return true;
                }
            }
        }
    }
}
