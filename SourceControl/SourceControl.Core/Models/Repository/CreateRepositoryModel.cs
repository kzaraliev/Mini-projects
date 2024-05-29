using Microsoft.AspNetCore.Identity;
using SourceControl.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SourceControl.Core.Models.Repository
{
    public class CreateRepositoryModel
    {

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public required string OwnerId { get; set; }
    }
}
