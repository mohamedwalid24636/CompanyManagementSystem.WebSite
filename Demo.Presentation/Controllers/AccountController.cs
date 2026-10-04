using System.Configuration;
using Demo.DataAccesse.Models.IdentityModels;
using Demo.Presentation.Utilities;
using Demo.Presentation.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Identity.Client;

namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _UserManager, SignInManager<ApplicationUser> _signinManager) : Controller   //UserManager<ApplicationUser>   ===> Services of the Package <User that i use>

    {
        //Register 
        public IActionResult Register() => View();
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);
            var User = new ApplicationUser()
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
                // Notic That u Didn't Assign IsActive property

            };
            var Result = _UserManager.CreateAsync(User, registerViewModel.Password).Result;      // CreateAsync Returns => Result => Successed Or Errors 
            //==>>  USE .Result Or Await    if U Use Async
            //==>> CreateAsync Will Do Hashing To the Paasword  
            //==> بلغي كلمة task  اللي راجعالي بال  .Result



            if (Result.Succeeded) return RedirectToAction("Login");
            else
            {
                foreach (var Error in Result.Errors)    
                {
                    ModelState.AddModelError(string.Empty, Error.Description);
                }
                return View(registerViewModel);
            }
        }


        #region Login Steps
        // 1- Check ModelState
        // 2- Find User By Email
        // 3- Check if User is not null
        // 4- Check Password
        // 5- Sign In
        // 6- Check Sign In Result
        // 7- Redirect to Home
        //Notic That Login Will Generate Token And Will Be Saved In Application Memmory Cookies (Google) عشان مش كل شوية اعمل Login



        #endregion

        //Login
        public IActionResult Login() => View();


        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);
            var User = _UserManager.FindByEmailAsync(loginViewModel.Email).Result;
            if (User is not null)
            {
                bool Flag = _UserManager.CheckPasswordAsync(User, loginViewModel.Password).Result;         //CheckPasswordAsync() Returns ==> true or false         
                if (Flag)
                {
                    var Result = _signinManager.PasswordSignInAsync(User, loginViewModel.Password, loginViewModel.RememberMe, false).Result;        //PasswordSignInAsync() Returns ==> SignInResult     may have (isNotAllowed , isLockedOut , Succeeded , TwoFacotorRequired )
                    if (Result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Not Allowed");
                    if (Result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Lockout");
                    if (Result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                }


            }
            else
            {
                //invalid Email
                ModelState.AddModelError(string.Empty, "Invalid Email Or Password");

            }
            return View(loginViewModel);

        }




        public IActionResult ForgetPassword() => View();

        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {

                var User = _UserManager.FindByEmailAsync(ViewModel.Email).Result;
                if (User is not null)
                {


                    //BaseUrl/Account/ResetPassword/Email=mohamedwaled24636@gmail.com&Token=
                    var Token = _UserManager.GeneratePasswordResetTokenAsync(User).Result;   //Generate Token ForThis User 
                                                                                             //Dont Forget to Add Token Service

                    var ResetPasswordUrl = Url.Action("ResetPassword", "Account", new { email = ViewModel.Email, Token = Token }, Request.Scheme);
                    //(Action , Controller , Params , BaseUrl ) ;  

                    var email = new Email()
                    {
                        To = ViewModel.Email,
                        Subject = "Reset Password",
                        Body = ResetPasswordUrl
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction("CheckYourInbox"); // صفحة بتقولك راجع ايميلك
                }

            }
            ModelState.AddModelError(string.Empty, "Invalid operation");
            return View("ForgetPassword", ViewModel);

        }


        public IActionResult CheckYourInbox() => View();



        [HttpGet]  // By Default if the request comes from browser it will be  HttpGet 
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }







        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid) return View();
            string email = TempData["email"] as string ?? string.Empty;
            string token = TempData["token"] as string ?? string.Empty;
            var User = _UserManager.FindByEmailAsync(email).Result;
            // now u Can Call ResetPassword Function 


            if (User is not null)
            {
                var Result = _UserManager.ResetPasswordAsync(User, token, viewModel.Password).Result;
                if (Result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var Error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, Error.Description);
                        ModelState.AddModelError(string.Empty, "You Changed the Token Fucker");
                    }
                }
            }

            return View(viewModel);
         




        }


        [HttpPost]
        public IActionResult Logout()
        {
            _signinManager.SignOutAsync().Wait();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        //I Need  ==>>  User  , Token , New Password 




        //دلوقتي ال Token بيتكون باستخدام ال id فلما بكون عاوز اتاكد ان اليوزر ده هو اللي بعتلي الريست باسورد بفك ال token و بيتطلعلي نفس ال id user 


        // token  بيستخدم مره واحده اذاي ?
        // ال token بيتكون باستخدام 3 حاجات 
        // 1- User Id
        // 2- Expiration Time
        // 3- Security Stamp  (بيتم تغيرها لما اليوزر يغير الباسورد بتاعته )

        // لما ال resetpassword function succeeded  بتغير قيمة ال security stamp  و بالتالي ال token اللي اتبعت قبل كده بيبقي مش صالح للاستخدام تاني   
        //لما يجي يفك تشفير ال token 

    }
}
