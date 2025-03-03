using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class PersonelGeciciGorevlendirilmeConfiguration : IEntityTypeConfiguration<PersonelGeciciGorevlendirilme>
    {
        public void Configure(EntityTypeBuilder<PersonelGeciciGorevlendirilme> builder)
        {
            builder.ToTable("PersonelGeciciGorevlendirilme");
            builder.HasKey(p => p.PersonelGeciciGorevlendirilmeID);
            builder.Property(p => p.PersonelGeciciGorevlendirilmeID).HasColumnType("SMALLINT").UseIdentityColumn();
            builder.Property(p => p.GorevlendirilmeTipiId).HasColumnType("SMALLINT").IsRequired();
            builder.Property(p => p.GorevlendirilmeAktifMi).HasDefaultValue(true).IsRequired();
            builder.Property(p => p.BaslangicTarihi).HasColumnType("DATETIME").IsRequired();
            builder.Property(p => p.BitisTarihi).HasColumnType("DATETIME").IsRequired(false);
            builder.Property(p => p.PersonelAyrilisYeriId).HasColumnType("SMALLINT").IsRequired();
            builder.Property(p => p.PersonelId).HasColumnType("SMALLINT").IsRequired();
            //Relationship
            builder.HasOne(pg => pg.Personel).WithMany(p => p.PersonelGeciciGorevlendirilmes).HasForeignKey(pg => pg.PersonelId).OnDelete(DeleteBehavior.Restrict); 
            builder.HasOne(pg => pg.PersonelAyrilisYeri).WithMany(k => k.PersonelGeciciGorevlendirilmes).HasForeignKey(pg => pg.PersonelAyrilisYeriId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(pg => pg.GorevlendirilmeTipi).WithMany(gt => gt.PersonelGeciciGorevlendirilmes).HasForeignKey(pg => pg.GorevlendirilmeTipiId).OnDelete(DeleteBehavior.Restrict);
            var config = new BaseEntityConfiguration<PersonelGeciciGorevlendirilme>();
            config.Configure(builder);
        }
    }
}
