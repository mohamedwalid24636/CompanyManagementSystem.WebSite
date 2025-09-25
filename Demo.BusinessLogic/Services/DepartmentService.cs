using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BusinessLogic.DataTransfareObject.DepartmentDtos;
using Demo.BusinessLogic.Factories;
using Demo.DataAccesse.Repositories.Interfaces;

namespace Demo.BusinessLogic.Services
{
    public class DepartmentService(IUnitOfWork _unitOfWork) : IDepartmentService
    {
        //private readonly IDepartmentRepository departmentRepository;

        //public DepartmentService(IDepartmentRepository departmentRepository)//لما اطلي منك ابجكت من IDepartmentRepository احقنلي departmentRepository 
        //{
        //    this.departmentRepository = departmentRepository;
        //} //Injection



        //GetALLDepartments 
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var data = _unitOfWork.DepartmentRepository.GetAll(true);
            var Departments = data.Select(D => D.ToDepartmentDto());
            return Departments;
        }


        //public DepartmentDetailsDto? GetDepartmentById(int id)
        //{
        //    var Department = departmentRepository.GetById(id);
        //    if (Department == null) return null;
        //    else
        //    {
        //        var ReturnedDepartment = new DepartmentDetailsDto()
        //        {
        //            name=Department.name,
        //            Id=Department.Id,
        //            Code=Department.Code,
        //            Description=Department.Description,
        //            CreatedBy = Department.CreatedBy,
        //            CreatedOn=DateOnly.FromDateTime(Department.CreatedOn),
        //            IsDeleted=Department.IsDeleted,
        //            LastModifiedBy=Department.LastModifiedBy,
        //            LastModifiedOn=DateOnly.FromDateTime(Department.LastModifiedOn),
        //        };
        //        return ReturnedDepartment;
        //    }


        //}




        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var Department = _unitOfWork.DepartmentRepository.GetById(id);
            return Department is null ? null : Department.ToDepartmentDetailsDto();

        }

        public int AddDepartment(AddDepartmentDto AddDepartmentDto)
        {
            _unitOfWork.DepartmentRepository.insert(AddDepartmentDto.ToEntity());
             return _unitOfWork.SaveChanges();
        }


        public int UpdateDepartment(UpdateDepartmentDto UpdateDepartmentDto)
        {
            _unitOfWork.DepartmentRepository.Update(UpdateDepartmentDto.ToEntity());
            return _unitOfWork.SaveChanges();
        }


        public bool Remove(int id)
        {
            var Department = _unitOfWork.DepartmentRepository.GetById(id);
            if (Department is null) return false;
            else
            {
                _unitOfWork.DepartmentRepository.Remove(Department);
                return _unitOfWork.SaveChanges() > 0 ? true : false;

            }

        }
    }
}
