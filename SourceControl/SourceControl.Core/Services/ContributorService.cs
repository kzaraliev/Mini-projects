using Microsoft.EntityFrameworkCore;
using SourceControl.Core.Contracts;
using SourceControl.Infrastructure.Data.Common;
using SourceControl.Infrastructure.Data.Models;

namespace SourceControl.Core.Services
{
    public class ContributorService : IContributorService
    {
        private readonly IRepository repository;
        private readonly IAuthService authService;


        public ContributorService(IRepository _repository, IAuthService _authService)
        {
            repository = _repository;
            authService = _authService;
        }

        public async Task<bool> AddContributorAsync(int repoId, string newContributorId, string ownerId)
        {
            if (await authService.CheckUserExistByIdAsync(newContributorId) && await authService.CheckUserExistByIdAsync(ownerId))
            {
                return false;
            }

            var repo =  await repository.AllReadOnly<Infrastructure.Data.Models.Repository>()
                 .Where(r => r.Id == repoId)
                 .FirstOrDefaultAsync();

            if (repo == null)
            {
                return false;
            }

            if (!repo.Contributors.Any(c => c.UserId == newContributorId) || repo.OwnerId != ownerId)
            {
                Contributor contributor = new Contributor()
                {
                    RepositoryId = repoId,
                    UserId = newContributorId
                };

                await repository.AddAsync(contributor);
                await repository.SaveChangesAsync();

                return true;
            };

            return false;
        }
    }
}
