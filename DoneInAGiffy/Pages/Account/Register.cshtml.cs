using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DoneInAGiffy.Pages.Account.Model;
using Microsoft.Data.SqlClient;
using GIFLibrary; //Need for inserting data

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
            if (ModelState.IsValid)  //Checks if theres anything in the textbox
            {
                // Insert data into database
                // 1) Create a database connection String
                //string connString = "Server = (localdb)\\MSSQLocalDB; Database = DoneInAGiffy; Trusted_Connection = true;";
                SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString());
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
                conn.Close();
                return RedirectToPage("Login");
            }
            return Page();
        }
    }
}
