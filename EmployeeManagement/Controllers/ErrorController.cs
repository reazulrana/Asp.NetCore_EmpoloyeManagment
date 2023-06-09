using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statuscode}")]
        public IActionResult HttpRequestCodeHandler(int statuscode)
        {
            List<string> errordeitals = new List<string>();
            switch(statuscode)
            {
                case 404: 
                    errordeitals.Add("404 Page Not Found");
                    errordeitals.Add("Sorry resource you find could not found");
                    ViewBag.ErrorMessage = errordeitals;
                    break;
            }
            return View("NotFound");
        }
    }
}
