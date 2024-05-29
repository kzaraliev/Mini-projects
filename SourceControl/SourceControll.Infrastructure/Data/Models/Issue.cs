using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SourceControl.Infrastructure.Data.Models
{
    public class Issue
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
        public required string Description { get; set; }

        [Required]
        public required string Tags { get; set; }

        [Required]
        public required string Status { get; set; } // Open, On Hold, Closed

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
