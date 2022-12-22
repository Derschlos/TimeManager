using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeManagerMVC.Areas.Identity.Data;
using TimeManagerMVC.Data;
using TimeManagerMVC.Interfaces;
using TimeManagerMVC.Models;

namespace TimeManagerMVC.Controllers
{
    [Authorize(Policy =$"{Constants.Policies.RequireManager}")]
    public class UserEditController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<TimeManagerUser> _signInManager;

        public UserEditController(IUnitOfWork UnitOfWork, SignInManager<TimeManagerUser> signInManager)
        {
            _unitOfWork = UnitOfWork;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var users = _unitOfWork.Users.GetTimeManagerUsers();
            return View(users);
        }

        public async Task<IActionResult> Edit(string id)
        {

            var user = _unitOfWork.Users.GetUserById(id);
            var roles = _unitOfWork.Roles.GetRoles();

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

            var roleItems = roles.Select(role =>
                new SelectListItem(
                    role.Name,
                    role.Id,
                    userRoles.Any(ur => ur.Contains(role.Name)))).ToList();

            var vm = new EditUserViewModel
            {
                User = user,
                Roles = roleItems
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync(EditUserViewModel data)
        {
            var user = _unitOfWork.Users.GetUserById(data.User.Id);
            if (user == null)
            {
                return NotFound();
            }
            user.Email = data.User.Email;
            user.UserName = data.User.UserName;

            var userRolesInDB = await _signInManager.UserManager.GetRolesAsync(user);
            var rolesToAdd = new List<string>();
            var rolesToRemove = new List<string>();
            foreach (var role in data.Roles)
            {
                var assingnedInDB = userRolesInDB.FirstOrDefault(ur => ur == role.Text);
                if (role.Selected)
                {
                    if (assingnedInDB == null)
                    {
                        rolesToAdd.Add(role.Text);
                    }
                }
                else
                {
                    if (assingnedInDB != null)
                    {
                        rolesToRemove.Add(role.Text);
                    }
                }
            }
            if (rolesToAdd.Any())
            {
                await _signInManager.UserManager.AddToRolesAsync(user, rolesToAdd);
            }
            if (rolesToRemove.Any())
            {
                await _signInManager.UserManager.RemoveFromRolesAsync(user, rolesToRemove);
            }

            _unitOfWork.Users.UpdateUser(user);


            return RedirectToAction("Edit", user);
        }
    }
}
