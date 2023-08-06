using Application.Features.Starships.Commands.Create;
using MediatR;

namespace Business.Features.Starships.Commands.Create
{
    public class CreateStarshipCommand:IRequest<CreatedStarshipResponse>
    {
        public string Name { get; set; }


        public class CreateStarshipCommandHandler : IRequestHandler<CreateStarshipCommand, CreatedStarshipResponse>
        {
            public Task<CreatedStarshipResponse> Handle(CreateStarshipCommand request, CancellationToken cancellationToken)
            {
                var starShipResponse = new CreatedStarshipResponse()
                {
                    Id=Guid.NewGuid(),
                    Name = request.Name,
                };
                throw new NotImplementedException();
            }
        }
    }
}
