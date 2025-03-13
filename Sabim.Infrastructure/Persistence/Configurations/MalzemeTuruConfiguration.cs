using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class MalzemeTuruConfiguration : IEntityTypeConfiguration<MalzemeTuru>
    {
        public void Configure(EntityTypeBuilder<MalzemeTuru> builder)
        {
            builder.ToTable("MalzemeTuru");

            builder.HasKey(x => x.MalzemeTuruID);
            builder.Property(x => x.TurAdi).HasMaxLength(30).ValueGeneratedOnAdd().IsRequired().IsUnicode(false);

            builder.HasMany(x => x.MalzemeCinsis)
                   .WithOne(x => x.MalzemeTuru)
                   .HasForeignKey(x => x.MalzemeTuruId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.TurAdi).IsUnique();
            var config = new BaseEntityConfiguration<MalzemeTuru>();
            config.Configure(builder);
        }
    }

}
