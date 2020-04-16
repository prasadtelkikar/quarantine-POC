using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace touch_core_internal.Dtos.TimeSheet
{
    public class AddTimeSheetDto
    {
        public Guid EmployeeId { get; set; }

        public DateTime FromDateTime { get; set; }

        public DateTime ToDateTime { get; set; }
    }
}