using Application.Services;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Starships.Commands.Delete
{
    public class DeleteStarshipCommand:IRequest<DeletedStarshipResponse>
    {
        public Guid Id { get; set; }
        public class DeleteStarshipCommandHandler : IRequestHandler<DeleteStarshipCommand, DeletedStarshipResponse>
        {
            private readonly IStarshipRepository _starshipRepository;
            private readonly IMapper _mapper;
            public DeleteStarshipCommandHandler(IStarshipRepository starshipRepository, IMapper mapper)
            {
                _starshipRepository = starshipRepository;
                _mapper = mapper;
            }
            public async Task<DeletedStarshipResponse> Handle(DeleteStarshipCommand request, CancellationToken cancellationToken)
            {
                var starship = await _starshipRepository.GetAsync(predicate: b => b.Id == request.Id);
                await _starshipRepository.DeleteAsync(starship);
                var deletedResponse= _mapper.Map<DeletedStarshipResponse>(starship);
                return deletedResponse;
            }
        }
    }
}
