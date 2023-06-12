using EmployeeManagement.Models;
using EmployeeManagement.Utility;
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

    [Authorize(Roles = "Admin,Manager")]
    
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
        //[AllowAnonymous] //Access Permission is for All User
        [Authorize(Roles ="Admin")]
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





        [HttpPost]

        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {

            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
            
                ViewBag.ErrorMessage = GlobalFunction.IdNotFound(model.Id);
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

        [HttpGet]
        public async Task<IActionResult> EditUserInRole(string roleId)
        {

            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if(role==null)
            {
                ViewBag.ErrorMessage = GlobalFunction.IdNotFound(roleId);
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();
                

            foreach(var user in userManager.Users)
            {

                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if(await userManager.IsInRoleAsync(user,role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);


        }



        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> EditUserInRole(List<UserRoleViewModel>model, string roleId)
        {

            var role = await roleManager.FindByIdAsync(roleId);

            if(role==null)
            {
                ViewBag.ErrorMessage = GlobalFunction.IdNotFound(roleId);
                return View("NotFound");
            }


            for(int i=0; i<model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if(result.Succeeded)
                {
                    if (i < (model.Count - 1))
                    
                        continue;
                    else
                        return RedirectToAction("EditRole", new { id = roleId });
                
                }
            }

            return RedirectToAction("EditRole",new {id=roleId });


        }


    }
}
