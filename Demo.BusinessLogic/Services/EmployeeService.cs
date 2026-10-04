using AutoMapper;
using Demo.BusinessLogic.DataTransfareObject.EmployeeDtos;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.AttachementServices;
using Demo.DataAccesse.Models.EmployeeModel;
using Demo.DataAccesse.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Demo.BusinessLogic.Services
{
    public class EmployeeService(/*IEmployeeRepository _EmployeeRepository*/  IUnitOfWork _UnitOfWork , IMapper _mapper , IAttachementService _AttachementService) : IEmployeeService
    {



        //Get All Employees
        public IEnumerable<GetEmployeesDto> GetAllEmployees(string? EmployeeSearchName /*bool WithTrackting = false*/ )
        {
            IEnumerable<Employee> Employees;

            if (string.IsNullOrWhiteSpace(EmployeeSearchName))
                Employees = _UnitOfWork.EmployeeRepository.GetAll();
            else
                Employees = _UnitOfWork.EmployeeRepository.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));

            var GetEmployessDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<GetEmployeesDto>>(Employees);
            return GetEmployessDto;


            //Regoin 01
            //IQuerable Vs IEnumerable
            // خد بالك هنا اننا بنستخدم IQuerable عشان نعمل Select على ال Employee و بعدين بنستخدم IEnumerable عشان نعمل Where على ال Age
            //var Data = _EmployeeRepository.GetAll(E => new Employee()
            //{
            //    Id = E.Id,
            //    Name = E.Name,
            //    Age = E.Age,
            //    Salary = E.Salary,
            //}).Where(E => E.Age > 25);  //iEnumerable 'where' after the select 'IQuerable' statement to filter employees older than 25 


            // Regoin 02
            //var Data = _EmployeeRepository.GetAll(E=>E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            //var GetEmployessDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<GetEmployeesDto>>(Data);
            //return GetEmployessDto;




        }

        // Get Employee By Id
        public EmployeeByIdDto? GetEmployeeById(int id)
        {
            var Employee = _UnitOfWork.EmployeeRepository.GetById(id);
            //if (Employee == null) return null;
            //else
            //{
            //    return Employee.ToEmployeeByIdDto();
            //}
            return Employee == null ? null : _mapper.Map<Employee, EmployeeByIdDto>(Employee); // Using AutoMapper to map Employee to EmployeeByIdDto 
        }



        // Add Employee
        public int AddEmployee(AddEmployeeDto AddEmployeeDto)
        {
            var Employee = _mapper.Map<AddEmployeeDto, Employee>(AddEmployeeDto);
          
                                                 //locally     // Delete   // Update 

            if (AddEmployeeDto.Image is not null )
            Employee.ImageName = _AttachementService.Upload(AddEmployeeDto.Image ,"Images");     
          
           _UnitOfWork.EmployeeRepository.insert(Employee);
            return _UnitOfWork.SaveChanges();
        }

        //Update Employee
        public int UpdateEmployee( UpdateEmployeeDto Dto)
        {
            if (Dto.ImgUrl != null && Dto.Image != null)      //PictureName != null  ===>  User already has picture   &&   //Image != null ===> User want to update his picture
            {                                                                         // بيقا لازم تمسح الصورة القذيمة الاول
                _AttachementService.Delete(Dto.ImgUrl);
            }
            if (Dto.Image != null)                                          //Image != null ===> User want to update his picture 
            {                                                                         // يبقا هترفع صورة جذيذه بعذ ما مسحت القذيمة     او     مفيش صورة قذيمة اصلا و هتضيف واخذه جذيذه علي طول  
                var ImagePath = _AttachementService.Upload(Dto.Image , "Images");
                if (ImagePath == null) throw new Exception("Image upload failed");  // ===== >  Handle Exception
                Dto.ImgUrl = ImagePath;
            }
            _UnitOfWork.EmployeeRepository.Update(_mapper.Map<UpdateEmployeeDto , Employee>(Dto));
            return _UnitOfWork.SaveChanges();
        }




        public bool RemoveEmployee(int id)
        {
            var Employee = _UnitOfWork.EmployeeRepository.GetById(id);
            if (Employee is null) return false; // Employee not found
            else
            {
                Employee.IsDeleted = true; // Mark as deleted
                _UnitOfWork.EmployeeRepository.Update(Employee);
                return _UnitOfWork.SaveChanges() > 0 ? true : false; // Return true if removed successfully
            
            }
        }

    }
}
