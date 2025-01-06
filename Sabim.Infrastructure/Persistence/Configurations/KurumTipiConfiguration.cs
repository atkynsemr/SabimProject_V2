using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class KurumTipiConfiguration : IEntityTypeConfiguration<KurumTipi>
    {
        public void Configure(EntityTypeBuilder<KurumTipi> builder)
        {
            builder.HasKey(kt => kt.KurumTipiID);
            builder.Property(kt => kt.KurumTipiID).HasColumnType("SMALLINT").ValueGeneratedOnAdd().IsRequired();
            builder.Property(kt => kt.KurumTipiAdi).HasMaxLength(50).IsRequired().IsUnicode();
            builder.HasIndex(kt => kt.KurumTipiAdi).IsUnique();
            //builder.HasData
            //    (
            //        new KurumTipi { KurumTipiID=1, KurumTipiAdi= "Personel Görev Yeri", DurumId=1 },
            //        new KurumTipi { KurumTipiID=2, KurumTipiAdi= "Teknik Hizmet Yeri", DurumId=1 },
            //        new KurumTipi { KurumTipiID=3, KurumTipiAdi= "Donanım Alınan Yer", DurumId=1 }
            //    );
            //Relationship
            builder.HasMany(k => k.Kurums).WithOne(kt => kt.KurumTipi).HasForeignKey(k => k.KurumTipiId).OnDelete(DeleteBehavior.Restrict);
            var config = new BaseEntityConfiguration<KurumTipi>();
            config.Configure(builder);
        }
    }
}
