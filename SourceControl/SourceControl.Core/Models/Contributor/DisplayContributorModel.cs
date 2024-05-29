namespace SourceControl.Core.Models.Contributor
{
    public class DisplayContributorModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int RepositoryId { get; set; }

        public string? Username { get; set; }
    }
}
