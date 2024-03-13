﻿using Demo.DAL.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;

		public UsersController(UserManager<ApplicationUser> userManager)
		{
			this.userManager = userManager;
		}

		public async Task<IActionResult> Index(string SearchValue = "")
		{
			if(string.IsNullOrEmpty(SearchValue)) 
			{
				var users = userManager.Users.ToList();
				return View(users);
			}
			else
			{
				var user = await userManager.FindByEmailAsync(SearchValue);
				return View(new List<ApplicationUser> { user });
			}
			
		}

		public async Task<IActionResult> Details(string id)
		{ 
			if(id is null) 
				return NotFound();

			var user = await userManager.FindByIdAsync(id);

			if (user is null)
				return NotFound();

			return View(user);
			

		}

		public async Task<IActionResult> Update(string id)
		{
            if (id is null)
                return NotFound();

            var user = await userManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            return View(user);
        }
		[HttpPost]
		[ValidateAntiForgeryToken] // Feature that prevent cross sign requet to web (Looks like sql injection ) '  علشان تمنع حدوث اي تغيير في حاله تعديل البيانات 
		
        public async Task<IActionResult> Update(string id, ApplicationUser applicationUser)
        {
			if(id != applicationUser.Id)
				return BadRequest();

			if(ModelState.IsValid)
			{
				try
				{
					var user = await userManager.FindByIdAsync(id);

					user.UserName = applicationUser.UserName;
					user.NormalizedUserName = applicationUser.UserName.ToUpper();
					user.PhoneNumber = applicationUser.PhoneNumber;

					var result = await userManager.UpdateAsync(user);

					if(result.Succeeded) 
						return RedirectToAction("Index");

					foreach (var error in result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);
				}
				catch(Exception ex)
				{
					throw;
				}
			}
            return View();
        }

		public async Task<IActionResult> Delete(string id)
		{
			if(id is null) 
				return NotFound();

			try
			{
				var user = await userManager.FindByIdAsync(id);

				var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

            }
			catch(Exception ex) 
			{
				throw;
			}
            return RedirectToAction("Index");
        }
    }
}
