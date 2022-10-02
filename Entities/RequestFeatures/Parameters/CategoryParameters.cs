using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures.Parameters
{
    public class CategoryParameters : RequestParameters
    {
        public CategoryParameters()
        {
            OrderBy = "name";
        }


        public string SearchTerm { get; set; }
    }
}
