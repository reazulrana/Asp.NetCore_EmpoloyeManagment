using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
    

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {

            if(ModelState.IsValid)
            {
                IdentityRole Role = new IdentityRole()
                {
                    Name=model.RoleName
                };

                 var result= await roleManager.CreateAsync(Role);
                if(result.Succeeded)
                {
                    return RedirectToAction("ListRoles","Administration");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }


            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous] //Access Permission is for All User
        public IActionResult ListRoles()
        {
            var Roles = roleManager.Roles;


            return View(Roles);
        }


        [HttpGet]
        
        public async Task<IActionResult> EditRole(string id)
        {

            var role = await roleManager.FindByIdAsync(id);
            if(role==null)
            {
                List<string> errors = new List<string>();
                errors.Add("Not Found Error Message");
                errors.Add($"The Role With This Id {id} Not Found");
                ViewBag.ErrorMessage = errors;
                return View("NotFound");
            }

            var model = new EditRoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach(var user in userManager.Users)
            {
                if(await userManager.IsInRoleAsync(user,role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);

        }





        [HttpGet]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {

            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                List<string> errors = new List<string>();
                errors.Add("Not Found Error Message");
                errors.Add($"The Role With This Id {model.Id} Not Found");
                ViewBag.ErrorMessage = errors;
                return View("NotFound");
            }

            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);

                if(result.Succeeded)
                {
                    return View("ListRoles", "Administration");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }

            

        }




        [AcceptVerbs("Post","Get")]
        [AllowAnonymous]
        public async Task<IActionResult> IsRoleInUse(string RoleName)
        {
            var role = await roleManager.FindByNameAsync(RoleName);

            if(role==null)
            {
                return Json(true);
            }
            else
            {
                return Json($"The {role} Role Is Already Created");

            }

        }

    }
}
