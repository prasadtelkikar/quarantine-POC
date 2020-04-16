using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using touch_core_internal.Dtos.Category;

namespace touch_core_internal.Dtos.Badge
{
    public class AddBadgeDto
    {
        public Guid CategoryId { get; set; }

        public string Name { get; set; }
    }
}