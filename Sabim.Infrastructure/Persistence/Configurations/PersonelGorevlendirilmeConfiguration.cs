using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class PersonelGorevlendirilmeConfiguration : IEntityTypeConfiguration<PersonelGorevlendirilme>
    {
        public void Configure(EntityTypeBuilder<PersonelGorevlendirilme> builder)
        {
            builder.HasKey(pg => pg.PersonelGorevlendirilmeID);
            builder.Property(pg => pg.PersonelGorevlendirilmeID).HasColumnType("SMALLINT").ValueGeneratedOnAdd().IsRequired();
            builder.Property(pg => pg.PersonelId).HasColumnType("SMALLINT").IsRequired();
            builder.Property(pg => pg.KisimId).HasColumnType("SMALLINT").IsRequired();
            builder.Property(pg => pg.AsilGorevlendirilmeYeriMi).HasDefaultValue(false).IsRequired();
            builder.Property(pg => pg.GorevlendirilmeTipiId).HasColumnType("SMALLINT").IsRequired();
            builder.Property(pg => pg.GorevlendirilmeAktifMi).HasDefaultValue(true).IsRequired();
            builder.Property(pg => pg.GorevlendirilmeBaslangicTarihi).HasColumnType("DATETIME").IsRequired();
            builder.Property(pg => pg.GorevlendirilmeBitisTarihi).HasColumnType("DATETIME").IsRequired(false);
            // Relationship configurations
            builder.HasOne(pg => pg.Personel).WithMany(p => p.PersonelGorevlendirilmes).HasForeignKey(pg => pg.PersonelId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(pg => pg.Kisim).WithMany(k => k.PersonelGorevlendirilmes).HasForeignKey(pg => pg.KisimId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(pg => pg.GorevlendirilmeTipi).WithMany(gt => gt.PersonelGorevlendirilmes).HasForeignKey(pg => pg.GorevlendirilmeTipiId).OnDelete(DeleteBehavior.Restrict);
            var config = new BaseEntityConfiguration<PersonelGorevlendirilme>();
            config.Configure(builder);
        }
    }
}