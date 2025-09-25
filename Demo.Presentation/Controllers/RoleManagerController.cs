using Demo.Presentation.ViewModels.RoleViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Demo.Presentation.Controllers
{
    public class RoleManagerController(RoleManager<IdentityRole> roleManager ,ILogger<EmployeeController> _Logger, IWebHostEnvironment _Environment) : Controller
    {
        public IActionResult Index(string? SearchKey , string? AddKey)
        {
            if (!string.IsNullOrEmpty(AddKey))
            {
                if (ModelState.IsValid)
                {
                    var Result = roleManager.CreateAsync(new IdentityRole() { Name = AddKey }).Result;
                    if (Result.Succeeded) ModelState.AddModelError(string.Empty, "Role Created Successfully");
                    else
                    {
                        foreach (var error in Result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
              
            }
            

            if (string.IsNullOrEmpty(SearchKey)) return View(roleManager.Roles.ToList());
            else return View(roleManager.Roles.Where(r => r.Name.Contains(SearchKey)).ToList());

        }


        [HttpGet]
        public IActionResult Details(string Id)
        {
            var role = roleManager.FindByIdAsync(Id).Result;
            if (role is null) return NotFound();

            else 
                return View(new RoleDetailsViewModel()
                {
                    Id = role.Id,
                    RoleName = role.Name,
                });
        }


        [HttpGet]
        public IActionResult Edit(string Id)
        {
            if (string.IsNullOrEmpty(Id)) return BadRequest();
            var Role = roleManager.FindByIdAsync(Id).Result;
            if (Role is null) return NotFound();
            else
            {
                var RoleEdit = new RoleEditViewModel()
                {
                    Id = Role.Id,
                    RoleName = Role.Name,
                };
                return View(RoleEdit);
            }
        }





        [HttpPost]
        public IActionResult Edit([FromRoute] string Id, RoleEditViewModel ViewModel)
        {
            if (string.IsNullOrEmpty(Id)) return BadRequest();
            if (ModelState.IsValid)
            {
                    try
                    {
                    var Role = roleManager.FindByIdAsync(Id).Result;
                    if (Role is null) return NotFound();
                    else
                    {

                        Role.Name = ViewModel.RoleName;
                        var Result = roleManager.UpdateAsync(Role).Result;

                        if (Result.Succeeded) return RedirectToAction("Index");
                        else
                        {
                            foreach (var error in Result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
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

            return View(ViewModel);


        }

        [HttpGet]
        public IActionResult Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id)) return BadRequest();
            var Role = roleManager.FindByIdAsync(Id).Result;
            if (Role is null) return NotFound();
            else
            {
                var RoleDetails = new RoleDetailsViewModel()
                {
                    Id = Role.Id,
                    RoleName = Role.Name,
                };
                return View(RoleDetails);
            }
        }

        [HttpPost]
        public IActionResult DeleteConfirm(string Id)
        {
            if (string.IsNullOrEmpty(Id)) return BadRequest();
            var Role = roleManager.FindByIdAsync(Id).Result;
            if (Role is null) return NotFound();
            var Result = roleManager.DeleteAsync(Role).Result;
            if (Result.Succeeded)
            {
                TempData["Message"] = "Role Deleted Successfully";
                return RedirectToAction("Index");
            }
            else
            {

                var ErrorList = new List<string>();

                foreach (var error in Result.Errors)
                {
                    ErrorList.Add( error.Description );
                }
                TempData["Message"] = ErrorList;

                return RedirectToAction("Index");
            }
        }






    }
}
