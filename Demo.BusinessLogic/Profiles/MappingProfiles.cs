using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Demo.BusinessLogic.DataTransfareObject.EmployeeDtos;
using Demo.DataAccesse.Models.EmployeeModel;
using Demo.DataAccesse.Models.IdentityModels;

namespace Demo.BusinessLogic.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Employee, GetEmployeesDto>()
                .ForMember(Dest => Dest.EmpGender, Options => Options.MapFrom(Src => Src.Gender))
                .ForMember(Dest => Dest.EmpType, Options => Options.MapFrom(Src => Src.EmployeeType))
                .ForMember(Dest => Dest.Department, Options => Options.MapFrom(Src => Src.Department != null ? Src.Department.name : null));



            ;
            CreateMap<Employee, EmployeeByIdDto>()
            .ForMember(Dest => Dest.Gender, Options => Options.MapFrom(Src => Src.Gender))
            .ForMember(Dest => Dest.EmployeeType, Options => Options.MapFrom(Src => Src.EmployeeType))
            .ForMember(Dest => Dest.HiringDate, Options => Options.MapFrom(Src => DateOnly.FromDateTime(Src.HiringDate)))
            .ForMember(Dest => Dest.Department, Options => Options.MapFrom(Src => Src.Department != null ? Src.Department.name : null))
            .ForMember(Dest => Dest.Image, Options => Options.MapFrom(Src => Src.ImageName));




            CreateMap<AddEmployeeDto, Employee>() //.ReverseMap() move this line to the end of the class if you want to use ReverseMap
            .ForMember(Dest => Dest.HiringDate, Options => Options.MapFrom(Src => Src.HiringDate.ToDateTime(TimeOnly.MinValue)));




            CreateMap<UpdateEmployeeDto, Employee>()
            .ForMember(Dest => Dest.HiringDate, Options => Options.MapFrom(Src => Src.HiringDate.ToDateTime(TimeOnly.MinValue)))
            .ForMember(Dest => Dest.ImageName, Options => Options.MapFrom(Src => Src.ImgUrl));





        }
    }
}
