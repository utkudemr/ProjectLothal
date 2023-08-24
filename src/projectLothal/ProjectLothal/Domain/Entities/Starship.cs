using Core.Persistance.Models;
using Domain.Enums;

namespace Domain.Entities
{
    public class Starship:Entity<Guid>
    {
        public Starship()
        {
            Characters = new HashSet<Character>();
        }

        public Starship(Guid id,string name, StarshipState starshipState) :this()
        {
            Id = id;
            Name = name;
            StarshipState = starshipState;
        }
        public  string Name { get; set; }
        public StarshipState StarshipState { get; set; }
        public virtual ICollection<Character> Characters { get; set; }

    }
}
