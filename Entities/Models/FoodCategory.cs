using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        [MaxLength(50, ErrorMessage = "Category name can't be longer than 50 characters")]
        [Required(ErrorMessage = "Category name is required")]
        public string Name { get; set; } 

        [MaxLength(50, ErrorMessage = "Category name can't be longer than 50 characters")]
        [Required(ErrorMessage = "Category name is required")]
        public string NameInNepali { get; set; }
        
        [MaxLength(250, ErrorMessage = "Category description can't be longer than 250 characters")]
        public string Description { get; set; }
        public ICollection<FoodItem> FoodItems { get; set; }

    }
}
