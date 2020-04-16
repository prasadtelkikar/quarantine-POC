using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using touch_core_internal.Dtos.Employee;

namespace touch_core_internal.Dtos.TimeSheet
{
    public class GetEmployeeToTimesheetDto
    {
        public Guid EmployeeId { get; set; }

        public DateTime FromDateTime { get; set; }

        public Guid TimeSheetId { get; set; }

        public DateTime ToDateTime { get; set; }
    }

    public class GetTimeSheetDto
    {
        public GetTimeSheetToEmployeeDto Employee { get; set; }

        public DateTime FromDateTime { get; set; }

        public Guid TimeSheetId { get; set; }

        public DateTime ToDateTime { get; set; }
    }
}