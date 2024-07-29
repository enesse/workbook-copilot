using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace Workbook.API.Functions
{
    public class ProjectFunctions
    {
        private readonly ILogger<ProjectFunctions> _logger;

        public ProjectFunctions(ILogger<ProjectFunctions> logger)
        {
            _logger = logger;
        }

        [OpenApiOperation(operationId: "getProjects", tags: ["projects"], Summary = "Get available projects", Description = "Get all available projects and tasks that a user can log hours on")]
        [OpenApiParameter("email", Type = typeof(string), In = ParameterLocation.Header, Required = true, Description = "The current user's email address")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Summary = "XXX", Description = "XXX")]
        [Function("Get-Projects")]
        public IActionResult GetProjects([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "projects")] HttpRequest req)
        {
            _logger.LogDebug("Get-Projects called");
            return new OkObjectResult("");
        }
    }
}
