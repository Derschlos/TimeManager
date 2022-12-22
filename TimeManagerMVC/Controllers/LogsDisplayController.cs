using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TimeManagerClassLibrary.Models;
using TimeManagerMVC.Data;
using TimeManagerMVC.Interfaces;

namespace TimeManagerMVC.Controllers
{
    [Authorize(Policy = $"{Constants.Policies.RequireUser}")]
    public class LogsDisplayController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogsDisplayController(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            var username = User.Identity.Name;
            var user = _unitOfWork.Users.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound();
            }
            ICollection<LogModel> Logs = await _unitOfWork.LogApi.GetLogsAsync(user.Id);
            return View(Logs);
        }
        public async Task<IActionResult> LogHistory(int Month, int Year)
        {
            var username = User.Identity.Name;
            var user = _unitOfWork.Users.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound();
            }
            ICollection<LogModel> Logs = await _unitOfWork.LogApi.
                GetLogsByMonthAsync(user.Id, Month, Year);
            return View(Logs);
        }
    }
}
