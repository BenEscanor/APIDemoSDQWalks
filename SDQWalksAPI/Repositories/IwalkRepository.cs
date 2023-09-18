using SDQWalksAPI.Models.Domain;

namespace SDQWalksAPI.Repositories
{
    public interface IwalkRepository
    {
        Task<Walk> CreateAsync(Walk walkl);
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAccending = true, int pageNumber = 1, int pageSize = 100);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> UpdateWalkAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
