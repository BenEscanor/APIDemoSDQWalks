using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDQWalksAPI.Data;
using SDQWalksAPI.Models.Domain;
using System.Linq;

namespace SDQWalksAPI.Repositories
{
    public class SQLWalkRepository : IwalkRepository
    {
        private readonly SDQWalksDbContext dbContext;

        public SQLWalkRepository(SDQWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walkDomain = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walkDomain != null)
            {
                dbContext.Walks.Remove(walkDomain);
                await dbContext.SaveChangesAsync();
                return walkDomain;
            }
            return null;
            
        }
        public async Task<List<Walk>> GetAllAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAccending = true, int pageNumber =1, int pageSize =100)
        {
            var walk = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walk = walk.Where(w => w.Name.Contains(filterQuery));
                }
            }
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walk = isAccending ? walk.OrderBy(x => x.Name) :
                    walk.OrderByDescending(walk => walk.Name);
                }

                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walk = isAccending ? walk.OrderBy(walk => walk.LenghInKm) :
                     walk.OrderByDescending(walk => walk.LenghInKm);
                }
            }

            var skipResult = (pageNumber - 1) * pageSize;

            return await walk.Skip(skipResult).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            var walkDomain = await dbContext.Walks
                .Include("Difficulty")
                .Include("Region").FirstOrDefaultAsync(x => x.Id == id);

            if (walkDomain == null)
            {
                return null;
            }
            return walkDomain;
        }

        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk walk)
        {
            var walkDomainExist = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walkDomainExist == null)
            {
                return null;
            }
            walkDomainExist.Name = walk.Name;
            walkDomainExist.Description = walk.Description;
            walkDomainExist.LenghInKm = walk.LenghInKm;
            walkDomainExist.RegionId = walk.RegionId;
            walkDomainExist.DifficultyId = walk.DifficultyId;

            await dbContext.SaveChangesAsync();
            return walkDomainExist;
        }
    }
}
