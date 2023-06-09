using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public static class ModelBiuilderExtension
    {

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(

                new Employee
                {
                    id = 2,
                    Name = "Moshumee Mollika Moue",
                    Email = "mou@gmail.com",
                    Department = Dept.Marketing

                },
                new Employee
                {
                    id = 3,
                    Name = "Ibrahim Rihan Ayat",
                    Email = "Rihan@gmail.com",
                    Department = Dept.IT

                },
                new Employee
                {
                    id = 4,
                    Name = "Hena",
                    Email = "hena@gmail.com",
                    Department = Dept.HR

                },
                new Employee
                {
                    id = 5,
                    Name = "Safia",
                    Email = "safia@gmail.com",
                    Department = Dept.HR

                },
                new Employee
                {
                    id = 6,
                    Name = "Safin",
                    Email = "safin@gmail.com",
                    Department = Dept.Marketing

                },
                new Employee
                {
                    id = 7,
                    Name = "Ayan",
                    Email = "ayan@gmail.com",
                    Department = Dept.Marketing

                }
                );
        }
    }
}
