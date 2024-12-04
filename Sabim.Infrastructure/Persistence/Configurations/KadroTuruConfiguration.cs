using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class KadroTuruConfiguration : IEntityTypeConfiguration<KadroTuru>
    {
        public void Configure(EntityTypeBuilder<KadroTuru> builder)
        {
            builder.HasKey(k => k.KadroTuruID);
            builder.Property(k => k.KadroTuruID).HasColumnType("SMALLINT").ValueGeneratedOnAdd().IsRequired();
            builder.Property(k => k.KadroTuruAdi).HasMaxLength(50).IsRequired().IsUnicode();
            builder.HasIndex(k => k.KadroTuruAdi).IsUnique();
            builder.HasData(
                new KadroTuru { KadroTuruID = 1, KadroTuruAdi = "Kadrolu", DurumId = 1 },
                new KadroTuru { KadroTuruID = 2, KadroTuruAdi = "Sözleşmeli(4/B)", DurumId = 1 },
                new KadroTuru { KadroTuruID = 3, KadroTuruAdi = "Sürekli İşçi(4/D)-Taşerondan Geçen", DurumId = 1 },
                new KadroTuru { KadroTuruID = 4, KadroTuruAdi = "Sürekli İşçi(4/D)-Açıktan Atama", DurumId = 1 }
                );
            //Relationship
            builder.HasMany(k => k.Personels).WithOne(p => p.KadroTuru).HasForeignKey(p => p.KadroTuruId).OnDelete(DeleteBehavior.Restrict);
            var baseConfig = new BaseEntityConfiguration<KadroTuru>();
            baseConfig.Configure(builder);
        }
    }
}
