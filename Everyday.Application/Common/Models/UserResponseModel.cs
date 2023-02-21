namespace Everyday.Application.Common.Models
{
    public class UserResponseModel
    {
        public string Login { get; }
        public string? EncodedToken { get; set; }
        public List<string> Roles { get; set; }

        public UserResponseModel(string login)
        {
            Login = login;

            Roles = new List<string>();
        }
    }
}
