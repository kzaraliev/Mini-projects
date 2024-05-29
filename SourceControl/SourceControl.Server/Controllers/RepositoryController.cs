using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SourceControl.Core.Contracts;
using SourceControl.Core.Models.Repository;
using static SourceControl.Core.Constants.CacheConstants;

namespace SourceControl.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RepositoryController : ControllerBase
    {
        private readonly IRepositoryService repositoryService;
        private readonly IMemoryCache memoryCache;

        public RepositoryController(IRepositoryService _repositoryService, IMemoryCache _memoryCache)
        {
            repositoryService = _repositoryService;
            memoryCache = _memoryCache;
        }

        [AllowAnonymous]
        [HttpGet("GetAllRepositories")]
        public async Task<IActionResult> GetAllRepositories()
        {
            var repos = memoryCache.Get<IEnumerable<DisplayRepositoryModel>>(RepositoryCacheKey);

            if (repos == null || repos.Any() == false)
            {
                repos = await repositoryService.GetAllRepositoriesAsync();

                var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

                memoryCache.Set(RepositoryCacheKey, repos, cacheOptions);
            }

            return Ok(repos);
        }

        [AllowAnonymous]
        [HttpGet("GetRepositoryById")]
        public async Task<IActionResult> GetRepositoryById(int id, string userId)
        {
            var repo = await repositoryService.GetRepositoryByIdAsync(id, userId);

            if (repo == null)
            {
                return NotFound();
            }

            return Ok(repo);
        }

        [HttpPost("AddRepository")]
        public async Task<IActionResult> AddRepository(CreateRepositoryModel data)
        {
            try
            {
                int repoId = await repositoryService.AddRepositoryAsync(data);
                return Ok(new
                {
                    message = "Success",
                    request = Url.Action("GetRepositoryById", "Repository", new { id = repoId }, Request.Scheme)
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the repo.");
            }
        }

        [HttpDelete("DeleteRepository")]
        public async Task<IActionResult> DeleteRepository(int repoId, string userId)
        {
            if(await repositoryService.DeleteRepositoryByIdAsync(repoId, userId))
            {
                return Ok(new
                {
                    message = "Success",
                });
            }

            return BadRequest();
        }
    }
}
