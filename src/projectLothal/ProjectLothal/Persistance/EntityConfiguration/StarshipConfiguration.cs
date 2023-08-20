using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfiguration
{
    internal class StarshipConfiguration : IEntityTypeConfiguration<Starship>
    {
        public void Configure(EntityTypeBuilder<Starship> builder)
        {
            builder.ToTable("Starships").HasKey(a=>a.Id);
            builder.Property(a=>a.Id).HasColumnName("Id").IsRequired();
            builder.Property(a=>a.Name).HasColumnName("Name").IsRequired();
            builder.Property(a=>a.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(a=>a.ModifiedDate).HasColumnName("ModifiedDate");
            builder.Property(a=>a.DeletedDate).HasColumnName("DeletedDate");

            builder.HasQueryFilter(b => b.DeletedDate.HasValue == false);
        }
    }
}
