using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TimeManagerClassLibrary.Models;
using TimeManagerMVC.Interfaces;

namespace TimeManagerMVC.Controllers
{
    public class LogsDisplayController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogsDisplayController(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        public async Task<IActionResult> Index(string UserId)
        {
            // Console.WriteLine(User.Identity.Name);
            ICollection<LogModel> Logs = await _unitOfWork.LogApi.GetLogsAsync(UserId);
            return View(Logs);
        }
    }
}
