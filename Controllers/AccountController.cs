using Event_Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Management.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Required][EmailAddress] string email, [Required] string password,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = await _userManager.FindByEmailAsync(email);
                if (appUser != null)
                {
                    if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                    {
                        HttpContext.Session.SetString("UserId",appUser.Id.ToString());
                    }
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, password,false,false);
                    if (result.Succeeded)
                    {
                        
                        return Redirect(returnUrl ?? "/");
                    }                
                }
                ModelState.AddModelError(nameof(email), "Login Failed: Invalid Email or Password");
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
