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

        [Display(Name = "Link")]
        public string gifLink { get; set; }

        [Display(Name = "Category Name")]
        public int gifCategoryID { get; set; }

        public int UserID { get; set; }
    }
}
