using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfiguration
{
    public class PlanetConfiguration : IEntityTypeConfiguration<Planet>
    {
        public void Configure(EntityTypeBuilder<Planet> builder)
        {
            builder.ToTable("Films").HasKey(a => a.Id);
            builder.Property(a => a.Id).HasColumnName("Id").IsRequired();
            builder.Property(a => a.Name).HasColumnName("Name").IsRequired();
            builder.Property(a => a.ImageUrl).HasColumnName("ImageUrl").IsRequired();
            builder.Property(a => a.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(a => a.ModifiedDate).HasColumnName("ModifiedDate");
            builder.Property(a => a.DeletedDate).HasColumnName("DeletedDate");

            builder.HasIndex(indexExpression: b => b.Name, name: "Lothal_Planets_Name").IsUnique();

            builder.HasMany(b => b.Characters);

            builder.HasQueryFilter(b => b.DeletedDate.HasValue == false);
        }
    }

}
