using System.ComponentModel.DataAnnotations;

namespace DoneInAGiffy.Pages.Model
{
    public class GIF
    {
        public int gifID { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string gifTitle { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string gifDescription { get; set;}
        [Display(Name = "Uploaded on")]
        public DateTime gifUploadDate { get; set; }
        [Display(Name = "File path")]
        public string gifFilePath { get; set; }
        
    }
}
