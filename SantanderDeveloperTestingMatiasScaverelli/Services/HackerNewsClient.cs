using SantanderDeveloperTestingMatiasScaverelli.Models;
using SantanderDeveloperTestingMatiasScaverelli.Services.Interfaces;

namespace SantanderDeveloperTestingMatiasScaverelli.Services
{
    public class HackerNewsClient : IHackerNewsClient
    {
        private readonly HttpClient _httpClient;

        public HackerNewsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HackerAPIItem?> GetItem(int id, CancellationToken cancellation = default)
        {
            return await _httpClient.GetFromJsonAsync<HackerAPIItem>($"item/{id}.json", cancellation);
        }

        public async Task<List<int>> GetTopStoriesIds(CancellationToken cancellation = default)
        {
            var result = await _httpClient.GetFromJsonAsync<List<int>>("beststories.json", cancellation);

            return result ?? new List<int>();
        }
    }
}
