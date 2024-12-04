using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class DurumConfiguration : IEntityTypeConfiguration<Durum>
    {
        public void Configure(EntityTypeBuilder<Durum> builder)
        {
            builder.HasKey(d => d.DurumID);
            builder.Property(d => d.DurumID).HasColumnType("SMALLINT").ValueGeneratedOnAdd().IsRequired();
            builder.Property(d => d.DurumAdi).HasMaxLength(30).IsRequired().IsUnicode();
            builder.HasIndex(d => d.DurumAdi).IsUnique();
            builder.Property(d =>d.AktifMi).HasDefaultValue(true);
            builder.HasData(
                new Durum { DurumID=1, DurumAdi="Aktif", AktifMi=true},
                new Durum { DurumID=2, DurumAdi="Pasif", AktifMi=true},
                new Durum { DurumID=3, DurumAdi="Silinmiş", AktifMi=true},
                new Durum { DurumID=4, DurumAdi="Dondurulmuş", AktifMi=true},
                new Durum { DurumID=5, DurumAdi="Kapatılmış", AktifMi=true}
            );
        }
    }
}
