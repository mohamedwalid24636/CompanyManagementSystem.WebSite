using Demo.BusinessLogic.DataTransfareObject.EmployeeDtos;
using Demo.BusinessLogic.Services;
using Demo.DataAccesse.Models.EmployeeModel;
using Demo.DataAccesse.Models.Shared.Enums;
using Demo.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Demo.Presentation.Controllers
{
    [Authorize]
    public class EmployeeController (IEmployeeService _EmployeeService , ILogger<EmployeeController> _Logger , IWebHostEnvironment _Environment  /*, IDepartmentService _DepartmentService*/) : Controller
    {
        public IActionResult Index(string? EmployeeSearchName )
        {
            var Employees = _EmployeeService.GetAllEmployees(EmployeeSearchName);
            return View(Employees);
        }

        public IActionResult Create(/*[FromServices]IDepartmentService _DepartmentService*/)  // Injection on the Action Level !! , [FromServices] ال CLR // will inject the IDepartmentService dependency into this action method, allowing you to use it without injecting it into the constructor.
        {
            //ViewData["Departments"] = _DepartmentService.GetAllDepartments();    
            //هروح اعلم انجيكشن في ال بارشيال فيو 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // This attribute is used to prevent CSRF attacks by validating the request token only from browser "cant use postman" .
        public IActionResult Create(EmployeeViewModel EmployeeViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var addEmployeeDto = new AddEmployeeDto()
                    {
                        Name = EmployeeViewModel.Name,
                        Age = EmployeeViewModel.Age,
                        Address = EmployeeViewModel.Address,
                        IsActive = EmployeeViewModel.IsActive,
                        Email = EmployeeViewModel.Email,
                        Salary = EmployeeViewModel.Salary,
                        HiringDate = EmployeeViewModel.HiringDate,
                        Gender = EmployeeViewModel.Gender,
                        PhoneNumber = EmployeeViewModel.PhoneNumber,
                        EmployeeType = EmployeeViewModel.EmployeeType,
                        DepartmentId = EmployeeViewModel.DepartmentId, // Assuming you have a DepartmentId in your EmployeeViewModel
                        Image = EmployeeViewModel.Image,
                    };


                    var result = _EmployeeService.AddEmployee(addEmployeeDto);
                    if (result > 0)
                    {
                       return  RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee Can't be Created");
                        return View(addEmployeeDto);
                    }
                }
                catch (Exception ex)
                {
                    if (_Environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                       _Logger.LogError(ex.Message);
                 
                    }
                }
            }

            return View(EmployeeViewModel);

        }

        public IActionResult Details(int? id)
        {
            if (!id.HasValue)  return BadRequest();                             // Check if id has a value
            else
            {
               var Emp = _EmployeeService.GetEmployeeById(id.Value);
               return Emp is null ? BadRequest(): View(Emp);
            }
         
        }






        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest(); // Check if id has a value
            var Employee = _EmployeeService.GetEmployeeById(id.Value);
            if (Employee is null )return NotFound(); // Check if Employee is null
            var EmployeeViewModel = new EmployeeViewModel
            {
                Name = Employee.Name ,
                Email = Employee.Email,
                Age = Employee.Age,
                IsActive = Employee.IsActive,
                Salary = Employee.Salary,
                Address = Employee.Address,
                HiringDate = Employee.HiringDate,
                PhoneNumber = Employee.PhoneNumber,
                Gender = Enum.Parse<Gender>(Employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(Employee.EmployeeType),
                DepartmentId = Employee.DepartmentId// Assuming DepartmentId is nullable in Employee model

            };
            return View (EmployeeViewModel);
        }

        [HttpPost]
       public IActionResult Edit([FromRoute]int? id , EmployeeViewModel EmployeeViewModel)
        {
            if (!id.HasValue/*||id!=UpdateEmployee.Id*/ )return BadRequest(); // Check if id has a value and if it matches the id in the UpdateEmployeeDto
            if (!ModelState.IsValid) return View(EmployeeViewModel);
            else
            {
                try
                {
                    var UpdateEmployeeDto = new UpdateEmployeeDto()
                    {
                        Id = id.Value, // Use id.Value to get the actual integer value
                        Name = EmployeeViewModel.Name,
                        Age = EmployeeViewModel.Age,
                        Address = EmployeeViewModel.Address,
                        IsActive = EmployeeViewModel.IsActive,
                        Email = EmployeeViewModel.Email,
                        Salary = EmployeeViewModel.Salary,
                        HiringDate = EmployeeViewModel.HiringDate,
                        PhoneNumber = EmployeeViewModel.PhoneNumber,
                        Gender = EmployeeViewModel.Gender,
                        EmployeeType = EmployeeViewModel.EmployeeType,
                        DepartmentId = EmployeeViewModel.DepartmentId, // Assuming you have a DepartmentId in your EmployeeViewModel

                    };
                    var result = _EmployeeService.UpdateEmployee(UpdateEmployeeDto);
                    if (result > 0) return RedirectToAction("Index");
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee Can't be Updated");
                    }
                }
                catch (Exception ex)
                {
                    if (_Environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        _Logger.LogError(ex.Message);
                        return View("ErrorView", ex);
                    }
                }
            }
            return View(EmployeeViewModel); // Return the view with the UpdateEmployeeDto if there is an error or if the model state is invalid
        }


        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {

                bool Deleted = _EmployeeService.RemoveEmployee(id);
                if (Deleted)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Is Not Deleted");
                    return RedirectToAction("Delete", new { id = id });        // خد بالك ملهاش لازمة طالما الميثود مش get (you use Delete Modal)
                }

            }
            catch (Exception ex)
            {
                if (_Environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction("Index");

                }
                else
                {
                    _Logger.LogError(ex.Message);
                    return View("ErrorView", ex);

                }
            }

        }
    }
}
