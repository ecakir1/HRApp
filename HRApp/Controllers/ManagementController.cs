using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRApp.Models;
using DAL.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using HRApp.Services;
//using GSF.Security.Cryptography;

namespace HRApp.Controllers
{
    [Authorize(Roles = "CompanyExecutive")]
    public class ManagementController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;
        private readonly HRApp.Services.IEmailSender _emailSender;

        public ManagementController(UserManager<Employee> userManager, SignInManager<Employee> signInManager, HRApp.Services.IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SendInvitation()
        {
            return View();
        }
        /*
        [HttpPost]
        public async Task<IActionResult> SendInvitation(SendInvitationViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var email in model.Emails)
                {
                    var password = GenerateRandomPassword();
                    var user = new Employee { UserName = email, Email = email };
                    var result = await _userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Employee");
                        await _emailSender.SendEmailAsync(email, "Invitation to Join", $"Your username: {email}, Your password: {password}");
                    }
                }

                ViewBag.Message = "Invitations sent successfully!";
                return View();
            }

            return View(model);
        }
        /*
        /*
        private string GenerateRandomPassword()
        {
            var options = _userManager.Options.Password;
            var passwordGenerator = new PasswordGenerator();
            var password = passwordGenerator.GeneratePassword(options.RequiredLength);
            return password;
        }
        */
    }
}
