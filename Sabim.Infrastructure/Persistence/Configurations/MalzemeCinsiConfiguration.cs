using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class MalzemeCinsiConfiguration : IEntityTypeConfiguration<MalzemeCinsi>
    {
        public void Configure(EntityTypeBuilder<MalzemeCinsi> builder)
        {
            builder.ToTable("MalzemeCinsi");

            builder.HasKey(x => x.MalzemeCinsiID);
            builder.Property(x => x.MalzemeCinsiAdi).HasMaxLength(25).ValueGeneratedOnAdd().IsRequired().IsUnicode(false);


            builder.HasOne(x => x.MalzemeTuru)
                   .WithMany(x => x.MalzemeCinsis)
                   .HasForeignKey(x => x.MalzemeTuruId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.MalzemeModels)
                   .WithOne(x => x.MalzemeCinsi)
                   .HasForeignKey(x => x.MalzemeCinsiId)
                   .OnDelete(DeleteBehavior.Restrict); ;

            builder.HasIndex(x => x.MalzemeCinsiAdi).IsUnique();
            var config = new BaseEntityConfiguration<MalzemeCinsi>();
            config.Configure(builder);
        }
    }

}
