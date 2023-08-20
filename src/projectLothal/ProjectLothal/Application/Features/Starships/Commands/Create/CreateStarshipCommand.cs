using Application.Features.Starships.Commands.Create;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Business.Features.Starships.Commands.Create
{
    public class CreateStarshipCommand:IRequest<CreatedStarshipResponse>
    {
        public string Name { get; set; }


        public class CreateStarshipCommandHandler : IRequestHandler<CreateStarshipCommand, CreatedStarshipResponse>
        {
            private readonly IStarshipRepository _starshipRepository;
            private readonly IMapper _mapper;

            public CreateStarshipCommandHandler(IStarshipRepository starshipRepository, IMapper mapper)
            {
                _starshipRepository = starshipRepository;
                _mapper = mapper;
            }
            public async Task<CreatedStarshipResponse> Handle(CreateStarshipCommand request, CancellationToken cancellationToken)
            {
                Starship starship = _mapper.Map<Starship>(request);
                starship.Id=Guid.NewGuid();
                var result= await _starshipRepository.InsertAsync(starship);

                var starShipResponse = _mapper.Map<CreatedStarshipResponse>(result);
                return starShipResponse;
            }
        }
    }
}
