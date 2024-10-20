using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HRApp.Models;
using DAL.Models;
using Microsoft.AspNetCore.Identity;

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
            var user = await _userManager.GetUserAsync(User);
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
                Educations = employeeDetail.Educations?.ToList() ?? new List<Education>()
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "FirstLogin")]
        public async Task<IActionResult> Index(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var employeeDetail = user.EmployeeDetail ?? new EmployeeDetail();
                employeeDetail.Birthdate = model.Birthdate;
                employeeDetail.Address = model.Address;
                employeeDetail.Position = model.Position;
                employeeDetail.Educations = model.Educations;

                // Eğer EmployeeDetail daha önce null ise, yeni oluşturulan EmployeeDetail'i kullanıcıya atayın
                if (user.EmployeeDetail == null)
                {
                    user.EmployeeDetail = employeeDetail;
                }

                await _userManager.UpdateAsync(user);
                ViewBag.Message = "Profile updated successfully!";
            }
            return View(model);
        }
    }
}
