﻿using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Text;
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
            _baseLogUri = $"{_shiftLogApi["ConnectionType"]}://{_shiftLogApi["Host"]}:{_shiftLogApi["Port"]}/ShiftLoger/Loger";
        }

        public async Task<ICollection<LogedDaysModel>> GetLogsAsync(string UserId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(
                $"{_baseLogUri}/{UserId}");
            return await ParseResponseAsync(response);
        }

        public async Task<ICollection<LogedDaysModel>> GetLogsByMonthAsync(string UserId, int Month, int Year)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(
                $"{_baseLogUri}/{UserId}/{Month}/{Year}");
            return await ParseResponseAsync(response);
        }




        public async Task<ICollection<LogedDaysModel>> ParseResponseAsync(HttpResponseMessage Response)
        {
            try
            {
                Response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                ICollection<LogedDaysModel> noLogs = new List<LogedDaysModel>();
                return noLogs;
            }
            string responseBody = await Response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ICollection<LogedDaysModel>>(responseBody);
        }

        public async Task<LogModel> PostStartStopLogAsync(string UserId)
        {
            var content = new StringContent(UserId, Encoding.UTF8);
            HttpResponseMessage response = await _httpClient.PostAsync($"{_baseLogUri}?UserId={UserId}",content);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                LogModel noLogs = new LogModel(UserId);                               
                return noLogs;
            }
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LogModel>(responseBody);
        }
    }
}
