namespace SourceControl.Core.Models.Identity
{
    public class UserPublicModel
    {
        public required string Id { get; set; }
        public required string Email { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
