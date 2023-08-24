using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfiguration
{
    public class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.ToTable("Characters").HasKey(a => a.Id);
            builder.Property(a => a.Id).HasColumnName("Id").IsRequired();
            builder.Property(a => a.Name).HasColumnName("Name").IsRequired();
            builder.Property(a => a.Height).HasColumnName("Height").IsRequired();
            builder.Property(a => a.HairColor).HasColumnName("HairColor").IsRequired();
            builder.Property(a => a.PlanetId).HasColumnName("PlanetId").IsRequired();
            builder.Property(a => a.ImageUrl).HasColumnName("ImageUrl").IsRequired();
            builder.Property(a => a.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(a => a.ModifiedDate).HasColumnName("ModifiedDate");
            builder.Property(a => a.DeletedDate).HasColumnName("DeletedDate");

            builder.HasIndex(indexExpression: b => b.Name, name: "Lothal_Characters_Name").IsUnique();

            builder.HasMany(b => b.Starships);
            builder.HasMany(b => b.Films);
            builder.HasMany(b => b.Films);
            builder.HasOne(b => b.Planet);

            builder.HasQueryFilter(b => b.DeletedDate.HasValue == false);
        }
    }

}
