namespace SourceControl.Core.Models.Issue
{
    public class DisplayIssueModel
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }

        public string Status { get; set; } // Open, On Hold, Closed

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
