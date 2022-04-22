using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWeb.Models
{
    public class SqlEmployeeRepositry : IEmployee
    {
        private readonly AppDbContext appDbContext;
        public SqlEmployeeRepositry(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        //Add Employee
        public Employee AddEmployee(Employee emplpyee)
        {
            Employee employee1 = new Employee();
            employee1.CreateDateTime = DateTime.Now;
            employee1.UpdateDateTime = DateTime.Now;
            appDbContext.Employees.Add(emplpyee);
            appDbContext.SaveChanges();
            return emplpyee;
        }


        //Delete a employee
        public void DeleteEmployee(int Id)
        {
            Employee employee = appDbContext.Employees.Find(Id);
            if(employee != null)
            {
                appDbContext.Employees.Remove(employee);
                appDbContext.SaveChanges();
            }
        }

        //Get All Employee
        public IEnumerable<Employee> GetAllEmployee()
        {
            return appDbContext.Employees;
        }

        //Get Employee by id
        public Employee GetEmployeeById(int Id)
        {
            return appDbContext.Employees.Find(Id);
        }

        //Validatin of email

        public bool IsValidEmail(string email)
        {
            var emloyee = appDbContext.Employees.Where(e => e.Email == email).FirstOrDefault();
            if(emloyee == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Update the employee data
        public Employee UpdateEmployee(Employee employee)
        {
            var _employee = appDbContext.Employees.Attach(employee);
            _employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            appDbContext.SaveChanges();
            return employee;
        }

    }
}
