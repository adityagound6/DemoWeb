using DemoWeb.Models;
using DemoWeb.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWeb
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        //Method to check email is already registeer or not
        [AllowAnonymous]
        [AcceptVerbs("Post","Get")]
        public async Task<IActionResult> isEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"The email {email} is exit");
            }
        }

        //Mothod For registeration or signup get method
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        //Post Method to save registration
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email,Email=model.Email, 
                    City=model.City,CreateDateTime = DateTime.Now};
                var result = await userManager.CreateAsync(user, model.Password);

                if(result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }

        //Metthod for logout
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        //Get Method For Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult LogIn()
        {
            return View();
        }


        //Post Method for login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn(LogInViewModel model,string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email,model.Password,
                    model.RememberMe,false);

                if (result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    //return RedirectToAction("Index", "Home");
                }
                
                ModelState.AddModelError(string.Empty, "Either username or password wrong");
                

            }
            return View(model);
        }
        
    }
}
