using Demo.DataAccesse.Models.EmployeeModel;
using Demo.DataAccesse.Models.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels
{
    public class EmployeeViewModel
    {
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
        [Display (Name = "Department")]
        public int? DepartmentId { get; set; } // Foreign Key to Department
        public IFormFile? Image { get; set; }
    }
}
