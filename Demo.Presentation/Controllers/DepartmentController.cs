using AspNetCoreGeneratedDocument;
using Demo.BusinessLogic.DataTransfareObject;
using Demo.BusinessLogic.DataTransfareObject.DepartmentDtos;
using Demo.BusinessLogic.Services;
using Demo.DataAccesse.Repositories;
using Demo.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Demo.Presentation.Controllers
{// الحاجه اللي بترحجعك علي http post علي طول 
 // هي ال (submit Form)
    public class DepartmentController(IDepartmentService _departmentService,
        ILogger<DepartmentController> _Logger // اللي هيكون ماسك ال اكسبشنز اللي هتحصل في ال ديبراتمنت كونترولر 
        , IWebHostEnvironment _environment  /*بعرف انا في انهي انفايرونمنت*/ ) : Controller
    {


        public IActionResult Index()
        {
            // viewData , ViewBag ==> they work on the same Dictionary  !   , used To send Additional data From Action To View 

            //ViewData["Message"] = "Hello From View Data";   //Object  
            //ViewBag.Message = "Hello From View Bag ";       //Dynamic 

            ViewData["Message"] = new DepartmentDto { name = "Mohamed Walid " }; //Object
            ViewBag.Message = new DepartmentDto { name = "Mohamed Walid"}; //Dynamic  

            var Departments = _departmentService.GetAllDepartments().ToList();
            return View(Departments);
        }

        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(DepartmentViewModel DepartmentViewModel)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                try
                {

                    var DepartmentDto = new AddDepartmentDto()   
                    {
                        Name = DepartmentViewModel.Name,
                        Code = DepartmentViewModel.Code,
                        Description = DepartmentViewModel.Description,
                        CreatedOn = DepartmentViewModel.CreatedOn,
                    };

                    int Result = _departmentService.AddDepartment(DepartmentDto);
                    string message;
                    if (Result > 0)
                            message = $"Department { DepartmentViewModel.Name } Is Created Successfully";
                    else 
                            message = $"Department {DepartmentViewModel.Name } Is Not Created";

                    TempData["Message"] = message; // TempData is used to pass data between actions, it will be available for the next request only
                    return RedirectToAction("Index" ); // Redirect to Index Action with a message


                    // if (Result > 0)
                    //{
                    //    return RedirectToAction("Index");  // لان ال return view ---- هترجعك علي صفحة الفيو علي طول بدون ما تعدي علي الاكشن 
                    //}
                    //else
                    //{
                    //    ModelState.AddModelError(string.Empty, "Departmnet Can't Be Created");
                    //}
                }
                catch (Exception ex)
                {

                    if (_environment.IsDevelopment())
                    { //Devolopment
                        ModelState.AddModelError(string.Empty, ex.Message);
                        
                    }
                    else
                    { //deployment -- Event Viewer 
                        _Logger.LogError(ex.Message);
                    }

                }
            }
                return View(DepartmentViewModel);
        }


        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();  // has value function exist because the action recieves int? id (nullable integer)
            var Department = _departmentService.GetDepartmentById(id.Value);
            if(Department is null) return NotFound();
            return View (Department);
        }


        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var Department = _departmentService.GetDepartmentById(id.Value);
            if (Department is null) return NotFound();
            var DepartmnetEditeViewModel = new DepartmentViewModel()
            {
                Name = Department.name,
                Code=Department.Code,
                Description=Department.Description,
                CreatedOn=Department.CreatedOn,

            };
            return View (DepartmnetEditeViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int? id ,DepartmentViewModel ViewModel)
        {
            if (!id.HasValue) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var UpdatedDepartment = new UpdateDepartmentDto()
                    {
                        Id = id.Value,
                        Name = ViewModel.Name,
                        Code = ViewModel.Code,
                        Description = ViewModel.Description,
                        CreatedOn = ViewModel.CreatedOn,
                    };
                    var Result = _departmentService.UpdateDepartment(UpdatedDepartment);
                    if (Result > 0) return RedirectToAction("Index");
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Is not updated ");
                    }

                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
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
            return View(ViewModel);

        }

        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var Department = _departmentService.GetDepartmentById(id.Value);
        //    if (Department is null) return NotFound();
        //    return View(Department);

        //}
        [HttpPost]
        public IActionResult Delete(int id)
        {  if (id == 0) return BadRequest();
                try
                {

                    bool Deleted = _departmentService.Remove(id);
                    if (Deleted)
                        return RedirectToAction("Index");
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Is Not Deleted");
                        return RedirectToAction("Delete", new { id = id });
                    }

                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
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
