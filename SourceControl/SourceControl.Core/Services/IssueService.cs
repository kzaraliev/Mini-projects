using Microsoft.EntityFrameworkCore;
using SourceControl.Core.Contracts;
using SourceControl.Core.Models.Issue;
using SourceControl.Infrastructure.Data.Common;
using SourceControl.Infrastructure.Data.Models;

namespace SourceControl.Core.Services
{
    public class IssueService : IIssueService
    {
        private readonly IRepository repository;


        public IssueService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<bool> AddIssueAsync(AddIssueModel data)
        {
            var repo = await repository.AllReadOnly<Infrastructure.Data.Models.Repository>()
                .Include(r => r.Contributors)
                .Where(r => r.Id == data.RepositoryId)
                .FirstOrDefaultAsync();

            if (repo == null)
            {
                return false;
            }

            if (!repo.IsPublic)
            {
                bool isContributor = repo.Contributors.Any(c => c.UserId == data.UserId);

                if (data.UserId != repo.OwnerId && !isContributor)
                {
                    // User is not authorized to view this repository
                    return false;
                }
            }

            Issue issue = new Issue()
            {
                RepositoryId = data.RepositoryId,
                CreatorId = data.UserId,
                Tags = data.Tags,
                Description = data.Description,
                Status = data.Status,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await repository.AddAsync(issue);
            await repository.SaveChangesAsync();

            return true;
        }
    }
}
