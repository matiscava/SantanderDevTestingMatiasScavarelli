using SantanderDeveloperTestingMatiasScaverelli.Models;
using SantanderDeveloperTestingMatiasScaverelli.Services.Interfaces;
using System.Collections.ObjectModel;

namespace SantanderDeveloperTestingMatiasScaverelli.Services
{
    public class HackerNewsService : IHackerNewsService
    {

        private readonly IHackerNewsClient _hackerNewsClient;

        public HackerNewsService(IHackerNewsClient hackerNewsClient)
        {
            _hackerNewsClient = hackerNewsClient;
        }

        public async Task<ReadOnlyCollection<BestStoryDto>> GetBestStoryAsync(int n, CancellationToken cancellation = default)
        {
            var bestStoryIds = await _hackerNewsClient.GetTopStoriesIds(cancellation);

            if (!bestStoryIds.Any())
            {
                return Array.Empty<BestStoryDto>().ToList().AsReadOnly();
            }

            var candidates = bestStoryIds.Take(Math.Max(n * 2, n)).ToList();

            var tasks = candidates.Select(id => _hackerNewsClient.GetItem(id, cancellation)).ToList();

            var items = await Task.WhenAll(tasks);

            var stories = items.Where(isValidStory).Select(item => new BestStoryDto
            {
                Time = DateTimeOffset.FromUnixTimeSeconds(item.Time ?? 0),
                CommentCount = item.Kids?.Count() ?? 0,
                PostedBy = item.By ?? "",
                Score = item.Score ?? 0,
                Title = item.Title ?? "",
                Uri = item.Url ?? ""
            }).OrderBy(x => x.Score).Take(n).ToList();

            return stories.AsReadOnly();

        }

        private static bool isValidStory(HackerAPIItem? item)
        {
            if (item is null)
            {
                return false;
            }

            if (!string.Equals(item.Type, "story", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if(item.Deleted == true || item.Dead == true)
            {
                return false;
            }

            if(string.IsNullOrWhiteSpace(item.Title) || string.IsNullOrWhiteSpace(item.By))
            {
                return false;
            }

            if(item.Time <= 0 || item.Time == null)
            {
                return false;
            }

            return true;

        }
    }
}
