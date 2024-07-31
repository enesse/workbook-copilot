using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using Workbook.API.Extensions;
using Workbook.API.Services;

namespace Workbook.API.Functions
{
    public class ProjectFunctions
    {
        private readonly ILogger<ProjectFunctions> _logger;
        private readonly WorkbookRepository _repository;
        
        public ProjectFunctions(ILogger<ProjectFunctions> logger, WorkbookRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [OpenApiOperation(operationId: "getProjects", tags: ["projects"], Summary = "Get available projects", Description = "Get all available projects and tasks that a user can log hours on")]
        [OpenApiParameter("email", Type = typeof(string), In = ParameterLocation.Header, Required = true, Description = "The current user's email address")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Summary = "Return a list of projects and tasks or an error code", Description = "A list of active projects and tasks")]
        [Function("Get-Projects")]
        public async Task<HttpResponseData> GetProjects([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "projects")] HttpRequestData req)
        {
            _logger.LogDebug("Get-Projects called");

            var email = req.GetEmail();
            var user = await _repository.GetUser(email);
            var projects = await _repository.GetProjects(user.Id);

            return await req.OkResponse(projects);
        }
    }
}
