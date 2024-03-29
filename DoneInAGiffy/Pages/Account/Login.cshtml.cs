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
        public Login loginUser {  get; set; }
        public void OnGet()
        {
        }
        public ActionResult OnPost() 
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString());
                string cmdText = "SELECT Password FROM User WHERE Email=@email";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
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
                            return RedirectToPage("Profile"); // Edit what page it returns to
                        }
                        else
                        {
                            ModelState.AddModelError("LoginError", "Invalid credentials. Try again.");
                            return Page();
                        }
                    }
                    return Page();
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Invalid credentials. Try again.");
                    return Page();
                }

                conn.Close();
            }
            else
            {
                return Page();
            }

        }
    }
}
