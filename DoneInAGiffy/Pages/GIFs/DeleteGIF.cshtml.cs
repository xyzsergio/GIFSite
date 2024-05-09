using GIFLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;

namespace DoneInAGiffy.Pages.GIFs
{
    // [Authorize(Roles = "1")]
    public class DeleteGIFModel : PageModel
    {
        public IActionResult OnGet(int id)
        {
            using (SqlConnection conn = new SqlConnection(SecurityHelper.GetDBConnectionString()))
            {
                string cmdText = "DELETE FROM Gif WHERE GIFID=@itemId";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@itemId", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                return RedirectToPage("ViewGIFs");
            }
        }
    }
}
