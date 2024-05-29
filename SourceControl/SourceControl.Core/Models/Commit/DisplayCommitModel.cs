namespace SourceControl.Core.Models.Commit
{
    public class DisplayCommitModel
    {
        public int Id { get; set; }

        public int RepositoryId { get; set; }

        public string AuthorId { get; set; }

        public DateTime CommitDate { get; set; }

        public string Description { get; set; }
    }
}
