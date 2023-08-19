

namespace Core.Persistance.Models
{
    public class Entity<TId> : IEntityTimestamps
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
        public int? Creator { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

    }
}
