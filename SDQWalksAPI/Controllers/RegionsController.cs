using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SDQWalksAPI.CostumeActionFilters;
using SDQWalksAPI.Data;
using SDQWalksAPI.Models.Domain;
using SDQWalksAPI.Models.Dtos;
using SDQWalksAPI.Repositories;

namespace SDQWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly SDQWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(SDQWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles ="Reader,Writer")]
        public async Task<IActionResult> GetAllRegion([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAccending = true, int pageNumber = 1, int pageSize = 100)
        {
            var regionDomian = await regionRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAccending ?? true, pageNumber, pageSize);
            mapper.Map<List<RegionDto>>(regionDomian);

            throw new Exception("This is a new exception");
            return Ok(regionDomian);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "Reader,Writer")]

        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            var regionDto = mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDto);
        }

        [HttpPost]
        [ValidaModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreteRegion([FromBody] AddregionRequestDto addregionRequestDto)
        {
            var regionsDomain = mapper.Map<Region>(addregionRequestDto);

            regionsDomain = await regionRepository.CreateAsync(regionsDomain);

            var regionDto = mapper.Map<RegionDto>(regionsDomain);

            return CreatedAtAction(nameof(GetRegionById), new { id = regionsDomain.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidaModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomain = mapper.Map<Region>(updateRegionRequestDto);

            regionDomain = await regionRepository.UpdateAsync(id, regionDomain);

            if (regionDomain == null)
            {
                return BadRequest("No se encuentra");
            }
            var regionDto = mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeteleRegion([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.Delete(id);
            if (regionDomain == null)
            {
                return NotFound("No se pudo encontrar");
            }
            var regionDto = mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDto);
        }
    }
}