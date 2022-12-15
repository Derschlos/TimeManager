using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace TimeManagerMVC.Controllers
{
    public class LogsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _shiftLogApi;

        public LogsController()
        {
            _httpClient = new HttpClient();
            _shiftLogApi = ConfigurationManager
        }

        public IActionResult Index()
        {

            return View();
        }
    }
}
