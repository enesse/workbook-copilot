using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Workbook.API.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string GetEmail(this HttpRequestData req)
        {
            if (req.Headers.TryGetValues("email", out var email))
            {
                return email.First();
            }

            throw new ArgumentException("Did not provide valid email");
        }

        public static HttpResponseData OkResponse(this HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        public static async Task<HttpResponseData> OkResponse(this HttpRequestData req, object body)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(body);
            return response;
        }

        public static async Task<HttpResponseData> ErrorResponse(this HttpRequestData req, ILogger logger, string errorMessage, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            logger.LogError(errorMessage);

            var response = req.CreateResponse(statusCode);
            await response.WriteStringAsync(errorMessage);
            return response;
        }

        public static async Task<HttpResponseData> ErrorResponse(this HttpRequestData req, ILogger logger, Exception exception, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            logger.LogError(exception.Message);

            var response = req.CreateResponse(statusCode);
            await response.WriteStringAsync(exception.Message);
            return response;
        }
    }
}
