using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SourceControl.Infrastructure.Data.Models
{
    public class Contributor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public required string UserId { get; set; }
        public IdentityUser User { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Repository))]
        public int RepositoryId { get; set; }
        public Repository Repository { get; set; } = null!;
    }
}
