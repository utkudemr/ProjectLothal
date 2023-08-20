using Application.Services;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistance.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Starships.Queries.GetList;

public class GetListStarshipQuery:IRequest<GetListResponse<GetListStarshipListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public class GetListStarshipQueryHandler : IRequestHandler<GetListStarshipQuery, GetListResponse<GetListStarshipListItemDto>>
    {
        private readonly IStarshipRepository _starshipRepository;
        private readonly IMapper _mapper;
        public GetListStarshipQueryHandler(IStarshipRepository starshipRepository, IMapper mapper)
        {
            _starshipRepository = starshipRepository;
            _mapper = mapper;
        }
        public async Task<GetListResponse<GetListStarshipListItemDto>> Handle(GetListStarshipQuery request, CancellationToken cancellationToken)
        {
            var result = await _starshipRepository.GetListAsync(index: request.PageRequest.PageIndex, size: request.PageRequest.PageSize, cancellationToken:cancellationToken);
            GetListResponse<GetListStarshipListItemDto> response = _mapper.Map<GetListResponse<GetListStarshipListItemDto>>(result);
            return response;
        }
    }
}
