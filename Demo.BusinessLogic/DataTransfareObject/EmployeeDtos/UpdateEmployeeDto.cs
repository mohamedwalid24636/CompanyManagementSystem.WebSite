using Demo.DataAccesse.Models.EmployeeModel;
using Demo.DataAccesse.Models.Shared.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DataTransfareObject.EmployeeDtos
{
    public class UpdateEmployeeDto
    {
        public int Id { get; set; }
        [Required()]
        [MaxLength(50)]
        [MinLength(5)]
        public string Name { get; set; }
        [Range(22, 35)]
        public int Age { get; set; }
        [RegularExpression("^[1-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}",
            ErrorMessage = "Address Must Be Like 123-Street-City-Country ")]

        public string? Address { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public int CreatedBy { get; set; }
        public int LastModifiedBy { get; set; }
        public int? DepartmentId { get; set; } // Foreign Key to Department
        public string? ImgUrl { get; set; }
        public IFormFile? Image { get; set; }

        #region Old
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public int Age { get; set; }
        //public string? Address { get; set; }
        //public bool IsActive { get; set; }
        //public string? Email { get; set; }
        //public decimal Salary { get; set; }
        //public DateTime HiringDate { get; set; }
        //public Gender Gender { get; set; }
        //public string? PhoneNumber { get; set; }
        //public EmployeeType EmployeeType { get; set; }
        //public int CreatedBy { get; set; }
        //public int LastModifiedBy { get; set; } 
        #endregion
    }
}
