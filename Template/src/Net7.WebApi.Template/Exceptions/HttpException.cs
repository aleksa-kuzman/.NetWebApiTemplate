using System.Net;

namespace Net7.WebApi.Template.Exceptions
{
    /// <summary>
    /// Base exception class for http endpoints
    /// </summary>
    public class HttpException : Exception
    {
        /// <inheritdoc/>
        public HttpException(HttpStatusCode statusCode, string exceptionMessage) : base(exceptionMessage)
        {
            StatusCode = statusCode;
        }

        /// <inheritdoc/>
        public HttpException(HttpStatusCode statusCode, string exceptionMessage, Exception innerException) : base(exceptionMessage, innerException)
        {
            StatusCode = statusCode;
        }

        /// <inheritdoc/>
        public HttpStatusCode StatusCode { get; private set; }
    }
}