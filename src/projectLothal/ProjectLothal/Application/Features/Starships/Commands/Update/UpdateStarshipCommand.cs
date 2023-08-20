using Application.Services;
using AutoMapper;
using MediatR;

namespace Application.Features.Starships.Commands.Update
{
    public class UpdateStarshipCommand:IRequest<UpdateStarshipResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public class UpdateStarshipCommandHandler : IRequestHandler<UpdateStarshipCommand, UpdateStarshipResponse>
        {
            private readonly IStarshipRepository _starshipRepository;
            private readonly IMapper _mapper;
            public UpdateStarshipCommandHandler(IStarshipRepository starshipRepository, IMapper mapper)
            {
                _starshipRepository = starshipRepository;
                _mapper = mapper;
            }
            public async Task<UpdateStarshipResponse> Handle(UpdateStarshipCommand request, CancellationToken cancellationToken)
            {
                var starship=await _starshipRepository.GetAsync(predicate:b=>b.Id==request.Id);
                starship = _mapper.Map(request, starship);
                await _starshipRepository.UpdateAsync(starship);
                var result=_mapper.Map<UpdateStarshipResponse>(starship);
                return result;
            }
        }
    }
}
