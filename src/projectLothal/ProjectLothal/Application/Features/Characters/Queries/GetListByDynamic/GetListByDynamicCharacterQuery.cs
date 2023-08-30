

using Application.Services;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistance.Dynamic;
using Core.Persistance.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Characters.Queries.GetListByDynamic
{
    public class GetListByDynamicCharacterQuery : IRequest<GetListResponse<GetListByDynamicCharacterListItemDto>>
    {
        public PageRequest PageRequest { get; set; }
        public DynamicQuery DynamicQuery { get; set; }
        public class GetListByDynamicCharacterQueryHandler : IRequestHandler<GetListByDynamicCharacterQuery, GetListResponse<GetListByDynamicCharacterListItemDto>>
        {
            private readonly ICharacterRepository _characterRepository;
            private readonly IMapper _mapper;
            public GetListByDynamicCharacterQueryHandler(ICharacterRepository characterRepository, IMapper mapper)
            {
                _characterRepository = characterRepository;
                _mapper = mapper;
            }
            public async Task<GetListResponse<GetListByDynamicCharacterListItemDto>> Handle(GetListByDynamicCharacterQuery request, CancellationToken cancellationToken)
            {
               var characters = await _characterRepository.GetListByDynamicAsync(
               request.DynamicQuery,
               include: a => a.Include(b => b.Planet),
               index: request.PageRequest.PageIndex, size: request.PageRequest.PageSize);

                var mappedCharacters = _mapper.Map<GetListResponse<GetListByDynamicCharacterListItemDto>>(characters);
                return mappedCharacters;
            }
        }
    }
}
