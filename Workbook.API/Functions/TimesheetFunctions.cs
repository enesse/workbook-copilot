using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.OpenApi.Models;
using Microsoft.Azure.Functions.Worker.Http;
using Workbook.API.Extensions;
using Workbook.API.Models;
using Workbook.API.Services;
using Newtonsoft.Json;

namespace Workbook.API.Functions
{
    public class TimesheetFunctions
    {
        private readonly ILogger<ProjectFunctions> _logger;
        private readonly WorkbookRepository _repository;

        public TimesheetFunctions(ILogger<ProjectFunctions> logger, WorkbookRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [OpenApiOperation(operationId: "getTimesheets", tags: ["timesheets"], Summary = "Get timesheets", Description = "Get all timesheets for a given date range")]
        [OpenApiParameter("email", Type = typeof(string), In = ParameterLocation.Header, Required = true, Description = "The current user's email address")]
        [OpenApiParameter("fromDate", Type = typeof(string), In = ParameterLocation.Query, Required = true, Description = "The start date for which to fetch timesheets from")]
        [OpenApiParameter("toDate", Type = typeof(string), In = ParameterLocation.Query, Required = true, Description = "The end date for which to fetch timesheets from")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Timesheet>), Summary = "Returns timesheets or an error code", Description = "A list of timesheets")]
        [Function("Get-Timesheets")]
        public async Task<HttpResponseData> GetTimesheets([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "timesheets")] HttpRequestData req)
        {
            _logger.LogDebug("Get-Timesheets called");

            var email = req.GetEmail();
            var user = await _repository.GetUser(email);

            var fromDate = DateTime.Parse(req.Query.GetValues("fromDate")[0]);
            var toDate = DateTime.Parse(req.Query.GetValues("toDate")[0]);

            var timesheets = await _repository.GetTimesheet(user.Id, fromDate, toDate);
            return await req.OkResponse(timesheets);
        }
        
        [OpenApiOperation(operationId: "addTimesheet", tags: ["timesheets"], Summary = "Add timesheet", Description = "Add a timesheet")]
        [OpenApiParameter("email", Type = typeof(string), In = ParameterLocation.Header, Required = true, Description = "The current user's email address")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(TimesheetCreate), Summary = "Returns OK or an error code", Description = "OK if the submitted timesheet is successfully registered in Workbook")]
        [Function("Add-Timesheet")]
        public async Task<HttpResponseData> AddTimesheet([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "timesheets")] HttpRequestData req)
        {
            _logger.LogDebug("Add-Timesheet called");

            using var reader = new StreamReader(req.Body);
            var requestBody = await reader.ReadToEndAsync();
            var timesheet = JsonConvert.DeserializeObject<TimesheetCreate>(requestBody);

            var result = await _repository.CreateTimesheet(timesheet);
            return result
                ? req.OkResponse()
                : req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [OpenApiOperation(operationId: "completeTimesheets", tags: ["timesheets"], Summary = "Complete timesheets for a specific day", Description = "Mark all timesheets for a specific day as completed")]
        [OpenApiParameter("email", Type = typeof(string), In = ParameterLocation.Header, Required = true, Description = "The current user's email address")]
        [OpenApiParameter("date", Type = typeof(string), In = ParameterLocation.Query, Required = true, Description = "The date string for the day to complete timesheets for")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Summary = "Returns OK or an error code", Description = "OK if the requested timesheet is successfully marked as completed in Workbook")]
        [Function("Complete-Timesheets")]
        public async Task<HttpResponseData> CompleteTimesheets([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "timesheets/complete")] HttpRequestData req)
        {
            _logger.LogDebug("Complete-Timesheets called");

            var date = DateTime.Parse(req.Query.GetValues("date")[0]);
            var email = req.GetEmail();
            var user = await _repository.GetUser(email);

            var completed = await _repository.CompleteTimesheets(user.Id, date);
            return completed 
                ? req.OkResponse() 
                : req.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}