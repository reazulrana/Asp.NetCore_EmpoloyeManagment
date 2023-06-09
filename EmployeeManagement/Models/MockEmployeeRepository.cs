using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {

        private readonly List<Employee> _employeeList;
        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee(){ id=1, Name="Rana", Department=Dept.IT, Email="rana12312344@gmail.com"},
                new Employee(){ id=2, Name="Mou", Department=Dept.HR, Email="mou12312344@gmail.com"},
                new Employee(){ id=3, Name="Ayat", Department=Dept.Marketing, Email="mou12312344@gmail.com"}


            };
        }

        public Employee Add(Employee employee)
        {
            employee.id = _employeeList.Max(e => e.id) + 1;
            _employeeList.Add(employee);

            return employee;
            //throw new NotImplementedException();
        }

        public Employee Delete(int id)
        {
            Employee employee = _employeeList.FirstOrDefault(x => x.id == id);
            if(employee!=null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }

        public List<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(e => e.id == id);
        }

        public Employee Update(Employee employeeChanges)
        {
            Employee employee = _employeeList.FirstOrDefault(x => x.id == employeeChanges.id);
            if (employee != null)
            {
                employee.Name = employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;
            }
            return employee;
        }
    }
}
