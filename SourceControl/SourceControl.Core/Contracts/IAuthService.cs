using Microsoft.AspNetCore.Identity;
using SourceControl.Core.Models.Identity;

namespace SourceControl.Core.Contracts
{
    public interface IAuthService
    {
        Task<UserPublicModel> GetUserPublicData(string email);
        Task<string> GenerateTokenString(string email);
        Task<LoginTransfer> Login(LoginUser user);
        Task<IdentityResult> RegisterUser(RegisterUser user);
        Task<bool> AddRole(string roleName);
        Task<bool> AddUserToRole(string userId, string roleName);
        Task<bool> CheckUserExistByIdAsync(string id);
    }
}