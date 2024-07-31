using Newtonsoft.Json;
using System.Net.Http.Headers;
using Workbook.API.Configurations;
using Workbook.API.Models;

namespace Workbook.API.Services
{
    public class WorkbookClient
    {
        private readonly HttpClient _client;

        public WorkbookClient(HttpClient client, WorkbookServiceConfiguration configuration)
        {
            client.BaseAddress = new Uri(configuration.BaseAddress);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration.ApiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _client = client;
        }

        public async Task<List<User>?> GetAllUsers()
        {
                var response = await _client.GetAsync("/api/json/reply/ResourceWithEmployeesVisualizationRequest?Active=true");
                return response.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync())
                    : new List<User>();
        }

        public async Task<List<Project>> GetProjects(int employeeId)
        {
            var requestUri = $"/api/json/reply/TimeEntrySheetVisualizationRequest?ResourceId={employeeId}&Date={DateTime.UtcNow:yyyy-MM-ddTHH:mm:ss.fffZ}";
            var response = await _client.GetAsync(requestUri);
            return (response.IsSuccessStatusCode 
                ? JsonConvert.DeserializeObject<List<Project>>(await response.Content.ReadAsStringAsync()) 
                : []) ?? [];
        }

        public async Task<List<Timesheet>> GetTimesheet(int employeeId, DateTime from, DateTime to)
        {
            var requestUri = $"/api/json/reply/CapacityDaySummaryRequest?ResourceId={employeeId}&StartDate={from:O}&EndDate={to:O}";
            var response = await _client.GetAsync(requestUri);
            if (!response.IsSuccessStatusCode)
            {
                return [];
            }

            var timesheets = JsonConvert.DeserializeObject<List<TimesheetDto>>(await response.Content.ReadAsStringAsync());
            return (timesheets ?? []).Select(dto => new Timesheet(dto)).ToList();
        }
    }
}
