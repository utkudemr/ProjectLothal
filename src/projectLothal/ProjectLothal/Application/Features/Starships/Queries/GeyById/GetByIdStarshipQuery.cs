using Application.Services;
using AutoMapper;
using MediatR;

namespace Application.Features.Starships.Queries.GeyById
{
    public class GetByIdStarshipQuery:IRequest<GetByIdStarshipResponse>
    {
        public Guid Id { get; set; }

        public class GetByIdStarshipQueryHandler:IRequestHandler<GetByIdStarshipQuery,GetByIdStarshipResponse>
        {
            private readonly IMapper _mapper;
            private readonly IStarshipRepository _starshipRepository;

            public GetByIdStarshipQueryHandler(IStarshipRepository starshipRepository, IMapper mapper)
            {
                _starshipRepository = starshipRepository;
                _mapper = mapper;
            }

            public async Task<GetByIdStarshipResponse> Handle(GetByIdStarshipQuery request, CancellationToken cancellationToken)
            {
                var starship= await _starshipRepository.GetAsync(predicate: b => b.Id == request.Id,cancellationToken:cancellationToken);
                var starshipResponse= _mapper.Map<GetByIdStarshipResponse>(starship);
                return starshipResponse;
            }
        }

       
    }
}
