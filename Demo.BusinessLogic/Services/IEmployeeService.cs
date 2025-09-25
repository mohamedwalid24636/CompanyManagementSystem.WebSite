using Demo.BusinessLogic.DataTransfareObject.EmployeeDtos;

namespace Demo.BusinessLogic.Services
{
    public interface IEmployeeService
    {
        IEnumerable<GetEmployeesDto> GetAllEmployees(string? EmployeeSearchName);
        EmployeeByIdDto? GetEmployeeById(int id);
        int AddEmployee(AddEmployeeDto AddEmployeeDto);
        int UpdateEmployee(UpdateEmployeeDto UpdateEmployeeDto);
        bool RemoveEmployee(int id);
    }
}