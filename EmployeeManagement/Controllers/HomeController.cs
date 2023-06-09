using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {

        private readonly IEmployeeRepository _IEmployeeRepository;
        private readonly IHostingEnvironment hostEnvironment;

        public HomeController(IEmployeeRepository IEmployeeRepository, IHostingEnvironment hostingEnvironment)
        {
            _IEmployeeRepository = IEmployeeRepository;
            this.hostEnvironment = hostingEnvironment;
        }

        [AllowAnonymous] //Access Permission is for All User
        public IActionResult Index()
        {

            List<Employee> employeelist = _IEmployeeRepository.GetAllEmployee().ToList();
            //return "Hello World";
            return View(employeelist);
        }

        [AllowAnonymous] //Access Permission is for All User
        public IActionResult Details(int? id)
        {
            HomeDetailsViewModel _homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = this._IEmployeeRepository.GetEmployee(id ?? 1),
                PageTitle = "Employee Details"

            };

            //return "Hello World";
            return View(_homeDetailsViewModel);
        }


        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]

        public IActionResult Create(EmployeeCreateViewModel model)
        {

            if (ModelState.IsValid == true)
            {
                string uniqueFileName = null;
                //if (model.Photos != null && model.Photos.Count() > 1)
                    if (model.Photo != null)

                    {

                uniqueFileName = UploadFile(model);
        
                }

                Employee newEmployee = new Employee();

                newEmployee.Name = model.Name;
                newEmployee.Email = model.Email;
                newEmployee.Department = model.Department;
                newEmployee.PhotoPath = uniqueFileName;


                _IEmployeeRepository.Add(newEmployee);
                return RedirectToAction("Details", new { id = newEmployee.id });
            }

            return View();
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee employee = _IEmployeeRepository.GetEmployee(id);

            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id);

            }

            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel()
            {
                id = employee.id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath,
            };


            return View(employeeEditViewModel);
        }




        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {

            if (ModelState.IsValid == true)
            {
                string uniqueFileName = null;
                //if (model.Photos != null && model.Photos.Count() > 1)
                if (model.Photo != null)
                {
                    if(model.ExistingPhotoPath!=null)
                    {
                        string oldfilename = Path.Combine(hostEnvironment.WebRootPath, "Images", model.ExistingPhotoPath);
                        System.IO.File.Delete(oldfilename);
                    }

                    uniqueFileName = UploadFile(model);
                }

                Employee updateEmployee = _IEmployeeRepository.GetEmployee(model.id);

                updateEmployee.Name = model.Name;
                updateEmployee.Email = model.Email;
                updateEmployee.Department = model.Department;
                updateEmployee.PhotoPath = uniqueFileName;


                _IEmployeeRepository.Update(updateEmployee);
                return RedirectToAction("Index");
            }

            return View();

       
        }


        private string UploadFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName;
            //for single images uploading
            string uploadFolder = Path.Combine(hostEnvironment.WebRootPath, "Images");
            uniqueFileName = Guid.NewGuid() + "_" + model.Photo.FileName;
            string fullpath = Path.Combine(uploadFolder, uniqueFileName);

            using(FileStream filestream=new FileStream(fullpath,FileMode.Create)) 
            { 
            model.Photo.CopyTo(filestream);
            }

            //for multiple images uploading
            //foreach (IFormFile photo in model.Photos)
            //{
            //    string uploadFolder = Path.Combine(hostEnvironment.WebRootPath, "Images");
            //    uniqueFileName = Guid.NewGuid() + "_" + photo.FileName;
            //    string fullpath = Path.Combine(uploadFolder, uniqueFileName);
            //    photo.CopyTo(new FileStream(fullpath, FileMode.Create));
            //}
            return uniqueFileName;
        }
    }
}
