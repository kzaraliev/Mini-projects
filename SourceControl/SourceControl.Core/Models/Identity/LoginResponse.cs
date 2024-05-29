namespace SourceControl.Core.Models.Identity
{
    public class LoginResponse
    {
        public bool IsLogedIn { get; set; } = false;
        public string JwtToken { get; set; } = string.Empty;
    }
}
