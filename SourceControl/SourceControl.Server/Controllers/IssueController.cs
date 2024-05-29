using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SourceControl.Core.Contracts;
using SourceControl.Core.Models.Issue;

namespace SourceControl.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService issueService;

        public IssueController(IIssueService _issueService)
        {
            issueService = _issueService;
        }
        
        [HttpPost("AddIssue")]
        public async Task<IActionResult> AddIssueToRepo(AddIssueModel data)
        {
            if (await issueService.AddIssueAsync(data))
            {
                return Ok();
            }

            return Unauthorized();
        }
    }
}
