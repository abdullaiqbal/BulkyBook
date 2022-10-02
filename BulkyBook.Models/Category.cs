using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order Must be between 1 to 100!!")]
        public string DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        // This is just for testing
        //private int _test;
        //public int Test { 
        //    get 
        //    {
        //        if (_test % 2 == 0)
        //            return _test;
        //        else return 0;
        //    } 
        //    set
        //    {
        //        _test = value; 
        //    } 
        //}
       
    }
}
