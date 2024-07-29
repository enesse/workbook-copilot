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
                var response = await _client.GetAsync("resource/employeesvisualization");
                return response.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync())
                    : new List<User>();
        }
    }
}
