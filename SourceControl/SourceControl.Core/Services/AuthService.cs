using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SourceControl.Core.Contracts;
using SourceControl.Core.Models.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SourceControl.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration config;

        public AuthService(UserManager<IdentityUser> _userManager, IConfiguration _config, RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            config = _config;
            roleManager = _roleManager;
        }

        public async Task<bool> CheckUserExistByIdAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public async Task<string> GenerateTokenString(string email)
        {
            var identityUser = await userManager.FindByEmailAsync(email);
            if (identityUser == null) throw new Exception("User not found");

            var userRoles = await userManager.GetRolesAsync(identityUser);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,email),
                new Claim(ClaimTypes.Name,email),
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetConfigurationValue("Jwt:Key")));

            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: GetConfigurationValue("Jwt:Issuer"),
                audience: GetConfigurationValue("Jwt:Audience"),
                signingCredentials: signingCred);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }

        public async Task<UserPublicModel> GetUserPublicData(string email)
        {
            var identityUser = await userManager.FindByEmailAsync(email);

            if (identityUser == null)
            {
                throw new Exception("No such user");
            }

            var userRoles = await userManager.GetRolesAsync(identityUser);

            var user = new UserPublicModel()
            {
                Id = identityUser.Id,
                Email = identityUser.Email ?? "Error",
                Roles = userRoles
            };

            return user;
        }

        public async Task<LoginTransfer> Login(LoginUser user)
        {
            bool result = true;
            string refreshToken = string.Empty;

            var identityUser = await userManager.FindByEmailAsync(user.Email);
            if (identityUser is null)
            {
                result = false;
            }

            if (result)
            {
                result = await userManager.CheckPasswordAsync(identityUser, user.Password);
            }

            return new LoginTransfer()
            {
                IsSuccessful = result,
            };
        }

        public async Task<IdentityResult> RegisterUser(RegisterUser user)
        {
            var identityUser = new IdentityUser { UserName = user.Email, Email = user.Email };

            var result = await userManager.CreateAsync(identityUser, user.Password);

            return result;
        }

        public async Task<bool> AddRole(string roleName)
        {
            if (await roleManager.RoleExistsAsync(roleName) == false)
            {
                var result = await roleManager.CreateAsync(new IdentityRole { Name = roleName, ConcurrencyStamp = Guid.NewGuid().ToString() });
                return result.Succeeded;
            }

            return false;
        }

        public async Task<bool> AddUserToRole(string userId, string roleName)
        {
            if (await roleManager.RoleExistsAsync(roleName))
            {
                var user = await userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    if (await userManager.IsInRoleAsync(user, roleName) == false)
                    {
                        await userManager.AddToRoleAsync(user, roleName);

                        return true;
                    }
                }
            }

            return false;
        }

        private ClaimsPrincipal? GetTokenPrincipal(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetConfigurationValue("Jwt:Key")));

            var validation = new TokenValidationParameters
            {
                IssuerSigningKey = securityKey,
                ValidateLifetime = false,
                ValidateActor = false,
                ValidateIssuer = false,
                ValidateAudience = false,
            };

            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }

        private string GetConfigurationValue(string key)
        {
            return config.GetSection(key).Value ?? throw new ArgumentException($"Configuration for {key} is not set properly.");
        }
    }
}
