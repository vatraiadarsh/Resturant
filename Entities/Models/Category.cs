using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string NameInNepali { get; set; }
        public string Description { get; set; }
        public ICollection<FoodItem> FoodItems { get; set; }

    }
}
