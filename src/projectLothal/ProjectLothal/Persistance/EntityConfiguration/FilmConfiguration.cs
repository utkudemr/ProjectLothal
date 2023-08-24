using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfiguration
{
    public class FilmConfiguration : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.ToTable("Films").HasKey(a => a.Id);
            builder.Property(a => a.Id).HasColumnName("Id").IsRequired();
            builder.Property(a => a.Title).HasColumnName("Title").IsRequired();
            builder.Property(a => a.Producer).HasColumnName("Producer").IsRequired();
            builder.Property(a => a.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(a => a.ModifiedDate).HasColumnName("ModifiedDate");
            builder.Property(a => a.DeletedDate).HasColumnName("DeletedDate");

            builder.HasIndex(indexExpression: b => b.Title, name: "Lothal_Films_Name").IsUnique();

            builder.HasMany(b => b.Characters);

            builder.HasQueryFilter(b => b.DeletedDate.HasValue == false);
        }
    }

}
