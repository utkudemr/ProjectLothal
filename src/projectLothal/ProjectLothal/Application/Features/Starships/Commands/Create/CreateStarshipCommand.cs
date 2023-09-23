using Application.Features.Starships.Commands.Create;
using Application.Features.Starships.Rules;
using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Business.Features.Starships.Commands.Create
{
    public class CreateStarshipCommand : IRequest<CreatedStarshipResponse>, ITransactionalRequest, ICacheRemoverRequest
    {
        public string Name { get; set; }

        public string CacheKey => "";

        public bool ByPassCache => false;
        public string? CacheGroupKey => "GetStarships";

        public class CreateStarshipCommandHandler : IRequestHandler<CreateStarshipCommand, CreatedStarshipResponse>
        {
            private readonly IStarshipRepository _starshipRepository;
            private readonly IMapper _mapper;
            private readonly StarshipBusinessRules _starshipBusinessRules;

            public CreateStarshipCommandHandler(IStarshipRepository starshipRepository, IMapper mapper, StarshipBusinessRules starshipBusinessRules)
            {
                _starshipRepository = starshipRepository;
                _mapper = mapper;
                _starshipBusinessRules = starshipBusinessRules;
            }


            public async Task<CreatedStarshipResponse> Handle(CreateStarshipCommand request, CancellationToken cancellationToken)
            {
                await _starshipBusinessRules.StarshipNameCanNotBeDuplicatedWhenInsert(request.Name);

                Starship starship = _mapper.Map<Starship>(request);
                starship.Id = Guid.NewGuid();

                var result = await _starshipRepository.InsertAsync(starship);

                var starShipResponse = _mapper.Map<CreatedStarshipResponse>(result);
                return starShipResponse;
            }
        }
    }
}
