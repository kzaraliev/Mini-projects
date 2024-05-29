using System.ComponentModel.DataAnnotations;

namespace SourceControl.Core.Models.Issue
{
    public class AddIssueModel
    {
        public int RepositoryId { get; set; }
        public required string UserId { get; set; }
        public string Description { get; set; }
        public required string Tags { get; set; }
        public required string Status { get; set; }
    }
}
