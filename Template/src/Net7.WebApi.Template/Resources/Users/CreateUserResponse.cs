namespace Net7.WebApi.Template.Resources.Users
{
    public class CreateUserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
    }
}