using SantanderDeveloperTestingMatiasScaverelli.Models;
using System.Collections.ObjectModel;

namespace SantanderDeveloperTestingMatiasScaverelli.Services.Interfaces
{
    public interface IHackerNewsService
    {
        Task<ReadOnlyCollection<BestStoryDto>> GetBestStoryAsync(int n, CancellationToken cancellation = default);
    }
}
