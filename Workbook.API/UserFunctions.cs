using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace Workbook.API
{
    public class UserFunctions
    {
        private readonly ILogger<UserFunctions> _logger;

        public UserFunctions(ILogger<UserFunctions> logger)
        {
            _logger = logger;
        }

        [OpenApiOperation(operationId: "getUser", tags: ["user"], Summary = "Get Workbook user", Description = "Get a specific Workbook user by their email")]
        [OpenApiParameter("email", Type = typeof(int), In = ParameterLocation.Query, Required = true, Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/json", bodyType: typeof(string), Summary = "XXX", Description = "XXX")]
        [Function("Get-User")]
        public IActionResult GetProjects([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user")] HttpRequest req)
        {
            _logger.LogDebug("Get-User called");
            return new OkObjectResult("");
        }
    }
}
