using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class GorevlendirilmeTipiConfiguration : IEntityTypeConfiguration<GorevlendirilmeTipi>
    {
        public void Configure(EntityTypeBuilder<GorevlendirilmeTipi> builder)
        {
            builder.HasKey(gt => gt.GorevlendirilmeTipiID);
            builder.Property(gt => gt.GorevlendirilmeTipiID).HasColumnType("SMALLINT").ValueGeneratedOnAdd().IsRequired();
            builder.Property(gt => gt.GorevlendirilmeTipiAciklama).IsRequired().HasMaxLength(40).IsUnicode();
            builder.HasIndex(gt => gt.GorevlendirilmeTipiAciklama).IsUnique();
            //Relationship
            builder.HasMany(gt => gt.PersonelGorevlendirilmes).WithOne(pg => pg.GorevlendirilmeTipi).HasForeignKey(pg => pg.GorevlendirilmeTipiId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(gt => gt.PersonelGeciciGorevlendirilmes).WithOne(pg => pg.GorevlendirilmeTipi).HasForeignKey(pg => pg.GorevlendirilmeTipiId).OnDelete(DeleteBehavior.Restrict);
            var config = new BaseEntityConfiguration<GorevlendirilmeTipi>();
            config.Configure(builder);
        }
    }
}