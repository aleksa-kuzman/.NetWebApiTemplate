using System.Net;

namespace Net7.WebApi.Template.Resources
{
    /// <summary>
    /// Error response object that the api will return
    /// </summary>
    public class HttpErrorResponse
    {
        /// <summary>
        /// Constructor with all neccessary parameters
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="message"></param>
        /// <param name="errors"></param>
        /// <param name="correlationId"></param>
        public HttpErrorResponse(HttpStatusCode httpStatusCode, string message, IDictionary<string, string[]> errors, string correlationId = null!)
        {
            HttpStatusCode = httpStatusCode;
            CorrelationId = correlationId;
            Message = message;
            Errors = errors;
        }

        /// <summary>
        /// Denotes status code
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; set; }

        /// <summary>
        /// Parameter used for tracking a request through multiple services
        /// (front end, backend services etc...)
        /// </summary>
        public string CorrelationId { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Error details
        /// </summary>
        public IDictionary<string, string[]> Errors { get; set; }
    }
}