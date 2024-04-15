using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DoneInAGiffy.Pages.Account.Model;
using Microsoft.Data.SqlClient;
using GIFLibrary;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;
using System.Xml;

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
                // create new user
                string cmdText = "INSERT INTO [User](Username, Email, Password, LastLoginTime, AdminID) " + 
                    "VALUES(@username, @email, @password, @lastlogintime, @adminid)";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@username", newUser.Username);
                cmd.Parameters.AddWithValue("@email", newUser.Email);
                cmd.Parameters.AddWithValue("@password", SecurityHelper.GeneratePasswordHash(newUser.Password));
                cmd.Parameters.AddWithValue("@lastlogintime", DateTime.Now);
                cmd.Parameters.AddWithValue("@adminid", 1);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                
            }
            
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                // put adminID into administrator table
                string cmdText = "INSERT INTO [Administrator](UserID, PermissionID) " +
                "VALUES(@userid, @permissionid)";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@permissionid", 1);
                cmd.Parameters.AddWithValue("@userid", getUserID(newUser.Email));

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
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
    }
}
