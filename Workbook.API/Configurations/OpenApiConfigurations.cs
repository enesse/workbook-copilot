using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;

namespace Workbook.API.Configurations
{
    public class OpenApiConfigurations : IOpenApiConfigurationOptions
    {
        public OpenApiInfo Info { get; set; } = new()
        {
            Version = "1.0.0",
            Title = "Workbook API",
            Description = "A simple Deltek Workbook API",
            Contact = new OpenApiContact
            {
                Name = "Erik Nesse",
                Email = "erik.nesse@noaignite.com",
                Url = new Uri("https://github.com/enesse/workbook-copilot"),
            },
            License = new OpenApiLicense
            {
                Name = "MIT",
                Url = new Uri("http://opensource.org/licenses/MIT"),
            }
        };

        public List<OpenApiServer> Servers { get; set; } = DefaultOpenApiConfigurationOptions.GetHostNames();

        public OpenApiVersionType OpenApiVersion { get; set; } = OpenApiVersionType.V2;
        public bool IncludeRequestingHostName { get; set; } = true;
        public bool ForceHttp { get; set; } = false;
        public bool ForceHttps { get; set; } = false;
        public List<IDocumentFilter> DocumentFilters { get; set; }
    }
}
