using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SourceControl.Core.Contracts;
using SourceControl.Core.Models.Identity;

namespace SourceControl.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
       private readonly IAuthService authService;
       
       public AuthController(IAuthService _authService, IMemoryCache _memoryCache)
       {
           authService = _authService;
       }
       
       [AllowAnonymous]
       [HttpPost("Register")]
       public async Task<IActionResult> RegisterUser(RegisterUser user)
       {
           var result = await authService.RegisterUser(user);
           if (result.Succeeded)
           {
               return await Login(new LoginUser() { Email = user.Email, Password = user.Password });
           }
       
           if (result.Errors.Any(error => error.Code == "DuplicateEmail"))
           {
               return BadRequest(new { message = "Oops! That email is already connected to an account." });
           }
       
           if (result.Errors.Any())
           {
               var errorMessage = string.Join("; ", result.Errors.Select(error => error.Description));
               return BadRequest(new { message = $"Something went wrong: {errorMessage}" });
           }
       
           return BadRequest(new { message = "Something went wrong" });
       }
       
       [AllowAnonymous]
       [HttpPost("Login")]
       public async Task<IActionResult> Login(LoginUser user)
       {
           if (!ModelState.IsValid)
           {
               return BadRequest();
           }
       
           var result = await authService.Login(user);
       
           if (result.IsSuccessful == true)
           {
               var tokenString = await authService.GenerateTokenString(user.Email);
       
               var userDetails = await authService.GetUserPublicData(user.Email);
       
               AuthenticationDetails response = new AuthenticationDetails()
               {
                   AccessToken = tokenString,
                   Email = userDetails.Email,
                   UserId = userDetails.Id,
                   Roles = userDetails.Roles,
               };
       
               return Ok(response);
           }
       
           return Unauthorized(new { message = "Incorrect email or password" });
       }
       
       [Authorize(Roles = "Administrator")]
       [HttpPost("AddRole")]
       public async Task<IActionResult> AddRole(string roleName)
       {
           var result = await authService.AddRole(roleName);
       
           return Ok(result);
       }
       
       [Authorize(Roles = "Administrator")]
       [HttpPost("AddUserToRole")]
       public async Task<IActionResult> AddUserToRole(string userId, string roleName)
       {
           var result = await authService.AddUserToRole(userId, roleName);
       
           return Ok(result);
       }
    }
}
