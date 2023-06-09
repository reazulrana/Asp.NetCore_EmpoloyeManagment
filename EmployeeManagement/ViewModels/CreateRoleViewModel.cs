using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name ="Role Name")]

        [Remote("IsRoleInUse", "Administration")]
        public string RoleName { get; set; }
    }
}
