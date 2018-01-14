using System.ComponentModel.DataAnnotations;

namespace AssignmentWebApp.Models
{
    public class Product
    {        
        public int ProductID { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is required.")]
        public string Name { get; set; }
        [Display(Name = "Price")]
        public double ListPrice { get; set; }
        public string Description { get; set; }
    }
}