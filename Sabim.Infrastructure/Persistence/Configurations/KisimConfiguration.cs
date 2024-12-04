using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class KisimConfiguration : IEntityTypeConfiguration<Kisim>
    {
        public void Configure(EntityTypeBuilder<Kisim> builder)
        {
            builder.HasKey(k => k.KisimID);
            builder.Property(k => k.KisimID).HasColumnType("SMALLINT").ValueGeneratedOnAdd().IsRequired();
            builder.Property(k => k.KisimAdi).IsRequired().HasMaxLength(50).IsUnicode();
            builder.Property(k => k.Aciklama).HasMaxLength(75);
            builder.Property(k => k.DahiliTelefon).IsRequired().HasMaxLength(25);
            //Relationship
            builder.HasOne(k => k.Birim).WithMany(b => b.Kisims).HasForeignKey(k => k.BirimId);
            builder.HasOne(k => k.KabinetBazliBolum).WithMany(k => k.Kisims).HasForeignKey(k => k.KabinetBazliBolumId);
            builder.HasMany(k => k.PersonelGorevlendirilmes).WithOne(pg => pg.Kisim).HasForeignKey(pg => pg.KisimId).OnDelete(DeleteBehavior.Restrict);
            var config = new BaseEntityConfiguration<Kisim>();
            config.Configure(builder);
        }
    }
}
