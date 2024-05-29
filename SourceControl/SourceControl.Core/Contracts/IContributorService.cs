
namespace SourceControl.Core.Contracts
{
    public interface IContributorService
    {
        Task<bool> AddContributorAsync(int repoId, string userId, string ownerId);
    }
}
