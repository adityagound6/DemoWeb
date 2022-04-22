using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoWeb.Models;

namespace DemoWeb.Models
{
    public class MockEmployeeRepositry : IEmployee
    {
        //MockDepartmentRepositry department;
        private List<Employee> _EmployeeList;
        public MockEmployeeRepositry()
        {
            _EmployeeList = new List<Employee>()
            {
                new Employee() {EmployeeId = 1,EmployeeName="Aditya", Department = Dept.DotNet,Salary = 32455},
                new Employee() {EmployeeId = 2,EmployeeName="Ayushi", Department = Dept.DataAnalysis,Salary = 124550 },
                new Employee() {EmployeeId = 3,EmployeeName="Kuanl", Department = Dept.CppDevelopment,Salary = 2455 },
                new Employee() {EmployeeId = 4,EmployeeName="Surya", Department = Dept.DataScience,Salary = 52455 },
                new Employee() {EmployeeId = 5,EmployeeName="Ankita", Department = Dept.BDA,Salary = 100 }
            };
        }

        //Get employee by id
        public Employee GetEmployeeById(int Id)
        {
            return _EmployeeList.FirstOrDefault(e => e.EmployeeId == Id);
            //throw new NotImplementedException();
        }


        //Delete a employee
        public void DeleteEmployee(int Id)
        {
            //Employee employee = GetEmployeeById(Id);
            _EmployeeList.RemoveAll(e => e.EmployeeId == Id);
        }
        
        //Add to new employee
        public Employee AddEmployee(Employee employee)
        {
            employee.EmployeeId =  _EmployeeList.Max(e => e.EmployeeId) + 1;
            _EmployeeList.Add(employee);
            return employee;
        }

        //Get All Employee 
        IEnumerable<Employee> IEmployee.GetAllEmployee()
        {
            return _EmployeeList;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            var _employee = _EmployeeList.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();
            //_employee.EmployeeId = employee.EmployeeId;
            if (_employee != null)
            {
                //_employee.EmployeeId = employee.EmployeeId;       
                _employee.EmployeeName = employee.EmployeeName;
                _employee.Salary = employee.Salary;
                //return employee;
            }
            //Console.Write(_employee.)
            return _employee;
        }

        public bool IsValidEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
