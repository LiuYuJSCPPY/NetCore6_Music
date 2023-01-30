using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Core6Music.Web.Areas.Dashboard.ViewModels;
using NToastNotify;

namespace Core6Music.Web.Areas.Dashboard.Controllers
{
    [Area("Dashborad")]
    public class UserRoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IToastNotification _toastNotification;

        public UserRoleController(RoleManager<IdentityRole> roleManager,IToastNotification toastNotification) {
        
            _roleManager = roleManager;
            _toastNotification = toastNotification;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Roles = _roleManager.Roles;
            return View(Roles);
        }

        public IActionResult CreateRole()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(RoleViewModel roleViewModel)
        {
            
            if(await _roleManager.RoleExistsAsync(roleViewModel.RoleName) != null && ModelState.IsValid)
            {
                IdentityRole Role = new IdentityRole()
                {
                    Name = roleViewModel.RoleName 
                };
               IdentityResult Result = await _roleManager.CreateAsync(Role);
                if (Result.Succeeded)
                {
                    _toastNotification.AddErrorToastMessage("新增角色成功!!");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toastNotification.AddErrorToastMessage("新增角色失敗");
                    return View(roleViewModel);
                }
                
            }
            else
            {
                _toastNotification.AddErrorToastMessage("新增角色失敗");
                return View(roleViewModel);
            }
            
            
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var role = await _roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                return NotFound();

            }
           

            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(IdentityRole Model)
        {
            if (Model == null)
            {
                return NotFound();
            }
            else
            {
                var result = await _roleManager.UpdateAsync(Model);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
        public async Task<IActionResult> Delete(string Id)
        {
            var role = _roleManager.FindByIdAsync(Id);
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string Id, IdentityRole role)
        {
            var drole = await _roleManager.FindByIdAsync(Id);
            await _roleManager.DeleteAsync(drole);
            return View();
        }
    }
}
