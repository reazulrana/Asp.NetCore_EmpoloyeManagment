using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class EmployeeCreateViewModel
    {

        [Required]
        [MaxLength(50, ErrorMessage = "Name Field Max Charcter Length is 50")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Name")]
        [Display(Name = "Office Mail")]
        public string Email { get; set; }

        [Required]
        public Dept? Department { get; set; }
        public IFormFile Photo { get; set; }
        //public List<IFormFile> Photos { get; set; }
    }
}
