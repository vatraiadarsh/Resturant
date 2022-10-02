using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class FoodItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; } // per plate, per kg, per piece
        public bool isAvailable { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
