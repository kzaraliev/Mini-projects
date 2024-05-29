using SourceControl.Core.Models.Repository;

namespace SourceControl.Core.Contracts
{
    public interface IRepositoryService
    {
        Task<int> AddRepositoryAsync(CreateRepositoryModel data);
        Task<bool> DeleteRepositoryByIdAsync(int repoId, string userId);
        Task<IEnumerable<DisplayRepositoryModel>?> GetAllRepositoriesAsync();
        Task<DetailsRepositoryModel?> GetRepositoryByIdAsync(int id, string userId);
    }
}
