using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace touch_core_internal.Dtos.Category
{
    public class GetCategoryDto
    {
        public Guid CategoryId { get; set; }

        public string Name { get; set; }

        public int Points { get; set; }
    }
}