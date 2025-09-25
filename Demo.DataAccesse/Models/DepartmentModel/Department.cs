using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccesse.Models.Shared;

namespace Demo.DataAccesse.Models.DepartmentModel
{
    public class Department : BaseEntity
    {
        public string name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
        
    }
}
