using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HRApp.Models;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HRApp.Controllers
{
    [Authorize(Roles = "Employee")]
    public class ProfileController : Controller
    {
        private readonly UserManager<Employee> _userManager;

        public ProfileController(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(Policy = "FirstLogin")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.Users
                .Include(u => u.EmployeeDetail)
                .ThenInclude(ed => ed.Educations)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var employeeDetail = user.EmployeeDetail ?? new EmployeeDetail();
            var model = new ProfileViewModel
            {
                Birthdate = employeeDetail.Birthdate,
                Address = employeeDetail.Address,
                Position = employeeDetail.Position,
                Educations = employeeDetail.Educations?.ToList() ?? new List<Education>(),
                Experiences = employeeDetail.Experiences?.ToList() ?? new List<Experience>()
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "FirstLogin")]
        public async Task<IActionResult> Index(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.Users
                    .Include(u => u.EmployeeDetail)
                    .ThenInclude(ed => ed.Educations)
                    .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var employeeDetail = user.EmployeeDetail;
                if (employeeDetail == null)
                {
                    employeeDetail = new EmployeeDetail
                    {
                        EmployeeDetailId = Guid.NewGuid(),
                        EmployeeId = user.Id,
                        Birthdate = model.Birthdate,
                        Address = model.Address,
                        Position = model.Position,
                        Educations = model.Educations,
                        Experiences = model.Experiences
                    };
                    user.EmployeeDetail = employeeDetail;
                }
                else
                {
                    employeeDetail.Birthdate = model.Birthdate;
                    employeeDetail.Address = model.Address;
                    employeeDetail.Position = model.Position;
                    employeeDetail.Educations = model.Educations;
                    employeeDetail.Experiences = model.Experiences;
                }

                await _userManager.UpdateAsync(user);
                ViewBag.Message = "Profile updated successfully!";
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Policy = "FirstLogin")]
        public IActionResult AddEducation()
        {
            return View(new EducationViewModel());
        }

        [HttpPost]
        [Authorize(Policy = "FirstLogin")]
        public async Task<IActionResult> AddEducation(EducationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.Users
                    .Include(u => u.EmployeeDetail)
                    .ThenInclude(ed => ed.Educations)
                    .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var employeeDetail = user.EmployeeDetail;
                if (employeeDetail == null)
                {
                    employeeDetail = new EmployeeDetail
                    {
                        EmployeeDetailId = Guid.NewGuid(),
                        EmployeeId = user.Id,
                        Educations = new List<Education>()
                    };
                    user.EmployeeDetail = employeeDetail;
                }

                var education = new Education
                {
                    EducationId = Guid.NewGuid(),
                    School = model.School,
                    Degree = model.Degree,
                    FieldOfStudy = model.FieldOfStudy,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    EmployeeDetailId = employeeDetail.EmployeeDetailId
                };

                employeeDetail.Educations.Add(education);
                await _userManager.UpdateAsync(user);
                ViewBag.Message = "Education added successfully!";

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
