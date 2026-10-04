using System.Collections.Generic;
using AspNetCoreGeneratedDocument;
using Demo.DataAccesse.Models.IdentityModels;
using Demo.Presentation.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Demo.Presentation.Controllers
{
    public class UserManagerController(UserManager<ApplicationUser> UserManager, RoleManager<IdentityRole> roleManager, ILogger<EmployeeController> _Logger, IWebHostEnvironment _Environment) : Controller
    {
        public IActionResult Index(string? SearchKey)
        {
            var Users = SearchKey is null ? UserManager.Users.ToList():
                UserManager.Users.Where(u => u.FirstName.Contains(SearchKey) || u.LastName.Contains(SearchKey)).ToList();

            var userRoles = new Dictionary<string, string>();

            foreach (var user in Users)
            {
                var role = (UserManager.GetRolesAsync(user)).Result.FirstOrDefault();
                userRoles[user.Id] = role ?? "No Role";
            }

            ViewBag.UserRoles = userRoles;
            return View(Users);

        }
        public IActionResult Details(string Id)
        {
            var User = UserManager.FindByIdAsync(Id).Result;
            if (User is null) return NotFound();
            else
            {
                var UserDetails = new UserDetailsViewModel
                {
                    Id = User.Id,
                    FName = User.FirstName,
                    LName = User.LastName,
                    Email = User.Email,
                    PhoneNumber = User.PhoneNumber,
                };
                return View(UserDetails);
            }
        }

        [HttpGet]
        public IActionResult Edit(string? Id)
        {
            if (string.IsNullOrEmpty(Id)) return BadRequest();
            var User = UserManager.FindByIdAsync(Id).Result;
            if (User is null) return NotFound();
            else
            {
                var UserEdit = new UserEditViewModel
                {
                    FName = User.FirstName,
                    LName = User.LastName,
                    PhoneNumber = User.PhoneNumber,
                    Role = UserManager.GetRolesAsync(User).Result.FirstOrDefault(),
                    Roles = roleManager.Roles
                          .Select(r => r.Name!)
                          .ToList()
                };
                return View(UserEdit);
            }
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] string? Id, UserEditViewModel ViewModel)
        {

            if (string.IsNullOrEmpty(Id)) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    //var User = new ApplicationUser()
                    //{
                    //    Id = Id,
                    //    FirstName = ViewModel.FName,
                    //    LastName = ViewModel.LName,
                    //    PhoneNumber = ViewModel.PhoneNumber,

                    //};
                    var User = UserManager.FindByIdAsync(Id).Result;
                    if (User is null) return NotFound();
                    User.FirstName = ViewModel.FName;
                    User.LastName = ViewModel.LName;
                    User.PhoneNumber = ViewModel.PhoneNumber;

                   
                        var UserRoles = UserManager.GetRolesAsync(User).Result;
                        if (UserRoles.Count > 0)
                        {
                            var result = UserManager.RemoveFromRolesAsync(User, UserRoles).Result;
                        }

                        if ( !string.IsNullOrEmpty(ViewModel.Role)&&roleManager.RoleExistsAsync(ViewModel.Role).Result)
                        {
                            var result = UserManager.AddToRoleAsync(User, ViewModel.Role).Result;
                        }

                    



                    var Result = UserManager.UpdateAsync(User).Result;
                    if (Result.Succeeded)
                        return RedirectToAction("Index");
                    else
                    {
                        foreach (var Error in Result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, Error.Description);
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
        public IActionResult Delete(string? Id)
        {
            if (string.IsNullOrEmpty(Id)) return BadRequest();
            var User = UserManager.FindByIdAsync(Id).Result;
            if (User is null) return NotFound();
            else return View(new UserDeleteViewModel()
            {
                Id = User.Id,
                FName = User.FirstName,
                LName = User.LastName,
                Email = User.Email,
                PhoneNumber = User.PhoneNumber,
            });

        }


        [HttpPost]
        public IActionResult DeleteConfirm([FromRoute] string Id)
        {
            if (string.IsNullOrEmpty(Id)) return BadRequest();
            try
            {
                var DeletedUser = UserManager.FindByIdAsync(Id).Result;
                if (DeletedUser is null) return NotFound();
                else
                {
                    var Result = UserManager.DeleteAsync(DeletedUser).Result;
                    if (Result.Succeeded)
                    {
                        TempData["Message"] = "User Deleted Successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var Error in Result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, Error.Description);
                        }
                        TempData["Message"] = "User Can't be Deleted";
                        return RedirectToAction("Index");

                    }

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

