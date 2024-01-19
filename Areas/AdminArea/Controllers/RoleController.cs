﻿using HomeTaskkMVC4.Models;
using HomeTaskkMVC4.ViewModels.AdminRole;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HomeTaskkMVC4.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if(string.IsNullOrEmpty(roleName)) return NotFound();
          await  _roleManager.CreateAsync(new IdentityRole { Name = roleName });
            
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(string Id)
        {
            var user= await _userManager.FindByIdAsync(Id);
            if(user==null) return NotFound();

            UpdateRoleVM updateRoleVM = new();
            updateRoleVM.UserRoles= await _userManager.GetRolesAsync(user);
            updateRoleVM.Roles= _roleManager.Roles.ToList();
            updateRoleVM.User = user;
      
            return View(updateRoleVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id,List<string> roles)
        {
            var user=await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var oldRoles = await _userManager.GetRolesAsync(user);
             await _userManager.RemoveFromRolesAsync(user,oldRoles);
            await _userManager.AddToRolesAsync(user,roles);


            return RedirectToAction("Index", "User");
        }
    }
}
