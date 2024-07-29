using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using Workbook.API.Extensions;
using Workbook.API.Models;
using Workbook.API.Services;

namespace Workbook.API.Functions
{
    public class UserFunctions
    {
        private readonly ILogger<UserFunctions> _logger;
        private readonly WorkbookRepository _repository;

        public UserFunctions(ILogger<UserFunctions> logger, WorkbookRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [OpenApiOperation(operationId: "getUser", tags: ["user"], Summary = "Get Workbook user", Description = "Get a specific Workbook user by their email")]
        [OpenApiParameter("email", Type = typeof(string), In = ParameterLocation.Header, Required = true, Description = "The current user's email address")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(User), Summary = "The user or an error response", Description = "Returns the user associated with the given email address")]
        [Function("Get-User")]
        public async Task<HttpResponseData> GetUser([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user")] HttpRequestData req)
        {
            _logger.LogDebug("Get-User called");

            var email = req.GetEmail();
            var user = await _repository.GetUser(email);
            return user == null
                ? await req.ErrorResponse(_logger, "User not found", HttpStatusCode.NotFound)
                : await req.OkResponse(user);
        }
    }
}
