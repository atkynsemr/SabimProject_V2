using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class MalzemeMarkaConfiguration : IEntityTypeConfiguration<MalzemeMarka>
    {
        public void Configure(EntityTypeBuilder<MalzemeMarka> builder)
        {
            builder.ToTable("MalzemeMarka");

            builder.HasKey(x => x.MalzemeMarkaID);
            builder.Property(x => x.MarkaAdi).HasMaxLength(20).ValueGeneratedOnAdd().IsRequired().IsUnicode(false);

            builder.HasMany(x => x.MalzemeModels)
                          .WithOne(x => x.MalzemeMarka)  // MalzemeModel'e karşılık gelen MalzemeMarka
                          .HasForeignKey(x => x.MalzemeMarkaId)  // Foreign key tanımı
                          .OnDelete(DeleteBehavior.Restrict); // Silme davranışı


            builder.HasOne(x => x.MalzemeCinsi)
                   .WithMany(x => x.MalzemeMarkas)
                   .HasForeignKey(x => x.MalzemeCinsiId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.MarkaAdi, x.MalzemeCinsiId }).IsUnique();
            builder.Property(x => x.MalzemeCinsiId)
                   .IsRequired();
            var config = new BaseEntityConfiguration<MalzemeMarka>();
            config.Configure(builder);
        }
    }

}
