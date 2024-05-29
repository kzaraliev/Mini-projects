using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SourceControl.Infrastructure.Data.Models
{
    public class PullRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Repository))]
        public int RepositoryId { get; set; }
        public Repository Repository { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Creator))]
        public required string CreatorId { get; set; }
        public IdentityUser Creator { get; set; } = null!;

        [Required]
        public string Description { get; set; } = string.Empty;

        public ICollection<Commit> Commits { get; set; } = new List<Commit>();

        [Required]
        public required string Status { get; set; } // Pending, Accepted, Rejected

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
