using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using DAL.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HRApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly HRManagementDbContext _context;

        public AdminController(UserManager<Employee> userManager, HRManagementDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult ManageUsers()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> PromoteToExecutive(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                if (currentRoles.Contains("Employee"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "Employee");
                }
                await _userManager.AddToRoleAsync(user, "Company Executive");
            }

            return RedirectToAction("ManageUsers");
        }

        [HttpGet]
        public async Task<IActionResult> Applications()
        {
            var companies = await _context.Companies.Include(c => c.Applicant).ToListAsync();
            var model = new List<CompanyViewModel>();

            foreach (var company in companies)
            {
                var roles = await _userManager.GetRolesAsync(company.Applicant);
                model.Add(new CompanyViewModel
                {
                    Company = company,
                    ApplicantRoles = roles
                });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                if (currentRoles.Contains("Company Executive"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "Company Executive");
                    await _userManager.AddToRoleAsync(user, "Employee");
                }
                else if (currentRoles.Contains("Employee"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "Employee");
                    await _userManager.AddToRoleAsync(user, "Company Executive");
                }
            }

            return RedirectToAction("Applications");
        }
    }

    public class CompanyViewModel
    {
        public Company Company { get; set; }
        public IList<string> ApplicantRoles { get; set; }
        public string Name { get; internal set; }
        public string Address { get; internal set; }
        public string Email { get; internal set; }
        public string PhoneNumber { get; internal set; }
    }
}
