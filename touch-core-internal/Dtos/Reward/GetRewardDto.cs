using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using touch_core_internal.Dtos.Badge;
using touch_core_internal.Dtos.Employee;

namespace touch_core_internal.Dtos.Reward
{
    public class GetRewardDto
    {
        public GetBadgeDto Badge { get; set; }

        public GetEmployeeDto Employee { get; set; }

        public Guid RewardId { get; set; }
    }
}