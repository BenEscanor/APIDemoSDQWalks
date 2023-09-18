using Azure.Messaging;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDQWalksAPI.Data;
using SDQWalksAPI.Models.Domain;
using SDQWalksAPI.Models.Dtos;

namespace SDQWalksAPI.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly SDQWalksDbContext dbContext;

        public SQLRegionRepository(SDQWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> Delete(Guid id)
        {
            var existRegion = await dbContext.Regions.FirstOrDefaultAsync(i => i.Id == id);
            if (existRegion != null)
            {
                dbContext.Regions.Remove(existRegion);
                await dbContext.SaveChangesAsync();
                return existRegion;
            }
                return null;
        }

        public async Task<List<Region>> GetAllAsync(string? filterOn = null, string? filterQuery = null,  string? sortBy = null,  bool isAccending = true, int pageNumber = 1, int pageSize =100)
        {
            var region = dbContext.Regions.AsQueryable();
            //filter
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    region = region.Where(x => x.Name.Contains(filterQuery));
                }
            }
            // sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    region = isAccending ? region.OrderBy(x => x.Name) : region.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Code", StringComparison.OrdinalIgnoreCase))
                {
                    region = isAccending ? region.OrderBy(x => x.Code) :
                    region.OrderByDescending(x => x.Code);
                }
            }
            //page
            var skipResult = (pageNumber - 1) * pageSize;

            return await region.Skip(skipResult).Take(pageSize).ToListAsync();
        }
        public async Task<Region?> GetByIdAsync(Guid id)
        {
            var existRegion = await dbContext.Regions.FirstAsync(x => x.Id == id);
            if (existRegion == null)
            {
                return null;
            }
            return existRegion;
            
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
           var existRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(existRegion == null)
            {
                return null;
            }
            existRegion.Code = region.Code;
            existRegion.Name = region.Name;
            existRegion.RegionImageUrl = region.RegionImageUrl;
            await dbContext.SaveChangesAsync();
            return existRegion;
        }
    }
}
