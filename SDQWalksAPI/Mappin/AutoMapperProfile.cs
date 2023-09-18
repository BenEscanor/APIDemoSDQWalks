using AutoMapper;
using SDQWalksAPI.Models.Domain;
using SDQWalksAPI.Models.Dtos;

namespace SDQWalksAPI.Mappin
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddregionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Walk,  WalkDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();   
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
        }
    }
}
