using Application.Services;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Characters.Queries.GetList;

public class GetListCharacterQuery:IRequest<GetListResponse<GetListCharacterListItemIDto>>
{
    public PageRequest PageRequest { get; set; }


    public class GetListCharacterQueryHandler : IRequestHandler<GetListCharacterQuery, GetListResponse<GetListCharacterListItemIDto>>
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;
        public GetListCharacterQueryHandler(ICharacterRepository characterRepository, IMapper mapper)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
        }
        public async Task<GetListResponse<GetListCharacterListItemIDto>> Handle(GetListCharacterQuery request, CancellationToken cancellationToken)
        {
            var characters= await _characterRepository.GetListAsync(
                include: a=>a.Include(b=>b.Planet).Include(a=>a.Starships),
                index: request.PageRequest.PageIndex, size: request.PageRequest.PageSize);

            var mappedCharacters = _mapper.Map<GetListResponse<GetListCharacterListItemIDto>>(characters);
            return mappedCharacters;
        }
    }
}
