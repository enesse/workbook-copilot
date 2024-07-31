using Workbook.API.Models;

namespace Workbook.API.Services
{
    public class WorkbookRepository
    {
        private List<User> Users { get; set; }

        private readonly WorkbookClient _client;

        public WorkbookRepository(WorkbookClient client)
        {
            _client = client;
            Users = new List<User>();
        }

        public async Task<User?> GetUser(string email)
        {
            var user = Users.FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                return user;
            }

            var users = await _client.GetAllUsers();
            if (users == null)
            {
                return user;
            }

            Users = users;
            user = Users.FirstOrDefault(x => x.Email == email);

            return user;
        }

        public async Task<List<Project>> GetProjects(int userId)
        {
            return await _client.GetProjects(userId);
        }

        public async Task<List<Timesheet>> GetTimesheet(int userId, DateTime from, DateTime to)
        {
            return await _client.GetTimesheet(userId, from, to);
        }
    }
}
