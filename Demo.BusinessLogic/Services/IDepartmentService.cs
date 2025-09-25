using Demo.BusinessLogic.DataTransfareObject.DepartmentDtos;

namespace Demo.BusinessLogic.Services
{
    public interface IDepartmentService
    {
        int AddDepartment(AddDepartmentDto AddDepartmentDto);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentById(int id);
        bool Remove(int id);
        int UpdateDepartment(UpdateDepartmentDto UpdateDepartmentDto);
    }
}