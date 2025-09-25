using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccesse.Models.EmployeeModel;
using Demo.DataAccesse.Models.Shared.Enums;

namespace Demo.BusinessLogic.DataTransfareObject.EmployeeDtos
{
    public class GetEmployeesDto
    {
        public  int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        [EmailAddress]                   // This will validate the Email format in the UI
        public string? Email { get; set; }
        [Display(Name ="Is Active")]     // This will display "Is Active" in the UI instead of "IsActive" ( asp-for="" ) 
        public bool IsActive { get; set; }
        [DataType(DataType.Currency)]    // This will format the Salary as currency in the UI 
        public decimal Salary { get; set; }
        public string EmpGender { get; set; }

        [Display(Name = "Employee Type")]
        public string EmpType { get; set; }
        public string? Department { get; set; } // Department Name
    }
}
