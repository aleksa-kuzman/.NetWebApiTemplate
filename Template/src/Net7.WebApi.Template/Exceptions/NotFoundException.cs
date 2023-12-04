using System.Net;

namespace Net7.WebApi.Template.Exceptions
{
    /// <summary>
    /// Throw when you want 404 not found response
    /// </summary>
    public class NotFoundException : HttpException
    {
        /// <inheritdoc/>
        public NotFoundException(string message) : base(HttpStatusCode.NotFound, message)
        {
        }

        /// <inheritdoc/>
        public NotFoundException(string message, Exception innerException) : base(HttpStatusCode.NotFound, message, innerException)
        {
        }
    }
}