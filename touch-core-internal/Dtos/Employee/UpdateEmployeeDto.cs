﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace touch_core_internal.Dtos.Employee
{
    public class UpdateEmployeeDto
    {
        public virtual string Designation { get; set; }

        public virtual string Email { get; set; }

        public Guid EmployeeId { get; set; }

        public virtual string Identifier { get; set; }

        public virtual string Name { get; set; }
    }
}