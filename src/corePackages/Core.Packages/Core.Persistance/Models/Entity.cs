

namespace Core.Persistance.Models
{
    public class Entity<TId>
    {
        public Entity()
        {
            Id = default;
        }

        public Entity(TId id)
        {
            Id = id;
        }
        public TId Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? Creator { get; set; }

        
    }
}
