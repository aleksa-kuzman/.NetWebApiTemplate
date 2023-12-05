namespace Net7.WebApi.Template
{
    /// <summary>
    /// Contains constants for the entire application
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Denotes connection string key in appsettings
        /// </summary>
        public const string CONNECTION_STRING_JSON_KEY = "ConnectionString";

        /// <summary>
        /// Denotes name of admin authorization policy
        /// </summary>
        public const string ADMIN_POLICY = "AdminPolicy";

        /// <summary>
        /// Denotes name of regular user authorization policy
        /// </summary>
        public const string REGULAR_USER_POLICY = "RegularUserPolicy";

        /// <summary>
        /// Denotes name of readonly user authorization policy
        /// </summary>
        public const string READONLY_USER_POLICY = "ReadonlyUserPolicy";
    }
}