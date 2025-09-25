using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Demo.BusinessLogic.DataTransfareObject.DepartmentDtos;
using Demo.BusinessLogic.DataTransfareObject.EmployeeDtos;
using Demo.DataAccesse.Models.EmployeeModel;
using Demo.DataAccesse.Models.Shared.Enums;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Demo.BusinessLogic.Factories
{
    public static class EmployeeFactories
    {
        //public static GetEmployeesDto ToGetEmployeesDto(this Employee Emp)
        //{
        //    return new GetEmployeesDto()
        //    {
        //        Id = Emp.Id,
        //        Name = Emp.Name,
        //        Age = Emp.Age,
        //        Email = Emp.Email,
        //        IsActive = Emp.IsActive,
        //        Salary = Emp.Salary,
        //        Gender = Emp.Gender.ToString(),
        //        EmployeeType = Emp.EmployeeType.ToString()
        //    };
        //}


        public static EmployeeByIdDto ToEmployeeByIdDto (this Employee Emp)
        {
            return new EmployeeByIdDto()
            {
                Id = Emp.Id,
                Name = Emp.Name,
                Age = Emp.Age,
                Email = Emp.Email,
                IsActive = Emp.IsActive,
                Salary = Emp.Salary,
                Gender = Emp.Gender.ToString(),
                EmployeeType = Emp.EmployeeType.ToString(),
                Address = Emp.Address,
                HiringDate = DateOnly.FromDateTime(Emp.HiringDate),
                PhoneNumber = Emp.PhoneNumber,
                CreatedBy = 1,
                LastModifiedBy = 1,
                CreatedOn = Emp.CreatedOn,
                LastModifiedOn = Emp.LastModifiedOn
            };
        }

        #region Old Extention Methods 
        //public static Employee ToEntity( this AddEmployeeDto E)
        //{
        //    return new Employee()
        //    {
        //        Name = E.Name,
        //        Age = E.Age,
        //        Email = E.Email,
        //        IsActive = E.IsActive,
        //        Salary = E.Salary,
        //        Gender = E.Gender,
        //        EmployeeType = E.EmployeeType,
        //        Address = E.Address,
        //        HiringDate = E.HiringDate,
        //        PhoneNumber = E.PhoneNumber,
        //        CreatedBy = E.CreatedBy,
        //        LastModifiedBy = E.LastModifiedBy

        //    };

        //}



        //public static Employee ToEntity(this UpdateEmployeeDto E)
        //{
        //    return new Employee()
        //    {
        //        Id = E.Id, // Assuming Id is part of UpdateEmployeeDto
        //        Name = E.Name,
        //        Age = E.Age,
        //        Email = E.Email,
        //        IsActive = E.IsActive,
        //        Salary = E.Salary,
        //        Gender = E.Gender,
        //        EmployeeType = E.EmployeeType,
        //        Address = E.Address,
        //        HiringDate = E.HiringDate,
        //        PhoneNumber = E.PhoneNumber,
        //        CreatedBy = E.CreatedBy,
        //        LastModifiedBy = E.LastModifiedBy
        //    };
        //}

        #endregion







    }
}
