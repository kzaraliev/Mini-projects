using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SourceControl.Infrastructure.Data.Models
{
    public class Repository
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        [ForeignKey(nameof(Owner))]
        public required string OwnerId { get; set; }
        public IdentityUser Owner { get; set; } = null!;

        public ICollection<Contributor> Contributors { get; set; } = new List<Contributor>();
        public ICollection<Commit> Commits { get; set; } = new List<Commit>();
        public ICollection<Issue> Issues { get; set; } = new List<Issue>();
        public ICollection<PullRequest> PullRequests { get; set; } = new List<PullRequest>();


        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
