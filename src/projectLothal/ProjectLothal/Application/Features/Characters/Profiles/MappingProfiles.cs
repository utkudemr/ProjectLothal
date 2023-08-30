

using Application.Features.Characters.Queries.GetList;
using Application.Features.Characters.Queries.GetListByDynamic;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistance.Paging;
using Domain.Entities;

namespace Application.Features.Characters.Profiles;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {
        CreateMap<Character, GetListCharacterListItemIDto>()
            .ForMember(destinationMember:c=>c.PlanetName, memberOptions:opt=>opt.MapFrom(c=>c.Planet.Name))
            .ForMember(destinationMember:c=>c.PlanetImageUrl,memberOptions:opt=>opt.MapFrom(c=>c.Planet.ImageUrl)).ReverseMap();
        CreateMap<Character, GetListByDynamicCharacterListItemDto>()
            .ForMember(destinationMember: c => c.PlanetName, memberOptions: opt => opt.MapFrom(c => c.Planet.Name))
            .ForMember(destinationMember: c => c.PlanetImageUrl, memberOptions: opt => opt.MapFrom(c => c.Planet.ImageUrl)).ReverseMap();
        CreateMap<Paginate<Character>, GetListResponse<GetListCharacterListItemIDto>>().ReverseMap();
        CreateMap<Paginate<Character>, GetListResponse<GetListByDynamicCharacterListItemDto>>().ReverseMap();
        CreateMap<Paginate<Starship>, GetListResponse<GetListCharactersStarshipListItemDto>>().ReverseMap();
        CreateMap<Starship, GetListCharactersStarshipListItemDto>().ReverseMap();
        CreateMap<Paginate<Starship>, GetListResponse<GetListCharactersStarshipDynamicListItemDto>>().ReverseMap();
        CreateMap<Starship, GetListCharactersStarshipDynamicListItemDto>().ReverseMap();
        
    }
}
