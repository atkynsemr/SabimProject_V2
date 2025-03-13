using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class MalzemeDurumuConfiguration : IEntityTypeConfiguration<MalzemeDurumu>
    {
        public void Configure(EntityTypeBuilder<MalzemeDurumu> builder)
        {
            builder.ToTable("MalzemeDurumu");

            builder.HasKey(x => x.MalzemeDurumuID);
            builder.Property(x => x.MalzemeDurumuAdi).HasMaxLength(20).ValueGeneratedOnAdd().IsRequired().IsUnicode(false);

            builder.HasIndex(x => x.MalzemeDurumuAdi).IsUnique();

            builder.HasMany(x => x.Malzemes)
              .WithOne(x => x.MalzemeDurumu)  // Her Malzeme'nin bir MalzemeDurumu olmalı
              .HasForeignKey(x => x.MalzemeDurumuId)  // Yabancı anahtar
              .OnDelete(DeleteBehavior.Restrict);  // Silme davranışı

            var config = new BaseEntityConfiguration<MalzemeDurumu>();
            config.Configure(builder);
        }
    }

}
