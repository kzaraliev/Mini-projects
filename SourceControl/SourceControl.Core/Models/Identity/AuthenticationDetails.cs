﻿namespace SourceControl.Core.Models.Identity
{
    public class AuthenticationDetails
    {
        public string AccessToken { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
