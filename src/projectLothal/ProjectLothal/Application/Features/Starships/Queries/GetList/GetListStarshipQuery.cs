using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Requests;
using Core.Application.Responses;
using MediatR;
namespace Application.Features.Starships.Queries.GetList;

public class GetListStarshipQuery:IRequest<GetListResponse<GetListStarshipListItemDto>>, ICachableRequest, ILoggableRequest
{
    public PageRequest PageRequest { get; set; }

    public string CacheKey => $"GetListStarshipQuery({PageRequest.PageIndex},{PageRequest.PageSize})";

    public bool ByPassCache { get; }

    public TimeSpan? SlidingExpiration { get; }

    public string? CacheGroupKey => "GetStarships";

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
