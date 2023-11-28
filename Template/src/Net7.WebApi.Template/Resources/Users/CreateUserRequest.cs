namespace Net7.WebApi.Template.Resources.Users
{
    public class CreateUserRequest
    {
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string UserType { get; set; } = null!;
    }
}