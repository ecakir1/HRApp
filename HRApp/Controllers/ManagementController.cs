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

        [HttpGet]
        public async Task<IActionResult> EditEmployee(Guid id)
        {
            var employee = await _context.Employees
                .Include(e => e.EmployeeDetail)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(Employee model)
        {
            if (ModelState.IsValid)
            {
                var employee = await _context.Employees
                    .Include(e => e.EmployeeDetail)
                    .FirstOrDefaultAsync(e => e.Id == model.Id);

                if (employee == null)
                {
                    return NotFound();
                }

                employee.UserName = model.UserName;
                employee.Email = model.Email;
                if (employee.EmployeeDetail == null)
                {
                    employee.EmployeeDetail = new EmployeeDetail();
                }
                employee.EmployeeDetail.Birthdate = model.EmployeeDetail.Birthdate;
                employee.EmployeeDetail.Address = model.EmployeeDetail.Address;
                employee.EmployeeDetail.Position = model.EmployeeDetail.Position;
                // Update other fields as necessary

                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(EmployeeDetails));
            }

            // Log model binding errors
            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

            foreach (var error in errors)
            {
                Console.WriteLine($"Key: {error.Key}, Errors: {string.Join(", ", error.Errors.Select(e => e.ErrorMessage))}");
            }

            return View(nameof(EditEmployee), model);
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
