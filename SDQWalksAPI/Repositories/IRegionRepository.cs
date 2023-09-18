using Microsoft.AspNetCore.Components.Web;
using SDQWalksAPI.Models.Domain;

namespace SDQWalksAPI.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync(string? filterOn = null, string? filterQuery =null, string? sortBy =null, bool isAccending = true, int pageNumber = 1, int pageSize = 100);
        Task<Region?> GetByIdAsync(Guid id);
        Task<Region> CreateAsync(Region region);
        Task<Region?> UpdateAsync(Guid id, Region region);
        Task<Region?> Delete(Guid id);
    }
}
