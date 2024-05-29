namespace SourceControl.Core.Models.Repository
{
    public class DisplayRepositoryModel
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public bool isPublic { get; set; }

        public required string OwnerId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set;}
    }
}
