using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
  public class PlatformsProfile : Profile
  {
    public PlatformsProfile()
    {
      // source to target
      CreateMap<Platform, PlatformReadDto>();
      CreateMap<PlatformCreateDto, Platform>();
      CreateMap<PlatformReadDto, PlatformPublishedDto>();
      CreateMap<Platform, GrpcPlatformModel>()
        .ForMember(destination => destination.PlatformId, opt => opt.MapFrom(src => src.Id))
        .ForMember(destination => destination.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(destination => destination.Publisher, opt => opt.MapFrom(src => src.Publisher));
    }
  }
}