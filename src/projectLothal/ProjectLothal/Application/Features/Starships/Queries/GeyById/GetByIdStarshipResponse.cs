

namespace Application.Features.Starships.Queries.GeyById
{
    public class GetByIdStarshipResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
