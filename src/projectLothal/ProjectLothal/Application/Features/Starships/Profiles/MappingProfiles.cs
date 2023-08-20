using Application.Features.Starships.Commands.Create;
using Application.Features.Starships.Queries.GetList;
using Application.Features.Starships.Queries.GeyById;
using AutoMapper;
using Business.Features.Starships.Commands.Create;
using Core.Application.Responses;
using Core.Persistance.Paging;
using Domain.Entities;

namespace Application.Features.Starships.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Starship,CreateStarshipCommand>().ReverseMap();
            CreateMap<Starship,CreatedStarshipResponse>().ReverseMap();
            CreateMap<Starship, GetListStarshipListItemDto>().ReverseMap();
            CreateMap<Starship, GetByIdStarshipResponse>().ReverseMap();
            CreateMap<Paginate<Starship>,GetListResponse<GetListStarshipListItemDto>>().ReverseMap();
        }
    }
}
