using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SourceControl.Infrastructure.Data.Models;

namespace SourceControl.Infrastructure.Data
{
    public class SourceControlDbContext : IdentityDbContext<IdentityUser>
    {
        public SourceControlDbContext(DbContextOptions<SourceControlDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contributor>()
                .HasOne(c => c.Repository)
                .WithMany(r => r.Contributors)
                .HasForeignKey(c => c.RepositoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Commit>()
                .HasOne(c => c.Repository)
                .WithMany(r => r.Commits)
                .HasForeignKey(c => c.RepositoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Commit>()
                .HasOne(c => c.Author)
                .WithMany()
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Issue>()
                .HasOne(i => i.Repository)
                .WithMany(r => r.Issues)
                .HasForeignKey(i => i.RepositoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Issue>()
                .HasOne(i => i.Creator)
                .WithMany()
                .HasForeignKey(i => i.CreatorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PullRequest>()
                .HasOne(p => p.Repository)
                .WithMany(r => r.PullRequests)
                .HasForeignKey(p => p.RepositoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PullRequest>()
                .HasOne(p => p.Creator)
                .WithMany()
                .HasForeignKey(p => p.CreatorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Modification>()
                .HasOne(m => m.Commit)
                .WithMany(c => c.Modifications)
                .HasForeignKey(m => m.CommitId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<Commit> Commits { get; set; }
        public DbSet<Contributor> Contributors { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Modification> Modifications { get; set; }
        public DbSet<PullRequest> PullRequests { get; set; }
        public DbSet<Repository> Repositories { get; set; }
    }
}
