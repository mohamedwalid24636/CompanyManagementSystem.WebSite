using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccesse.Models.DepartmentModel;
using Demo.DataAccesse.Models.Shared;
using Demo.DataAccesse.Models.Shared.Enums;

namespace Demo.DataAccesse.Models.EmployeeModel
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int Age { get; set; }
        public Decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public DateTime HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }

        public int? DepartmentId { get; set; } // Foreign Key to Department
        public virtual Department? Department { get; set; }
        public string? ImageName  { get; set; }
    }
}
