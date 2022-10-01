using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class FoodItem
    {
        public Guid Id { get; set; }

        [MaxLength(50, ErrorMessage = "Food name can't be longer than 50 characters")]
        [Required(ErrorMessage = "Food name is required")]

        public string Name { get; set; }

        // food description
        [MaxLength(250, ErrorMessage = "Food description can't be longer than 250 characters")]
        public string Description { get; set; }

        public string Image { get; set; }


        [Required(ErrorMessage = "Food price is required")]
        [Range(1, 1000, ErrorMessage = "Food price must be between 1 and 100000")]
        public decimal Price { get; set; }


        [MaxLength(50, ErrorMessage = "Food name can't be longer than 50 characters")]
        public string Unit { get; set; } // per plate, per kg, per piece


        [Required(ErrorMessage = "Food availability is required")]
        public bool isAvailable { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
