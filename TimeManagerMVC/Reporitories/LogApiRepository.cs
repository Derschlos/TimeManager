using Newtonsoft.Json;
using TimeManagerClassLibrary.Models;
using TimeManagerMVC.Interfaces;

namespace TimeManagerMVC.Reporitories
{
    public class LogApiRepository : ILogApiRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfigurationSection _shiftLogApi;
        private readonly string _baseLogUri;

        public LogApiRepository(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _shiftLogApi = configuration.GetSection("ShiftLogerApi");
            _baseLogUri = $"{_shiftLogApi["ConnectionType"]}://{_shiftLogApi["Host"]}:{_shiftLogApi["Port"]}/ShiftLoger";
        }

        public async Task<ICollection<LogModel>> GetLogsAsync(string UserId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_baseLogUri}/Loger/{UserId}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            ICollection<LogModel> Logs = JsonConvert.DeserializeObject<ICollection<LogModel>>(responseBody);
            return Logs;
        }
    }
}
