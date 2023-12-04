using System.Net;

namespace Net7.WebApi.Template.Exceptions
{
    /// <summary>
    /// Throw when you want 400 bad request response
    /// </summary>
    public class BadRequestException : HttpException
    {
        /// <summary>
        /// List of error that happened (optional)
        /// </summary>
        public IDictionary<string, string[]> ErrorList { get; set; }

        /// <inheritdoc/>
        public BadRequestException(string message, IDictionary<string, string[]> errorList = null!) : base(HttpStatusCode.BadRequest, message)
        {
            ErrorList = errorList;
        }

        /// <inheritdoc/>
        public BadRequestException(string message, Exception innerException, IDictionary<string, string[]> errorList = null!) : base(HttpStatusCode.BadRequest, message, innerException)
        {
            ErrorList = errorList;
        }

        /// <inheritdoc/>
        public string CreateResponseMessage()
        {
            if (ErrorList != null && ErrorList.Count > 0)
            {
                return $"{Message}: {string.Join(", ", ErrorList.Select(x => x.Key + " - " + string.Join(",", x.Value)).ToList())}";
            }
            return Message;
        }
    }
}