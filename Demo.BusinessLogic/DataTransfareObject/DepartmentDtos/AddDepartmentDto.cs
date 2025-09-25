using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DataTransfareObject.DepartmentDtos
{
    public class AddDepartmentDto
    {
        [Required(ErrorMessage ="Name Is Required !!!")]
        public string Name { get; set; } = null!;
        [Required]
        [Range(100,int.MaxValue)]
        public string Code { get; set; } = null!;
        public string Description { get; set; }
        public DateOnly CreatedOn { get; set; }
    }
}
