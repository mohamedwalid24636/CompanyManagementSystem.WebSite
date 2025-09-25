using System.Collections.Generic;
using AspNetCoreGeneratedDocument;
using Demo.DataAccesse.Models.IdentityModels;
using Demo.Presentation.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Demo.Presentation.Controllers
{
    public class UserManagerController(UserManager<ApplicationUser> UserManager, ILogger<EmployeeController> _Logger, IWebHostEnvironment _Environment) : Controller
    {
        public IActionResult Index(string? SearchKey)
        {
            return SearchKey is null ? View ( UserManager.Users.ToList())
                : View(UserManager.Users.Where(u => u.FirstName.Contains(SearchKey) || u.LastName.Contains(SearchKey)).ToList());
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

