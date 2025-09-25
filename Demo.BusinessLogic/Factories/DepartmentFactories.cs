using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Demo.BusinessLogic.DataTransfareObject.DepartmentDtos;
using Demo.DataAccesse.Models.DepartmentModel;

namespace Demo.BusinessLogic.Factories
{
    public static class DepartmentFactories
    {
        public static DepartmentDto ToDepartmentDto( this Department D )
        {
            return new DepartmentDto()
            {

                DeptId = D.Id,
                Code = D.Code,
                Description = D.Description,
                name = D.name,
                DateOfCreation = DateOnly.FromDateTime(D.CreatedOn)
            };
        }


        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto()
            {
                name = department.name,
                Id = department.Id,
                Code = department.Code,
                Description = department.Description,
                CreatedBy = department.CreatedBy,
                CreatedOn = DateOnly.FromDateTime(department.CreatedOn),
                IsDeleted = department.IsDeleted,
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn)
            };
        }

        public static Department ToEntity(this AddDepartmentDto D)
        {
            return new Department()
            {
                CreatedOn = D.CreatedOn.ToDateTime(new TimeOnly()),
                Description = D.Description,
                name=D.Name,
                Code= D.Code,

            };
        }

        public static Department ToEntity(this UpdateDepartmentDto D)
        {
            return new Department()
            {
                Id = D.Id,
                CreatedOn = D.CreatedOn.ToDateTime(new TimeOnly()),
                Description = D.Description,
                name = D.Name,
                Code = D.Code,

            };
        }

    }
}
