using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SourceControl.Infrastructure.Data.Models
{
    public class Modification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Commit))]
        public required int CommitId { get; set; }
        public Commit Commit { get; set; } = null!;

        [Required]
        public required string Filename { get; set; }

        [Required]
        public required string FileDifferences { get; set; }

        [Required]
        public required string ModificationType { get; set; } // Added, Modified, Deleted
    }
}
