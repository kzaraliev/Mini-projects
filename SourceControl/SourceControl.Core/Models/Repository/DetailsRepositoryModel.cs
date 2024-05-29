using SourceControl.Core.Models.Commit;
using SourceControl.Core.Models.Contributor;
using SourceControl.Core.Models.Issue;
using SourceControl.Core.Models.PullRequests;

namespace SourceControl.Core.Models.Repository
{
    public class DetailsRepositoryModel
    {
        public int Id { get; set; }

        public required string Name { get; set; } 

        public bool IsPublic { get; set; }

        public required string OwnerId { get; set; }

        public ICollection<DisplayContributorModel> Contributors { get; set; } = new List<DisplayContributorModel>();
        public ICollection<DisplayCommitModel> Commits { get; set; } = new List<DisplayCommitModel>();
        public ICollection<DisplayIssueModel> Issues { get; set; } = new List<DisplayIssueModel>();
        public ICollection<DisplayPullRequestModel> PullRequests { get; set; } = new List<DisplayPullRequestModel>();

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
