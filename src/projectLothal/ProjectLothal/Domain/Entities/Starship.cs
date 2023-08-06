using Core.Persistance.Models;

namespace Domain.Entities
{
    public class Starship:Entity<Guid>
    {
        public Starship()
        {
                
        }

        public Starship(Guid id,string name)
        {
            Id = id;
            Name = name;
        }
        public required string Name { get; set; }
    }
}
