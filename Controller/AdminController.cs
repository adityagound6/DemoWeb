using DemoWeb.Models;
using DemoWeb.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DemoWeb
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public AdminController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        //Create Role get method

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        //create role post method
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            IdentityRole identityRole = new IdentityRole { Name = model.RollName};
            IdentityResult result = await roleManager.CreateAsync(identityRole);
            if(result.Succeeded)
            {
                return RedirectToAction("Index","Home");
            }

            foreach(IdentityError error in result.Errors)
            {
                ModelState.AddModelError("",error.Description);
            }
            return View(model);
        }

        //show all role list
        public IActionResult RolesList()
        {
            var role = roleManager.Roles;
            return View(role);
        }

        //Delete Role
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if(role != null)
            {
                var de = await roleManager.DeleteAsync(role);
                if (de.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
                foreach (IdentityError error in de.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return RedirectToAction("RolesList");
        }


        //Edit Role get method
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if(role == null)
            {
                return View();
            }
            EditRoleViewModel model = new EditRoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name,
            };
            foreach(var users in userManager.Users)
            {
                if(await userManager.IsInRoleAsync(users, role.Name))
                {
                    model.Users.Add(users.UserName);
                }
            }
            return View(model);
        }

        //Edit Role post method
        [HttpPost]
        public async Task<IActionResult> Edit(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if(role == null)
            {
                return View();
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RolesList");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }

        //Edit User in role get method
        [HttpGet]
        public async Task<IActionResult> EditUser(string roleId)
        {
            ViewBag.RoleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId);
            if(role == null)
            {
                return View();
            }
            else
            {
                var model =new List<UserRoleViewModel>();
                foreach(var user in userManager.Users)
                {
                    var userRole = new UserRoleViewModel()
                    {

                        UserId = user.Id,
                        UserName = user.UserName,
                    };
                    if(await userManager.IsInRoleAsync(user,role.Name))
                    {
                        userRole.IsSelected = true;
                    }
                    else
                    {
                        userRole.IsSelected = false;
                    }
                    model.Add(userRole);
                }
                return View(model);
            }

        }

        // Edit User in role post method
        [HttpPost]
        public async Task<IActionResult> EditUser(List<UserRoleViewModel> model,string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if(role == null)
            {
                return View();
            }
            else
            {
                for(int i = 0; i < model.Count; i++)
                {
                    var user = await userManager.FindByIdAsync(model[i].UserId);
                    IdentityResult result = null;
                    if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user ,role.Name)))
                    {
                        result = await userManager.AddToRoleAsync(user,role.Name);
                    }
                    else if(!(model[i].IsSelected) && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        result = await userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    else
                    {
                        continue;
                    }
                    if (result.Succeeded)
                    {
                        if(i< model.Count - 1)
                        {
                            continue;
                        }
                        else
                        {
                            return RedirectToAction("Edit",new { Id = roleId});
                        }
                    }
                }
                return RedirectToAction("Edit", new { Id = roleId });
            }
        }


        //List of all users
        public IActionResult Users()
        {
            var user = userManager.Users;
            return View(user);
        }

        //Delete a user
        [HttpPost]
        public async Task<IActionResult> DeleteUsers(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if(user == null)
            {
                return RedirectToAction("Users");
            }
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Users");
            }
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return RedirectToAction("Users");
        }
        
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    City = model.City,
                    CreateDateTime = System.DateTime.Now
                };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }

        //Get Method For Update user
        [HttpGet]
        public async Task<IActionResult> UpdateUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if(user == null)
            {
                return RedirectToAction("Users");
            }
            EditUserViewModel model = new EditUserViewModel()
            {
                UserEmail = user.Email,
                UserID = user.Id,
                City = user.City,
                UserName = user.UserName,
                OldEmail = user.Email,
            };
            foreach(var role in roleManager.Roles)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Roles.Add(role.Name);
                }
            }
            return View(model);
        }

        //Post Method For update user
        [HttpPost]
        public async Task<IActionResult> UpdateUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserID);
            user.Email = model.UserEmail;
            user.City = model.City;
            user.UserName = model.UserName;
            var results = await userManager.UpdateAsync(user);
            if (results.Succeeded)
            {
                return RedirectToAction("Users");
            }
            foreach (IdentityError error in results.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }

    
        public IActionResult VerifyEmail(string UserEmail, string OldEmail)
        {
            if (UserEmail != OldEmail)
            {
                var email = userManager.FindByEmailAsync(UserEmail);
                if(email == null)
                {
                    return Json(true);
                }
                return Json($"The email {UserEmail} is exit");
            }
            else
            {
                return Json(true);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditRoleInUsers(string userId)
        {
            ViewBag.userId = userId;
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            else
            {
                var model = new List<RoleUserViewModel>();
                foreach (var role in roleManager.Roles)
                {
                    var RoleUser = new RoleUserViewModel()
                    {

                        RoleId = role.Id,
                        RoleName = role.Name,
                    };
                    if (await userManager.IsInRoleAsync(user, role.Name))
                    {
                        RoleUser.IsSelected = true;
                    }
                    else
                    {
                        RoleUser.IsSelected = false;
                    }
                    model.Add(RoleUser);
                }
                return View(model);
            }

        }
        [HttpPost]
        public async Task<IActionResult> EditRoleInUsers(List<RoleUserViewModel> model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            else
            {
                for (int i = 0; i < model.Count; i++)
                {
                    var role = await roleManager.FindByIdAsync(model[i].RoleId);
                    IdentityResult result = null;
                    if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, model[i].RoleName)))
                    {
                        result = await userManager.AddToRoleAsync(user, model[i].RoleName);
                    }
                    else if (!(model[i].IsSelected) && await userManager.IsInRoleAsync(user, model[i].RoleName))
                    {
                        result = await userManager.RemoveFromRoleAsync(user, model[i].RoleName);
                    }
                    else
                    {
                        continue;
                    }
                    if (result.Succeeded)
                    {
                        if (i < model.Count - 1)
                        {
                            continue;
                        }
                        else
                        {
                            return RedirectToAction("Users");
                        }
                    }
                }
                return RedirectToAction("Users");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserClaim(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return View();
            }

            UserClaimViewModel model = new UserClaimViewModel() { UserId = userId};
            var existingUserClaim = await userManager.GetClaimsAsync(user);
            foreach(Claim claim in ClaimStore.AllClaim)
            {
                UserClaim userClaim = new UserClaim() { ClaimType = claim.Type };
                if (existingUserClaim.All(c => c.Type == claim.Type))
                {
                    userClaim.IsSelected = true;
                }
                model.Claim.Add(userClaim);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ManageUserClaim(UserClaimViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if(user == null)
            {
                return View();
            }
            var claims = await userManager.GetClaimsAsync(user);
            var result = await userManager.RemoveClaimsAsync(user, claims);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Can't remove exixting claims");
                return View(model);
            }
            result = await userManager.AddClaimsAsync(user, model.Claim.Where(c => c.IsSelected).
                Select(c => new Claim(c.ClaimType, c.ClaimType)));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Can't remove existing claims");
                return View(model);
            }
            return RedirectToAction("Users");
        }
    }
}