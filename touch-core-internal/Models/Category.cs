using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace touch_core_internal.Models
{
    public class Category
    {
        public Guid CategoryId { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public int Points { get; set; }
    }
}