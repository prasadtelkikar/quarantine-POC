using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using touch_core_internal.Dtos.TimeSheet;

namespace touch_core_internal.Dtos.Employee
{
    public class GetEmployeeDto
    {
        public virtual string Designation { get; set; }

        public virtual string Email { get; set; }

        public virtual Guid EmployeeId { get; set; }

        public virtual string Identifier { get; set; }

        public virtual string Name { get; set; }

        public IList<GetEmployeeToTimesheetDto> TimeSheets { get; set; }
    }

    public class GetTimeSheetToEmployeeDto
    {
        public virtual string Designation { get; set; }

        public virtual string Email { get; set; }

        public virtual Guid EmployeeId { get; set; }

        public virtual string Identifier { get; set; }

        public virtual string Name { get; set; }
    }
}