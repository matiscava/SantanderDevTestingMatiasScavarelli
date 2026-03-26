using SantanderDeveloperTestingMatiasScaverelli.Models;

namespace SantanderDeveloperTestingMatiasScaverelli.Services.Interfaces
{
    public interface IHackerNewsClient
    {
        Task<List<int>> GetTopStoriesIds(CancellationToken cancellation = default);

        Task<HackerAPIItem?> GetItem(int id, CancellationToken cancellation = default);

    }
}
