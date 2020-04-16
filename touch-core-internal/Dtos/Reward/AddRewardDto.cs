using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace touch_core_internal.Dtos.Reward
{
    public class AddRewardDto
    {
        public Guid BadgeId { get; set; }

        public Guid EmployeeId { get; set; }
    }
}