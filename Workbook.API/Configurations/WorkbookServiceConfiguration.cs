using Microsoft.Extensions.Configuration;

namespace Workbook.API.Configurations
{
    public class WorkbookServiceConfiguration
    {
        private static string ConfigSectionName = "Workbook";
        public string BaseAddress { get; set; }
        public string ApiKey { get; set; }

        public WorkbookServiceConfiguration(IConfiguration configuration)
        {
            var configSection = configuration.GetSection(ConfigSectionName);
            BaseAddress = configSection[nameof(BaseAddress)];
            ApiKey = configSection[nameof(ApiKey)];
        }
    }
}
