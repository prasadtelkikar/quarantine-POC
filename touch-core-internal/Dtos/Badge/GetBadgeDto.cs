using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using touch_core_internal.Dtos.Category;

namespace touch_core_internal.Dtos.Badge
{
    public class GetBadgeDto
    {
        public Guid BadgeId { get; set; }

        public GetCategoryDto Category { get; set; }

        public string Name { get; set; }
    }
}