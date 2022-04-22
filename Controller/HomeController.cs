using DemoWeb.Models;
using DemoWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DemoWeb.HomeController
{
    public class HomeController : Controller
    {
        private readonly IEmployee _Iemployee;
        private readonly AppDbContext appDbContext;
        
        [Obsolete]
        private readonly IHostingEnvironment hostingEnvironment;

        //constractor of home controller
        [Obsolete]
        public HomeController(IEmployee _Iemployee, IHostingEnvironment hostingEnvironment,
                                AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            this._Iemployee = _Iemployee;
            this.hostingEnvironment = hostingEnvironment;
        }

        //List Of all employee and it is home page method
        // "/" or "/home" or "/home/index"
        public ViewResult Index()
        {
            var AllEmployee = _Iemployee.GetAllEmployee();
            return View(AllEmployee);
        }

        public ViewResult IndexCard()
        {
            var AllEmployee = _Iemployee.GetAllEmployee();
            return View(AllEmployee);
        }


        //Detail of particular employee get by id
        // "/home/details/{id}"
        public ViewResult Details(int id)
        {
            HomeDetailViewModelcs homeDetailViewModelcs = new HomeDetailViewModelcs()
            {
                Employee = _Iemployee.GetEmployeeById(id),
                PageTitle = "Details"
            };

           
            //ViewData["Pagtitle"] = "Details";
            //ViewBag.Employee = model;
            return View(homeDetailViewModelcs);
        }


        //Create Method in this we create a new employee
        // "/home/create"
        //It is Get method
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        // Create Method "Post Method"
        [HttpPost]
        [Obsolete]
        public IActionResult Create(CreateEmployeeViewModel model)
        {
            
            //Validation Of field 
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadFile(model);
                Employee newEmployee = new Employee
                {
                    CreateDateTime = DateTime.Now,
                    UpdateDateTime =DateTime.Now,
                    EmployeeName = model.EmployeeName,
                    Department = model.Department,
                    Salary = model.Salary,
                    Email = model.email,
                    PhotoPath = uniqueFileName
                };
                _Iemployee.AddEmployee(newEmployee);
                return RedirectToAction("Details", new { id = newEmployee.EmployeeId });
            }
            else
            {
                return View();
            }
            
        }  

        //Delete a particular employee
        public IActionResult Delete(int id)
        {
            _Iemployee.DeleteEmployee(id);
            return RedirectToAction("Index");
        }

        //Update a particular Row
        [HttpGet]
        public IActionResult Update(int id)
        {
            var model = _Iemployee.GetEmployeeById(id);
            //employee.
            UpdateEmployeeViewModel updateEmployeeViewModel = new UpdateEmployeeViewModel()
            {
                EmoloyeeId = model.EmployeeId,
                EmployeeName = model.EmployeeName,
                Department = model.Department,
                email = model.Email,
                GetEmail = model.Email,
                Salary = model.Salary,
                ExistingPhotoPath = model.PhotoPath,
                UpdateEmail = model.Email
            };
            return View(updateEmployeeViewModel);
        }
        [HttpPost]
        [Obsolete]
        public IActionResult Update(UpdateEmployeeViewModel model)
        {
            //Validation Of field 
            if (ModelState.IsValid)
            {
                Employee employee = _Iemployee.GetEmployeeById(model.EmoloyeeId);
                employee.EmployeeName = model.EmployeeName;
                employee.Department = model.Department;
                employee.Salary = model.Salary;
                employee.UpdateDateTime = DateTime.Now;
                employee.Email = model.UpdateEmail;
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string fileParh = Path.Combine(hostingEnvironment.WebRootPath, "Image", model.ExistingPhotoPath);
                        System.IO.File.Delete(fileParh);
                    }
                    employee.PhotoPath = ProcessUploadFile(model);
                }
                _Iemployee.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        [Obsolete]
        private string ProcessUploadFile(CreateEmployeeViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string photoUpload = Path.Combine(hostingEnvironment.WebRootPath, "Image");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(photoUpload, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        

        
        [AcceptVerbs("Post", "Get")]
        public IActionResult isEmailInUse(string email)
        {
            var employee = _Iemployee.IsValidEmail(email);
            if (!employee)
            {
                return Json(true);
            }
            else
            {
                return Json($"The email {email} is exit");
            }
        }

        [AcceptVerbs("Post", "Get")]
        public IActionResult VerifyEmail(string GetEmail,string updateEmail)
        {
            if(GetEmail != updateEmail)
            {
                return isEmailInUse(updateEmail);
            }
            else
            {
                return Json(true);
            }
        }

        [Obsolete]
        public IActionResult DeletePhoto(int id)
        {   
            Employee employee = _Iemployee.GetEmployeeById(id);
            if (employee.PhotoPath != null)
            {
                string fileParh = Path.Combine(hostingEnvironment.WebRootPath, "Image",employee.PhotoPath);
                System.IO.File.Delete(fileParh);
                return RedirectToAction("Update", new { id = employee.EmployeeId });
            }
            return RedirectToAction("Update", new { id = employee.EmployeeId } );
        }
    }
}
