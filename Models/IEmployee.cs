using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWeb.Models
{
    public interface IEmployee
    {
        //Get employee by id
        public Employee GetEmployeeById(int Id);
        public IEnumerable<Employee> GetAllEmployee();
        public Employee AddEmployee(Employee emplpyee);
        public void DeleteEmployee(int Id);
        public Employee UpdateEmployee(Employee employee);
        public bool IsValidEmail(string email);
    }
}
