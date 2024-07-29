using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.OpenApi.Models;

namespace Workbook.API.Functions
{
    public class TimesheetFunctions
    {
        private readonly ILogger<ProjectFunctions> _logger;

        public TimesheetFunctions(ILogger<ProjectFunctions> logger)
        {
            _logger = logger;
        }

        [OpenApiOperation(operationId: "getTimesheets", tags: ["timesheets"], Summary = "Get timesheets", Description = "Get all timesheets for a given date range")]
        [OpenApiParameter("email", Type = typeof(string), In = ParameterLocation.Header, Required = true, Description = "The current user's email address")]
        [OpenApiParameter("fromDate", Type = typeof(string), In = ParameterLocation.Query, Required = true, Description = "The start date for which to fetch timesheets from")]
        [OpenApiParameter("toDate", Type = typeof(string), In = ParameterLocation.Query, Required = true, Description = "The end date for which to fetch timesheets from")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Summary = "Returns timesheets or an error code", Description = "A list of timesheets")]
        [Function("Get-Timesheets")]
        public IActionResult GetTimesheets([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "timesheets")] HttpRequest req)
        {
            _logger.LogDebug("Get-Timesheets called");
            return new OkObjectResult("");
        }


        [OpenApiOperation(operationId: "addTimesheet", tags: ["timesheets"], Summary = "Add timesheet", Description = "Add a timesheet")]
        [OpenApiParameter("email", Type = typeof(string), In = ParameterLocation.Header, Required = true, Description = "The current user's email address")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Summary = "Returns OK or an error code", Description = "OK if the submitted timesheet is successfully registered in Workbook")]
        [Function("Add-Timesheet")]
        public IActionResult AddTimesheet([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "timesheets")] HttpRequest req)
        {
            _logger.LogDebug("Add-Timesheet called");
            return new OkResult();
        }

        [OpenApiOperation(operationId: "updateTimesheet", tags: ["timesheets"], Summary = "Update timesheet", Description = "Update a given timesheet")]
        [OpenApiParameter("email", Type = typeof(string), In = ParameterLocation.Header, Required = true, Description = "The current user's email address")]
        [OpenApiParameter("id", Type = typeof(int), In = ParameterLocation.Path, Required = true, Description = "The ID of the timesheet to update")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Summary = "Returns OK or an error code", Description = "OK if the submitted timesheet is successfully updated in Workbook")]
        [Function("Update-Timesheet")]
        public IActionResult UpdateTimesheet([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "timesheets/{id}")] HttpRequest req, int id)
        {
            _logger.LogDebug("Update-Timesheet called");
            return new OkResult();
        }

        [OpenApiOperation(operationId: "completeTimesheet", tags: ["timesheets"], Summary = "Complete timesheet", Description = "Mark a given timesheet as completed")]
        [OpenApiParameter("email", Type = typeof(string), In = ParameterLocation.Header, Required = true, Description = "The current user's email address")]
        [OpenApiParameter("id", Type = typeof(int), In = ParameterLocation.Path, Required = true, Description = "The ID of the timesheet to complete")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Summary = "Returns OK or an error code", Description = "OK if the requested timesheet is successfully marked as completed in Workbook")]
        [Function("Complete-Timesheet")]
        public IActionResult ApproveTimesheet([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "timesheets/{id}/approve")] HttpRequest req, int id)
        {
            _logger.LogDebug("Complete-Timesheet called");
            return new OkResult();
        }
    }
}