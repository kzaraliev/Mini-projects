
using SourceControl.Core.Models.Issue;

namespace SourceControl.Core.Contracts
{
    public interface IIssueService
    {
        Task<bool> AddIssueAsync(AddIssueModel data);
    }
}
