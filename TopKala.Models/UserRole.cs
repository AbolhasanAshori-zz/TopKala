using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopKala.Models
{
    public class UserRole : BaseEntity<int>
    {
        public string Name { get; set; }
    }
}