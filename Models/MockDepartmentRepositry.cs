using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWeb.Models
{
    public class MockDepartmentRepositry : IDepartment
    {
        private  List<Department> _department;
        public MockDepartmentRepositry()
        {
            _department = new List<Department>()
            {
                new Department() {DepartmentID = 1,DepartmentName="DotNet" },
                new Department() {DepartmentID = 2,DepartmentName="C++ Developer" },
                new Department() {DepartmentID = 3,DepartmentName="Data Science" },
                new Department() {DepartmentID = 4,DepartmentName="Data Analyses" },
                new Department() {DepartmentID = 5,DepartmentName="BDA" },
            };
        }
        public Department GetDepartmentById(int Id)
        {
            return _department.FirstOrDefault(k => k.DepartmentID == Id);
        }
    }
}
