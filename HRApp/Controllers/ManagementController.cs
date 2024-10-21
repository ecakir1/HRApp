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
using GSF.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using DAL.Core;
using Microsoft.EntityFrameworkCore;

namespace HRApp.Controllers
{
    [Authorize(Roles = "Company Executive")]
    public class ManagementController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;
        private readonly HRApp.Services.IEmailSender _emailSender;
        private readonly HRManagementDbContext _context;

        public ManagementController(UserManager<Employee> userManager, SignInManager<Employee> signInManager, HRApp.Services.IEmailSender emailSender, HRManagementDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _context = context;
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

        [HttpPost]
        public async Task<IActionResult> SendInvitation(SendInvitationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = model.Email;
                if (!new EmailAddressAttribute().IsValid(email))
                {
                    ModelState.AddModelError("Email", $"The email address {email} is not valid.");
                    return View(model);
                }

                var existingUser = await _userManager.FindByEmailAsync(email);
                if (existingUser != null)
                {
                    // User already exists, generate a new password and send the invitation email
                    var newPassword = GenerateRandomPassword();
                    var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
                    var resetResult = await _userManager.ResetPasswordAsync(existingUser, token, newPassword);

                    if (resetResult.Succeeded)
                    {
                        await _userManager.AddClaimAsync(existingUser, new Claim("FirstLogin", "true"));
                        await _emailSender.SendEmailAsync(email, "Invitation to Join", $"Your username: {email}, Your new password: {newPassword}");
                        ViewBag.Message = "Invitation sent successfully!";
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "An error occurred while resetting the password.");
                    }
                }
                else
                {
                    // User does not exist, create the user and send the invitation email
                    var password = GenerateRandomPassword();
                    var user = new Employee { UserName = email, Email = email };
                    var result = await _userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Employee");
                        await _userManager.AddClaimAsync(user, new Claim("FirstLogin", "true"));
                        await _emailSender.SendEmailAsync(email, "Invitation to Join", $"Your username: {email}, Your password: {password}");
                        ViewBag.Message = "Invitation sent successfully!";
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "An error occurred while creating the user.");
                    }
                }

                return View();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeDetails()
        {
            var employees = await _context.Employees
                .Include(e => e.EmployeeDetail)
                .ThenInclude(ed => ed.Educations)
                .Include(e => e.EmployeeDetail)
                .ThenInclude(ed => ed.Experiences)
                .Include(e => e.EmployeeDetail)
                .ThenInclude(ed => ed.Certifications)
                .ToListAsync();

            return View(employees);
        }

        private string GenerateRandomPassword()
        {
            var options = _userManager.Options.Password;
            var passwordGenerator = new PasswordGenerator();
            var password = passwordGenerator.GeneratePassword(options.RequiredLength);
            return password;
        }
    }
}
