using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DoneInAGiffy.Pages.Account.Model;
using Microsoft.Data.SqlClient;
using GIFLibrary;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;
using System.Xml;
using System.Text.RegularExpressions;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Identity;

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
                // Check if the email already exists
                if (EmailExists(newUser.Email))
                {
                    ModelState.AddModelError("newUser.Email", "An account with this email already exists.");
                    return Page();
                }

                // Check if the username already exists
                if (UsernameExists(newUser.Username))
                {
                    ModelState.AddModelError("newUser.Username", "This username is already taken.");
                    return Page();
                }
                if (!isPasswordValid(newUser.Password))
                {
                    ModelState.AddModelError("newUser.Password", "Password must be at least 10 characters long, have a number,\n" +
                        "have at least one uppercase and one lowercase letter.");
                    return Page();
                }

                // If email and username are unique, proceed with account creation
                RegisterUser();
                return RedirectToPage("Login");
            }
            return Page();
        }

        private void RegisterUser()
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                // create new user
                string cmdText = "INSERT INTO [User](Username, Email, Password, LastLoginTime, PermissionID) " + 
                    "VALUES(@username, @email, @password, @lastlogintime, @permissionid)";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@username", newUser.Username);
                cmd.Parameters.AddWithValue("@email", newUser.Email);
                cmd.Parameters.AddWithValue("@password", SecurityHelper.GeneratePasswordHash(newUser.Password));
                cmd.Parameters.AddWithValue("@lastlogintime", DateTime.Now);
                cmd.Parameters.AddWithValue("@permissionid", 2);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                
            }
            
        }
        private bool isPasswordValid (string password)
        {
            // at least 10 characters, one number, one lowercase and one uppercase
            string regex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{10,}$";
            Regex passwordValidation = new Regex(regex);

            return passwordValidation.IsMatch(password);
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
        private int getUserID(string email)
        {
            int returnThis = -1;
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "Select [User].UserID From [User] where Email=@email";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@email", email);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (!reader.IsDBNull(0))
                    {
                        returnThis = reader.GetInt32(0);
                    }
                        
                }
            }
            return returnThis;
        }

        private bool EmailExists(string email)
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "SELECT COUNT(*) FROM [User] WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private bool UsernameExists(string username)
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "SELECT COUNT(*) FROM [User] WHERE Username = @Username";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
