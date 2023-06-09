using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signinManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager)
        {
            this.userManager = userManager;
            this.signinManager = signinManager;
        }

        [HttpGet]
        [AllowAnonymous] //Access Permission is for All User

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous] //Access Permission is for All User

        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if(ModelState.IsValid)
            {

                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if(result.Succeeded)
                {
                    await signinManager.SignInAsync(user,isPersistent:false);
                    return RedirectToAction("Index", "Home");

                }
                foreach(var error in result.Errors)
                {

                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }



        [AcceptVerbs("Post","Get")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {

            var user = await userManager.FindByEmailAsync(email);

            if(user==null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is in use");
            }

        }


        [HttpGet]
        [AllowAnonymous] //Access Permission is for All User
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous] //Access Permission is for All User
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model,string returnUrl)
        {

            //string returnUrl = HttpContext.Request.Query["returnURL"];
            if (ModelState.IsValid)
            {

             
                var result = await signinManager.PasswordSignInAsync(model.Email, model.Password,model.RememberMe,false); 

                if (result.Succeeded)
                {

                    if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");

                }
               

                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signinManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



      

    }
}
