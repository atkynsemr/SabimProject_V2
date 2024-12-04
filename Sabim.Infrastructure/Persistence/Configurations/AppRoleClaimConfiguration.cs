using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sabim.Domain.Entities;

namespace Sabim.Infrastructure.Persistence.Configurations
{
    public class AppRoleClaimConfiguration : IEntityTypeConfiguration<AppRoleClaim>
    {
        public void Configure(EntityTypeBuilder<AppRoleClaim> builder)
        {
            builder.ToTable("AspNetRoleClaims");
            builder.HasKey(rc => rc.Id);
            builder.Property(rc => rc.EkranId).HasColumnType("SMALLINT").IsRequired();
            builder.Property(rc => rc.OlusturulmaTarihi).HasColumnType("DATETIME").IsRequired(false);
            builder.Property(rc => rc.GuncellenmeTarihi).HasColumnType("DATETIME").IsRequired(false);
            builder.Property(rc => rc.SilinmeTarihi).HasColumnType("DATETIME").IsRequired(false);
            builder.Property(rc => rc.DurumId).IsRequired();
            builder.Property(rc => rc.ClaimValue).HasMaxLength(100).IsRequired();
            // Relationships
            builder.HasOne(rc => rc.Ekran).WithMany(e => e.AppRoleClaims).HasForeignKey(rc => rc.EkranId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(rc => rc.Durum).WithMany().HasForeignKey(rc => rc.DurumId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(rc => rc.OlusturanPersonel).WithMany().HasForeignKey(rc => rc.OlusturanPersonelId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(rc => rc.GuncelleyenPersonel).WithMany().HasForeignKey(rc => rc.GuncelleyenPersonelId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(rc => rc.SilenPersonel).WithMany().HasForeignKey(rc => rc.SilenPersonelId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
