using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SourceControl.Infrastructure.Data.Models
{
    public class Commit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Repository))]
        public int RepositoryId { get; set; }
        public Repository Repository { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Author))]
        public required string AuthorId { get; set; }
        public IdentityUser Author { get; set; } = null!;

        [Required]
        public DateTime CommitDate { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        public ICollection<Modification> Modifications { get; set; } = new List<Modification>();
    }
}
