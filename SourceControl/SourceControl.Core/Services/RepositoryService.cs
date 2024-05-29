using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SourceControl.Core.Contracts;
using SourceControl.Core.Models.Repository;
using SourceControl.Infrastructure.Data.Common;

namespace SourceControl.Core.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly IRepository repository;
        private readonly IAuthService authService;


        public RepositoryService(IRepository _repository, IAuthService _authService)
        {
            repository = _repository;
            authService = _authService;

        }

        public async Task<int> AddRepositoryAsync(CreateRepositoryModel data)
        {
            if (!await authService.CheckUserExistByIdAsync(data.OwnerId))
            {
                throw new Exception("No such user");
            }

            Infrastructure.Data.Models.Repository repo = new Infrastructure.Data.Models.Repository()
            {
                Name = data.Name,
                IsPublic = data.IsPublic,
                OwnerId = data.OwnerId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await repository.AddAsync(repo);
            await repository.SaveChangesAsync();

            return repo.Id;
        }

        public async Task<bool> DeleteRepositoryByIdAsync(int repoId, string userId)
        {
            if (!await authService.CheckUserExistByIdAsync(userId))
            {
                return false;
            }

            var repo = await repository.AllReadOnly<Infrastructure.Data.Models.Repository>().Where(r => r.Id == repoId).FirstOrDefaultAsync();

            if (repo == null || repo.OwnerId != userId)
            {
                return false;
            }

            repository.Remove(repo);
            await repository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<DisplayRepositoryModel>> GetAllRepositoriesAsync()
        {
            return await repository.AllReadOnly<Infrastructure.Data.Models.Repository>()
                .Select(r => new DisplayRepositoryModel()
                {
                    Id = r.Id,
                    Name = r.Name,
                    OwnerId = r.OwnerId,
                    isPublic = r.IsPublic,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                })
                .Where(r => r.isPublic == true)
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }

        public async Task<DetailsRepositoryModel?> GetRepositoryByIdAsync(int id, string userId)
        {
            var repo = await repository.AllReadOnly<Infrastructure.Data.Models.Repository>()
               .Where(p => p.Id == id)
               .Select(p => new DetailsRepositoryModel()
               {
                   Id = p.Id,
                   Name = p.Name,
                   IsPublic = p.IsPublic,
                   OwnerId = p.OwnerId,
                   Contributors = p.Contributors
                .Where(c => c.RepositoryId == id)
                .Select(c => new Models.Contributor.DisplayContributorModel()
                {
                    Id = c.Id,
                    RepositoryId = c.RepositoryId,
                    UserId = c.UserId,
                    Username = repository.AllReadOnly<IdentityUser>()
                        .Where(u => u.Id == c.UserId)
                        .Select(u => u.UserName)
                        .FirstOrDefault()
                })
                .ToList(),

                   Commits = p.Commits
                .Where(c => c.RepositoryId == id)
                .Select(c => new Models.Commit.DisplayCommitModel()
                {
                    Id = c.Id,
                    AuthorId = c.AuthorId,
                    CommitDate = c.CommitDate,
                    Description = c.Description,
                    RepositoryId = c.RepositoryId
                })
                .ToList(),
                   CreatedAt = p.CreatedAt,
                   UpdatedAt = p.UpdatedAt,
                   Issues = p.Issues
                .Where(i => i.RepositoryId == id)
                .Select(i => new Models.Issue.DisplayIssueModel()
                {
                    Id = i.Id,
                    CreatedAt = i.CreatedAt,
                    UpdatedAt = i.UpdatedAt,
                    Description = i.Description,
                    Tags = i.Tags,
                    Status = i.Status
                })
                .ToList()

               })
               .FirstOrDefaultAsync();

            if (repo == null)
            {
                return null;
            }

            if (!repo.IsPublic)
            {
                bool isContributor = repo.Contributors.Any(c => c.UserId == userId);

                if (userId != repo.OwnerId && !isContributor)
                {
                    // User is not authorized to view this repository
                    return null;
                }
            }

            return repo;
        }
    }
}
