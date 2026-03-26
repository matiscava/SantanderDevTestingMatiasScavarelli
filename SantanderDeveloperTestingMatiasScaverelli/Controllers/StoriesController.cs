using Microsoft.AspNetCore.Mvc;
using SantanderDeveloperTestingMatiasScaverelli.Models;
using SantanderDeveloperTestingMatiasScaverelli.Services.Interfaces;

namespace SantanderDeveloperTestingMatiasScaverelli.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoriesController : Controller
    {
        private readonly IHackerNewsService _hackerNewsService;
        public StoriesController(IHackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }

        [HttpGet("bestStories")]
        [ProducesResponseType(typeof(IReadOnlyCollection<BestStoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBestStories([FromQuery] int? n, CancellationToken cancellation = default)
        {

            if (n == null)
            {
                return BadRequest("Query parameter 'n' is required.");
            }

            if (n <= 0)
            {
                return BadRequest("Query parameter 'n' must be a positive integer.");
            }

            if (n > 100)
            {
                return BadRequest("Query parameter 'n' must not exceed 100.");
            }

            var stories = await _hackerNewsService.GetBestStoryAsync(n.Value, cancellation);

            return Ok(stories);
        }

    }
}
