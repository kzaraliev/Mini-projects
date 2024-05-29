using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SourceControl.Core.Contracts;

namespace SourceControl.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContributorController : ControllerBase
    {
        private readonly IContributorService contributorService;

        public ContributorController(IContributorService _contributorService)
        {
            contributorService = _contributorService;
        }

        [HttpPost("AddContributor")]
        public async Task<IActionResult> AddContributor(int repoId, string newContributorId, string ownerId)
        {
            if (!await contributorService.AddContributorAsync(repoId, newContributorId, ownerId))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
