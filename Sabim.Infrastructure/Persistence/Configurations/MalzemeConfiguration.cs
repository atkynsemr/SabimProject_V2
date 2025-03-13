using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class MalzemeConfiguration : IEntityTypeConfiguration<Malzeme>
    {
        public void Configure(EntityTypeBuilder<Malzeme> builder)
        {
            builder.ToTable("Malzeme");

            builder.HasKey(x => x.MalzemeID);
            builder.Property(x => x.SeriNumarasi).HasMaxLength(25).ValueGeneratedOnAdd().IsRequired().IsUnicode(false);

            // MalzemeModel ilişkisi
            builder.HasOne(x => x.MalzemeModel)
                   .WithMany(x => x.Malzemes)  // Burada "Malzemeler" koleksiyonu kullanılıyor
                   .HasForeignKey(x => x.MalzemeModelId)
                   .OnDelete(DeleteBehavior.Restrict);

            // MalzemeDurumu ilişkisi
            builder.HasOne(x => x.MalzemeDurumu)
                   .WithMany(x => x.Malzemes)  // Burada da "Malzemeler" koleksiyonu kullanılıyor
                   .HasForeignKey(x => x.MalzemeDurumuId)
                   .OnDelete(DeleteBehavior.Restrict);
            var config = new BaseEntityConfiguration<Malzeme>();
            config.Configure(builder);
        }
    }


}
