using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class SavciCalisilanKatipConfiguration : IEntityTypeConfiguration<SavciCalisilanKatip>
    {
        public void Configure(EntityTypeBuilder<SavciCalisilanKatip> builder)
        {
            // Tablo ismi, opsiyonel olarak özelleştirilebilir
            builder.ToTable("SavciCalisilanKatip");

            // Birincil Anahtar
            builder.HasKey(sck => sck.SavciCalisilanKatipID);

            builder.HasOne(sck => sck.Savci)
                .WithMany(p => p.SavciOlarakCalisilanKatips)  // Farklı koleksiyon
                .HasForeignKey(sck => sck.SavciId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sck => sck.Katip)
                .WithMany(p => p.KatipOlarakCalisilanKatips)  // Farklı koleksiyon
                .HasForeignKey(sck => sck.KatipId)
                .OnDelete(DeleteBehavior.NoAction);

            // Diğer kolonların konfigürasyonları
            builder.Property(sck => sck.GorevlendirilmeBaslamaTarihi)
                .HasDefaultValueSql("GETDATE()");  // Varsayılan tarih, veritabanında bugünün tarihi olarak ayarlandı

            builder.Property(sck => sck.GorevlendirilmeAktifMi)
                .HasDefaultValue(true);  // Varsayılan değer: true (aktif)

            builder.Property(sck => sck.GorevlendirilmeBitisTarihi)
                .IsRequired(false);  // Bitim tarihi isteğe bağlı
            var config = new BaseEntityConfiguration<SavciCalisilanKatip>();
            config.Configure(builder);
        }
    }
}
