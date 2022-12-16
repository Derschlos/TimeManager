using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TimeManagerClassLibrary.Models;
using TimeManagerMVC.Interfaces;

namespace TimeManagerMVC.Controllers
{
    public class LogsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogsController(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            ICollection<LogModel> Logs = await _unitOfWork.LogApi.GetLogsAsync("b");
            return View(Logs);
        }
    }
}
