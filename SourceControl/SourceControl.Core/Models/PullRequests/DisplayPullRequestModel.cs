namespace SourceControl.Core.Models.PullRequests
{
    public class DisplayPullRequestModel
    {
        public int Id { get; set; }
        public int RepositoryId { get; set; }

        public required string CreatorId { get; set; }

        public string Description { get; set; } = string.Empty;

        public required string Status { get; set; } // Pending, Accepted, Rejected

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
